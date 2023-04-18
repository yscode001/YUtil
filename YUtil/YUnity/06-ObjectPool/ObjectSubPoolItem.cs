using System;
using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    internal partial class ObjectSubPoolItem
    {
        // 对象地址，唯一标识
        private readonly string address;

        private GameObject prefab;

        private readonly ObjectSubPool subPool;

        internal ObjectSubPoolItem(string address, GameObject prefab)
        {
            if (string.IsNullOrWhiteSpace(address) || prefab == null)
            {
                throw new System.Exception("ObjectPool-初始化ObjectSubPoolItem：address和prefab不能为空");
            }
            this.address = address;
            this.prefab = prefab;
            this.subPool = new ObjectSubPool();
        }
    }
    internal partial class ObjectSubPoolItem
    {
        internal bool Contains(GameObject go)
        {
            return subPool.Contains(go);
        }

        internal void OnlyClearCacheList()
        {
            subPool.OnlyClearCacheList();
        }
    }
    internal partial class ObjectSubPoolItem
    {
        internal GameObject Spawn(Transform parent)
        {
            if (prefab != null)
            {
                return subPool.Spawn(prefab, parent);
            }
            else
            {
                throw new System.Exception("ObjectPool-Spawn：prefab不能为空");
            }
        }
    }
    internal partial class ObjectSubPoolItem
    {
        internal void UnSpawn(GameObject go)
        {
            subPool.UnSpawn(go);
        }
        internal void UnSpawnAll()
        {
            subPool.UnSpawnAll();
        }
        internal void UnSpawnAll(List<GameObject> except)
        {
            subPool.UnSpawnAll(except);
        }
    }
    internal partial class ObjectSubPoolItem
    {
        internal void Release(Action<GameObject> release, GameObject go)
        {
            subPool.Release(release, go);
        }
        internal void ReleaseAll(Action<GameObject> release)
        {
            subPool.ReleaseAll(release);
        }
        internal void ReleaseAll(Action<GameObject> release, List<GameObject> except)
        {
            subPool.ReleaseAll(release, except);
        }
    }
}