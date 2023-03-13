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
    }

    internal partial class ObjectSubPool
    {
        internal GameObject Spawn(Transform parentTransform, GameObject prefabGO)
        {
            if (parentTransform == null || prefabGO == null)
            {
                throw new System.Exception("ObjectPool：parentTransform和prefabGO不能为空");
            }
            GameObject go = null;
            foreach (var obj in objectList)
            {
                if (obj.activeSelf == false)
                {
                    // 有未激活的游戏物体，取出进行复用
                    go = obj;
                    break;
                }
            }
            if (go == null)
            {
                // 无未激活的游戏物体，重新生成
                go = GameObject.Instantiate<GameObject>(prefabGO, parentTransform, false);
                objectList.Add(go);
            }
            go.SetActive(true);
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
                release?.Invoke(go);
                objectList.Remove(go);
            }
        }
        internal void ReleaseAll(Action<GameObject> release)
        {
            foreach (var obj in objectList)
            {
                Release(release, obj);
            }
        }
        internal void ReleaseAll(Action<GameObject> release, List<GameObject> except)
        {
            foreach (var obj in objectList)
            {
                if (except != null && except.Contains(obj))
                {
                    continue;
                }
                Release(release, obj);
            }
        }
    }
}