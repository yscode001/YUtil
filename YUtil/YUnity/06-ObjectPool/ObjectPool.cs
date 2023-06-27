using System;
using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    public partial class ObjectPool
    {
        private static readonly Dictionary<GameObject, ObjectSubPool> subpoolList = new Dictionary<GameObject, ObjectSubPool>();

        public static void OnlyClearCacheList()
        {
            foreach (var itemValue in subpoolList.Values)
            {
                itemValue.OnlyClearCacheList();
            }
            subpoolList.Clear();
        }
    }
    public partial class ObjectPool
    {
        public static GameObject Spawn(GameObject prefab, Transform parent, bool transformReset = true)
        {
            if (prefab == null)
            {
                throw new System.Exception("ObjectPool-Spawn：prefab不能为空");
            }
            if (!subpoolList.ContainsKey(prefab))
            {
                subpoolList.Add(prefab, new ObjectSubPool());
            }
            return subpoolList[prefab].Spawn(prefab, parent, transformReset);
        }
        public static T Spawn<T>(GameObject prefab, Transform parent, bool transformReset = true) where T : Component
        {
            return Spawn(prefab, parent, transformReset).GetOrAddComponent<T>();
        }
        public static Tuple<T1, T2> Spawn<T1, T2>(GameObject prefab, Transform parent, bool transformReset = true)
            where T1 : Component
            where T2 : Component
        {
            GameObject go = Spawn(prefab, parent, transformReset);
            return new Tuple<T1, T2>(go.GetOrAddComponent<T1>(), go.GetOrAddComponent<T2>());
        }
        public static Tuple<T1, T2, T3> Spawn<T1, T2, T3>(GameObject prefab, Transform parent, bool transformReset = true)
           where T1 : Component
           where T2 : Component
           where T3 : Component
        {
            GameObject go = Spawn(prefab, parent, transformReset);
            return new Tuple<T1, T2, T3>(go.GetOrAddComponent<T1>(), go.GetOrAddComponent<T2>(), go.GetOrAddComponent<T3>());
        }
        public static Tuple<T1, T2, T3, T4> Spawn<T1, T2, T3, T4>(GameObject prefab, Transform parent, bool transformReset = true)
           where T1 : Component
           where T2 : Component
           where T3 : Component
           where T4 : Component
        {
            GameObject go = Spawn(prefab, parent, transformReset);
            return new Tuple<T1, T2, T3, T4>(go.GetOrAddComponent<T1>(), go.GetOrAddComponent<T2>(), go.GetOrAddComponent<T3>(), go.GetOrAddComponent<T4>());
        }
        public static Tuple<T1, T2, T3, T4, T5> Spawn<T1, T2, T3, T4, T5>(GameObject prefab, Transform parent, bool transformReset = true)
           where T1 : Component
           where T2 : Component
           where T3 : Component
           where T4 : Component
           where T5 : Component
        {
            GameObject go = Spawn(prefab, parent, transformReset);
            return new Tuple<T1, T2, T3, T4, T5>(go.GetOrAddComponent<T1>(), go.GetOrAddComponent<T2>(), go.GetOrAddComponent<T3>(), go.GetOrAddComponent<T4>(), go.GetOrAddComponent<T5>());
        }
    }
    public partial class ObjectPool
    {
        public static void UnSpawn(GameObject go)
        {
            if (go == null) { return; }
            foreach (var itemValue in subpoolList.Values)
            {
                if (itemValue.Contains(go))
                {
                    itemValue.UnSpawn(go);
                    return;
                }
            }
        }

        public static void UnSpawnAll()
        {
            foreach (var itemValue in subpoolList.Values)
            {
                itemValue.UnSpawnAll();
            }
        }

        public static void UnSpawnAll(GameObject prefab)
        {
            if (prefab == null || !subpoolList.ContainsKey(prefab)) { return; }
            subpoolList[prefab].UnSpawnAll();
        }
    }
    public partial class ObjectPool
    {
        public static void Release(GameObject go, bool immediate = false)
        {
            if (go == null) { return; }
            List<GameObject> willRemove = new List<GameObject>();
            foreach (var item in subpoolList)
            {
                if (item.Value.Contains(go))
                {
                    item.Value.Release(go, immediate);
                    if (!item.Value.HasElement && item.Key != null)
                    {
                        willRemove.Add(item.Key);
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
            List<GameObject> willRemove = new List<GameObject>();
            foreach (var item in subpoolList)
            {
                item.Value.ReleaseAll(immediate);
                if (!item.Value.HasElement && item.Key != null)
                {
                    willRemove.Add(item.Key);
                }
            }
            foreach (var remove in willRemove)
            {
                subpoolList.Remove(remove);
            }
        }

        public static void ReleaseAll(GameObject prefab, bool immediate = false)
        {
            if (prefab == null || !subpoolList.ContainsKey(prefab)) { return; }
            List<GameObject> willRemove = new List<GameObject>();
            if (subpoolList.TryGetValue(prefab, out ObjectSubPool subpool))
            {
                subpool.ReleaseAll(immediate);
                if (!subpool.HasElement && prefab != null)
                {
                    willRemove.Add(prefab);
                }
            }
            foreach (var remove in willRemove)
            {
                subpoolList.Remove(remove);
            }
        }
    }
}