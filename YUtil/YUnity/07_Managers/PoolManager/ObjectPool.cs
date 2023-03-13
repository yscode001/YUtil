using System;
using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    public partial class ObjectPool
    {
        private static Dictionary<string, ObjectSubPoolItem> subpoolList = new Dictionary<string, ObjectSubPoolItem>();

        public static void OnlyClearCacheList()
        {
            foreach (KeyValuePair<string, ObjectSubPoolItem> dict in subpoolList)
            {
                dict.Value.OnlyClearCacheList();
            }
            subpoolList.Clear();
        }
    }
    public partial class ObjectPool
    {
        public static void Spawn(string address, Transform parentTransform, Func<GameObject> loadPrefabFunc, Action<GameObject> complete)
        {
            if (string.IsNullOrWhiteSpace(address) || parentTransform == null || loadPrefabFunc == null)
            {
                throw new System.Exception("ObjectPool：address和parentTransform和loadPrefabFunc不能为空");
            }
            if (!subpoolList.ContainsKey(address.Trim()))
            {
                subpoolList.Add(address.Trim(), new ObjectSubPoolItem(address, loadPrefabFunc));
            }
            if (subpoolList.TryGetValue(address.Trim(), out ObjectSubPoolItem subpoolItem))
            {
                subpoolItem.Spawn(parentTransform, complete);
            }
            else
            {
                throw new System.Exception("ObjectPool：从池中取出游戏物体出现错误");
            }
        }
    }
    public partial class ObjectPool
    {
        public static void UnSpawn(GameObject go)
        {
            if (go == null) { return; }
            ObjectSubPoolItem pool = null;
            foreach (var subpool in subpoolList.Values)
            {
                if (subpool.Contains(go))
                {
                    pool = subpool;
                    break;
                }
            }
            if (pool != null)
            {
                pool.UnSpawn(go);
            }
        }

        public static void UnSpawnAll()
        {
            foreach (var subpool in subpoolList.Values)
            {
                subpool.UnSpawnAll();
            }
        }

        public static void UnSpawnAll(List<GameObject> except)
        {
            foreach (var subpool in subpoolList.Values)
            {
                subpool.UnSpawnAll(except);
            }
        }

        public static void UnSpawnAll(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) { return; }
            if (!subpoolList.ContainsKey(address.Trim())) { return; }
            if (subpoolList.TryGetValue(address.Trim(), out ObjectSubPoolItem subpoolItem))
            {
                subpoolItem.UnSpawnAll();
            }
        }

        public static void UnSpawnAll(string address, List<GameObject> except)
        {
            if (string.IsNullOrWhiteSpace(address)) { return; }
            if (!subpoolList.ContainsKey(address.Trim())) { return; }
            if (subpoolList.TryGetValue(address.Trim(), out ObjectSubPoolItem subpoolItem))
            {
                subpoolItem.UnSpawnAll(except);
            }
        }
    }
    public partial class ObjectPool
    {
        public static void Release(Action<GameObject> release, GameObject go)
        {
            if (go == null) { return; }
            ObjectSubPoolItem pool = null;
            foreach (var subpool in subpoolList.Values)
            {
                if (subpool.Contains(go))
                {
                    pool = subpool;
                    break;
                }
            }
            if (pool != null)
            {
                pool.Release(release, go);
            }
        }

        public static void ReleaseAll(Action<GameObject> release)
        {
            foreach (var subpool in subpoolList.Values)
            {
                subpool.ReleaseAll(release);
            }
        }

        public static void ReleaseAll(Action<GameObject> release, List<GameObject> except)
        {
            foreach (var subpool in subpoolList.Values)
            {
                subpool.ReleaseAll(release, except);
            }
        }

        public static void ReleaseAll(Action<GameObject> release, string address)
        {
            if (string.IsNullOrWhiteSpace(address)) { return; }
            if (!subpoolList.ContainsKey(address.Trim())) { return; }
            if (subpoolList.TryGetValue(address.Trim(), out ObjectSubPoolItem subpoolItem))
            {
                subpoolItem.ReleaseAll(release);
            }
        }

        public static void ReleaseAll(Action<GameObject> release, string address, List<GameObject> except)
        {
            if (string.IsNullOrWhiteSpace(address)) { return; }
            if (!subpoolList.ContainsKey(address.Trim())) { return; }
            if (subpoolList.TryGetValue(address.Trim(), out ObjectSubPoolItem subpoolItem))
            {
                subpoolItem.ReleaseAll(release, except);
            }
        }
    }
}