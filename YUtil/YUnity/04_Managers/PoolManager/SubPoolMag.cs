using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    internal class SubPoolMag
    {
        /// <summary>
        /// 池中对象集合
        /// </summary>
        private readonly List<GameObject> objectList = new List<GameObject>();

        /// <summary>
        /// 预制体
        /// </summary>
        private GameObject prefabGO;

        /// <summary>
        /// 父物体的Transform
        /// </summary>
        private Transform parentTransform;

        private SubPoolMag() { }

        public SubPoolMag(Transform parentTransform, GameObject prefabGO)
        {
            if (parentTransform == null || prefabGO == null)
            {
                throw new System.Exception("parentTransform和prefabGO不能为空");
            }
            this.parentTransform = parentTransform;
            this.prefabGO = prefabGO;
        }

        /// <summary>
        /// 从池中取出游戏物体
        /// </summary>
        /// <returns></returns>
        internal GameObject Spawn()
        {
            GameObject go = null;
            foreach (var obj in objectList)
            {
                if (!obj.activeSelf)
                {
                    go = obj;
                    break;
                }
            }
            if (go == null)
            {
                go = GameObject.Instantiate<GameObject>(prefabGO);
                go.transform.SetParent(parentTransform, false);
                objectList.Add(go);
            }
            go.SetActive(true);
            go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
            return go;
        }

        /// <summary>
        /// 将游戏物体回收至池中
        /// </summary>
        /// <param name="go"></param>
        internal void UnSpawn(GameObject go)
        {
            if (go == null) { return; }
            if (Contains(go))
            {
                go.SendMessage("OnUnSpawn", SendMessageOptions.DontRequireReceiver);
                go.SetActive(false);
            }
        }

        /// <summary>
        /// 回收所有的游戏物体至池中
        /// </summary>
        internal void UnSpawnAll()
        {
            foreach (var obj in objectList)
            {
                if (obj.activeSelf)
                {
                    UnSpawn(obj);
                }
            }
        }

        /// <summary>
        /// 除了XXX，回收所有的游戏物体至池中
        /// </summary>
        /// <param name="except"></param>
        internal void UnSpawnAll(List<GameObject> except)
        {
            foreach (var obj in objectList)
            {
                if (obj.activeSelf)
                {
                    if (except != null && except.Contains(obj))
                    {
                        continue;
                    }
                    UnSpawn(obj);
                }
            }
        }

        /// <summary>
        /// 池中是否包含某个游戏物体
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        internal bool Contains(GameObject go)
        {
            if (go == null) { return false; }
            return objectList.Contains(go);
        }

        /// <summary>
        /// 清理池中所有物体，但不会对物体进行任何操作，常用与场景切换时
        /// </summary>
        internal void Clear()
        {
            objectList.Clear();
        }
    }
}