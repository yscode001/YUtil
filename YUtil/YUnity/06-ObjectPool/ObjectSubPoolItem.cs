using System;
using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    internal partial class ObjectSubPoolItem
    {
        // 对象地址，唯一标识
        public readonly string address;

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

        public bool HasElement => subPool.HasElement;
    }
    internal partial class ObjectSubPoolItem
    {
        internal GameObject Spawn(Transform parent, bool transformReset)
        {
            if (prefab != null)
            {
                return subPool.Spawn(prefab, parent, transformReset);
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
    }
    internal partial class ObjectSubPoolItem
    {
        internal void Release(GameObject go, bool immediate)
        {
            subPool.Release(go, immediate);
        }
        internal void ReleaseAll(bool immediate)
        {
            subPool.ReleaseAll(immediate);
        }
    }
}