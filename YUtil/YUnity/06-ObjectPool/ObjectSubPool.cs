﻿using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    public enum PositionEnum
    {
        World,
        Local,
    }

    internal partial class ObjectSubPool
    {
        private readonly List<GameObject> objectList = new List<GameObject>();

        internal bool Contains(GameObject go)
        {
            return go != null && objectList.Contains(go);
        }

        internal void OnlyClearCacheList()
        {
            objectList.Clear();
        }

        public bool HasElement => objectList.Count > 0;
    }

    internal partial class ObjectSubPool
    {
        /// <summary>
        /// 防止外界由于种种原因销毁了object
        /// </summary>
        private void RemoveNullObjectBeforeSpawn()
        {
            for (int i = objectList.Count - 1; i >= 0; i--)
            {
                if (objectList[i] == null)
                {
                    objectList.RemoveAt(i);
                }
            }
        }

        private GameObject SpawnAction(GameObject prefab, Transform parent)
        {
            if (prefab == null)
            {
                throw new System.Exception("ObjectPool-Spawn：prefab不能为空");
            }
            RemoveNullObjectBeforeSpawn();
            GameObject go = null;
            foreach (var obj in objectList)
            {
                if (obj.activeSelf == false)
                {
                    // 有未激活的游戏物体，取出进行复用
                    go = obj;
                    go.transform.SetParent(parent);
                    break;
                }
            }
            if (go == null)
            {
                // 无未激活的游戏物体，重新生成
                go = GameObject.Instantiate<GameObject>(prefab, parent, false);
                objectList.Add(go);
            }
            return go;
        }

        internal GameObject Spawn(GameObject prefab, Transform parent, bool transformReset)
        {
            GameObject go = SpawnAction(prefab, parent);
            if (transformReset) { go.transform.Reset(true); }

            ReusableGameObject reusable = go.GetComponent<ReusableGameObject>();
            if (reusable != null)
            {
                reusable.UnSpawnChildrenWhoInheritReusableGameObjectFromObjectPool();
                reusable.BeforeSpawnSetActive();
            }
            if (go.activeSelf == false)
            {
                go.SetActive(true);
            }
            go.SendMessage("OnSpawnOrUnSpawn", true, SendMessageOptions.DontRequireReceiver);
            return go;
        }

        internal GameObject SpawnWithPosition(GameObject prefab, Transform parent, PositionEnum positionEnum, Vector3 position)
        {
            GameObject go = SpawnAction(prefab, parent);
            go.transform.localScale = Vector3.one;
            go.transform.localEulerAngles = Vector3.zero;
            switch (positionEnum)
            {
                case PositionEnum.Local:
                    go.transform.localPosition = position;
                    break;
                default:
                    go.transform.position = position;
                    break;
            }

            ReusableGameObject reusable = go.GetComponent<ReusableGameObject>();
            if (reusable != null)
            {
                reusable.UnSpawnChildrenWhoInheritReusableGameObjectFromObjectPool();
                reusable.BeforeSpawnSetActive();
            }
            if (go.activeSelf == false)
            {
                go.SetActive(true);
            }
            go.SendMessage("OnSpawnOrUnSpawn", true, SendMessageOptions.DontRequireReceiver);
            return go;
        }
    }

    internal partial class ObjectSubPool
    {
        internal void UnSpawn(GameObject go)
        {
            if (go != null && go.activeSelf && Contains(go))
            {
                go.SendMessage("OnSpawnOrUnSpawn", false, SendMessageOptions.DontRequireReceiver);
                if (go.activeSelf)
                {
                    go.SetActive(false);
                }
            }
        }
        internal void UnSpawnAll()
        {
            foreach (var obj in objectList)
            {
                UnSpawn(obj);
            }
        }
    }

    internal partial class ObjectSubPool
    {
        internal void Release(GameObject go, bool immediate)
        {
            if (go != null && Contains(go))
            {
                // 先回收，再释放
                UnSpawn(go);
                objectList.Remove(go);
                if (immediate) { GameObject.DestroyImmediate(go); }
                else { GameObject.Destroy(go); }
            }
        }
        internal void ReleaseAll(bool immediate)
        {
            for (int i = objectList.Count - 1; i >= 0; i--)
            {
                var obj = objectList[i];
                Release(obj, immediate);
            }
            objectList.Clear();
        }
    }
}