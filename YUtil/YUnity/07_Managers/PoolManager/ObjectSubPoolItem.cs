using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace YUnity
{
    internal partial class ObjectSubPoolItem
    {
        private string objectAddress;
        private GameObject prefab;
        private ObjectSubPool subPool;

        private Func<GameObject> loadPrefabFunc;

        internal ObjectSubPoolItem(string objectAddress, Func<GameObject> loadPrefabFunc)
        {
            if (string.IsNullOrWhiteSpace(objectAddress) || loadPrefabFunc == null)
            {
                throw new System.Exception("ObjectPool：objectAddress和loadPrefabAction不能为空");
            }
            this.objectAddress = objectAddress;
            this.loadPrefabFunc = loadPrefabFunc;
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
        private bool AlreadyGetPrefab = false; // 是否取过资源
        private readonly Semaphore semaphore = new Semaphore(1, 1);

        internal void Spawn(Transform parentTransform, Action<GameObject> complete)
        {
            if (prefab != null)
            {
                complete?.Invoke(subPool.Spawn(parentTransform, prefab));
            }
            else if (AlreadyGetPrefab)
            {
                // 资源不存在
                this.Error($"ObjectPool：资源不存在：{objectAddress}");
                complete?.Invoke(null);
            }
            else if (string.IsNullOrWhiteSpace(objectAddress))
            {
                throw new System.Exception("ObjectPool：objectAddress不能为空");
            }
            else
            {
                // 取资源
                GetPrefabResourcesAsyncAndThenSpawn(parentTransform, complete);
            }
        }

        private void GetPrefabResourcesAsyncAndThenSpawn(Transform parentTransform, Action<GameObject> complete)
        {
            semaphore.WaitOne();
            if (prefab != null)
            {
                complete?.Invoke(subPool.Spawn(parentTransform, prefab));
            }
            else if (AlreadyGetPrefab)
            {
                // 资源不存在
                this.Error($"ObjectPool：资源不存在：{objectAddress}");
                complete?.Invoke(null);
            }
            else
            {
                prefab = loadPrefabFunc.Invoke();
                AlreadyGetPrefab = true;
                if (prefab != null)
                {
                    complete?.Invoke(subPool.Spawn(parentTransform, prefab));
                }
                else
                {
                    this.Error($"ObjectPool：资源不存在：{objectAddress}");
                    complete?.Invoke(null);
                }
                semaphore.Release();
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