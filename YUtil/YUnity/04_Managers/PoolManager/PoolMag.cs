using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 对象池
    /// </summary>
    public class PoolMag : MonoBehaviourBaseY
    {
        private PoolMag() { }
        public static PoolMag Instance { get; private set; } = null;

        internal void Init()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        /// <summary>
        /// SubPool的池子，key是Prefab在Resources文件夹下的路径
        /// </summary>
        private Dictionary<string, SubPoolMag> subpoolList = new Dictionary<string, SubPoolMag>();

        /// <summary>
        /// 从池中取出游戏物体
        /// </summary>
        /// <param name="prefabResourcesPath">Prefab在Resources文件夹下的路径</param>
        /// <param name="parentTransform"></param>
        /// <returns></returns>
        public GameObject Spawn(string prefabResourcesPath, Transform parentTransform)
        {
            if (string.IsNullOrWhiteSpace(prefabResourcesPath) || parentTransform == null)
            {
                throw new System.Exception("prefabResourcesPath和parentTransform不能为空");
            }
            if (!subpoolList.ContainsKey(prefabResourcesPath.Trim()))
            {
                RegieterNewSubPool(prefabResourcesPath.Trim(), parentTransform);
            }
            if (subpoolList.TryGetValue(prefabResourcesPath.Trim(), out SubPoolMag subpool))
            {
                return subpool.Spawn();
            }
            else
            {
                throw new System.Exception("从池中取出游戏物体出现错误");
            }
        }

        /// <summary>
        /// 将游戏物体回收至池中
        /// </summary>
        /// <param name="go"></param>
        public void UnSpawn(GameObject go)
        {
            if (go == null) { return; }
            SubPoolMag pool = null;
            foreach (var subpool in subpoolList.Values)
            {
                if (subpool.Contains(go))
                {
                    pool = subpool;
                    break;
                }
            }
            pool?.UnSpawn(go);
        }

        /// <summary>
        /// 回收所有的游戏物体至池中
        /// </summary>
        public void UnSpawnAll()
        {
            foreach (var subpool in subpoolList.Values)
            {
                subpool.UnSpawnAll();
            }
        }

        /// <summary>
        /// 除了XXX，回收所有的游戏物体至池中
        /// </summary>
        /// <param name="except"></param>
        public void UnSpawnAll(List<GameObject> except)
        {
            foreach (var subpool in subpoolList.Values)
            {
                subpool.UnSpawnAll();
            }
        }

        /// <summary>
        /// 回收某一Prefab的所有游戏物体至池中
        /// </summary>
        /// <param name="prefabResourcesPath"></param>
        public void UnSpawnAll(string prefabResourcesPath)
        {
            if (string.IsNullOrWhiteSpace(prefabResourcesPath)) { return; }
            if (!subpoolList.ContainsKey(prefabResourcesPath.Trim())) { return; }
            if (subpoolList.TryGetValue(prefabResourcesPath.Trim(), out SubPoolMag subpool))
            {
                subpool.UnSpawnAll();
            }
        }

        /// <summary>
        /// 除了XXX，回收某一Prefab的所有游戏物体至池中
        /// </summary>
        /// <param name="prefabResourcesPath"></param>
        /// <param name="except"></param>
        public void UnSpawnAll(string prefabResourcesPath, List<GameObject> except)
        {
            if (string.IsNullOrWhiteSpace(prefabResourcesPath)) { return; }
            if (!subpoolList.ContainsKey(prefabResourcesPath.Trim())) { return; }
            if (subpoolList.TryGetValue(prefabResourcesPath.Trim(), out SubPoolMag subpool))
            {
                subpool.UnSpawnAll(except);
            }
        }

        /// <summary>
        /// 注册新的池子
        /// </summary>
        /// <param name="prefabResourcesPath">Prefab在Resources文件夹下的路径</param>
        /// <param name="parentTransform"></param>
        private void RegieterNewSubPool(string prefabResourcesPath, Transform parentTransform)
        {
            GameObject prefabGO = Resources.Load<GameObject>(prefabResourcesPath.Trim());
            if (prefabGO == null)
            {
                throw new System.Exception("prefabResourcesPath错误，无法加载相应的Prefab");
            }
            SubPoolMag subpool = new SubPoolMag(parentTransform, prefabGO);
            subpoolList.Add(prefabResourcesPath.Trim(), subpool);
        }

        /// <summary>
        /// 清理池中所有物体，但不会对物体进行任何操作，常用与场景切换时
        /// </summary>
        public void Clear()
        {
            foreach (KeyValuePair<string, SubPoolMag> dict in subpoolList)
            {
                dict.Value.Clear();
            }
            subpoolList.Clear();
        }
    }
}