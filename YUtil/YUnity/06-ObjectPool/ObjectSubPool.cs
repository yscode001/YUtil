using System;
using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
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
        internal GameObject Spawn(GameObject prefab, Transform parent)
        {
            if (prefab == null)
            {
                throw new System.Exception("ObjectPool-Spawn：prefab不能为空");
            }
            GameObject go = null;
            foreach (var obj in objectList)
            {
                if (obj.activeSelf == false)
                {
                    // 有未激活的游戏物体，取出进行复用
                    go = obj;
                    go.transform.parent = parent;
                    break;
                }
            }
            if (go == null)
            {
                // 无未激活的游戏物体，重新生成
                go = GameObject.Instantiate<GameObject>(prefab, parent, false);
                objectList.Add(go);
            }
            go.transform.Reset();
            if (go.activeSelf == false)
            {
                go.SetActive(true);
            }
            go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
            return go;
        }
    }

    internal partial class ObjectSubPool
    {
        internal void UnSpawn(GameObject go)
        {
            if (go != null && go.activeSelf && Contains(go))
            {
                go.SendMessage("OnUnSpawn", SendMessageOptions.DontRequireReceiver);
                go.SetActive(false);
            }
        }
        internal void UnSpawnAll()
        {
            foreach (var obj in objectList)
            {
                UnSpawn(obj);
            }
        }
        internal void UnSpawnAll(List<GameObject> except)
        {
            foreach (var obj in objectList)
            {
                if (except != null && except.Contains(obj))
                {
                    continue;
                }
                UnSpawn(obj);
            }
        }
    }

    internal partial class ObjectSubPool
    {
        internal void Release(Action<GameObject> release, GameObject go)
        {
            if (go != null && Contains(go))
            {
                // 先回收，再释放
                UnSpawn(go);
                release?.Invoke(go);
                objectList.Remove(go);
            }
        }
        internal void ReleaseAll(Action<GameObject> release)
        {
            for (int i = objectList.Count - 1; i >= 0; i--)
            {
                var obj = objectList[i];
                Release(release, obj);
            }
            objectList.Clear();
        }
        internal void ReleaseAll(Action<GameObject> release, List<GameObject> except)
        {
            for (int i = objectList.Count - 1; i >= 0; i--)
            {
                var obj = objectList[i];
                if (except != null && except.Contains(obj))
                {
                    continue;
                }
                Release(release, obj);
            }
        }
    }
}