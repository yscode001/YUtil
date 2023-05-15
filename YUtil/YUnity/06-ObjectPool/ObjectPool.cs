using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    public partial class ObjectPool
    {
        private static readonly Dictionary<string, ObjectSubPoolItem> subpoolList = new Dictionary<string, ObjectSubPoolItem>();

        public static void OnlyClearCacheList()
        {
            foreach (var item in subpoolList)
            {
                item.Value.OnlyClearCacheList();
            }
            subpoolList.Clear();
        }
    }
    public partial class ObjectPool
    {
        public static GameObject Spawn(string address, GameObject prefab, Transform parent, bool transformReset = true)
        {
            if (string.IsNullOrWhiteSpace(address) || prefab == null)
            {
                throw new System.Exception("ObjectPool-Spawn：address和prefab不能为空");
            }
            if (!subpoolList.ContainsKey(address.Trim()))
            {
                subpoolList.Add(address.Trim(), new ObjectSubPoolItem(address.Trim(), prefab));
            }
            if (subpoolList.TryGetValue(address.Trim(), out ObjectSubPoolItem subpoolItem))
            {

                return subpoolItem.Spawn(parent, transformReset);
            }
            else
            {
                throw new System.Exception("ObjectPool-Spawn：取subpool时出错");
            }
        }
    }
    public partial class ObjectPool
    {
        public static void UnSpawn(GameObject go)
        {
            if (go == null) { return; }
            foreach (var subpool in subpoolList.Values)
            {
                if (subpool.Contains(go))
                {
                    subpool.UnSpawn(go);
                    return;
                }
            }
        }

        public static void UnSpawnAll()
        {
            foreach (var subpool in subpoolList.Values)
            {
                subpool.UnSpawnAll();
            }
        }

        public static void UnSpawnAll(string address)
        {
            if (string.IsNullOrWhiteSpace(address) || !subpoolList.ContainsKey(address.Trim())) { return; }
            if (subpoolList.TryGetValue(address.Trim(), out ObjectSubPoolItem subpoolItem))
            {
                subpoolItem.UnSpawnAll();
            }
        }
    }
    public partial class ObjectPool
    {
        public static void Release(GameObject go, bool immediate = false)
        {
            if (go == null) { return; }
            List<string> willRemove = new List<string>();
            foreach (var subpool in subpoolList.Values)
            {
                if (subpool.Contains(go))
                {
                    subpool.Release(go, immediate);
                    if (!subpool.HasElement)
                    {
                        willRemove.Add(subpool.address);
                    }
                    break;
                }
            }
            foreach (var remove in willRemove)
            {
                subpoolList.Remove(remove);
            }
        }

        public static void ReleaseAll(bool immediate = false)
        {
            List<string> willRemove = new List<string>();
            foreach (var subpool in subpoolList.Values)
            {
                subpool.ReleaseAll(immediate);
                if (!subpool.HasElement)
                {
                    willRemove.Add(subpool.address);
                }
            }
            foreach (var remove in willRemove)
            {
                subpoolList.Remove(remove);
            }
        }

        public static void ReleaseAll(string address, bool immediate = false)
        {
            if (string.IsNullOrWhiteSpace(address) || !subpoolList.ContainsKey(address.Trim())) { return; }
            List<string> willRemove = new List<string>();
            if (subpoolList.TryGetValue(address.Trim(), out ObjectSubPoolItem subpoolItem))
            {
                subpoolItem.ReleaseAll(immediate);
                if (!subpoolItem.HasElement)
                {
                    willRemove.Add(subpoolItem.address);
                }
            }
            foreach (var remove in willRemove)
            {
                subpoolList.Remove(remove);
            }
        }
    }
}