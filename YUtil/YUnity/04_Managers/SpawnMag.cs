using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 游戏物体生成器
    /// </summary>
    public class SpawnMag
    {
        private SpawnMag() { }
        private static T GetPrefab<T>(string prefabResourcesPath) where T : Object
        {
            if (string.IsNullOrWhiteSpace(prefabResourcesPath)) { return null; }
            return Resources.Load<T>(prefabResourcesPath);
        }

        public static T Spawn<T>(string prefabResourcesPath) where T : Object
        {
            T prefab = GetPrefab<T>(prefabResourcesPath);
            if (prefab == null) { return null; };
            return Object.Instantiate(prefab);
        }

        public static T Spawn<T>(string prefabResourcesPath, Transform parent) where T : Object
        {
            T prefab = GetPrefab<T>(prefabResourcesPath);
            if (prefab == null) { return null; };
            return Object.Instantiate(prefab, parent);
        }

        public static T Spawn<T>(string prefabResourcesPath, Transform parent, bool worldPositionStays) where T : Object
        {
            T prefab = GetPrefab<T>(prefabResourcesPath);
            if (prefab == null) { return null; };
            return Object.Instantiate(prefab, parent, worldPositionStays);
        }

        public static T Spawn<T>(string prefabResourcesPath, Vector3 position, Quaternion rotation) where T : Object
        {
            T prefab = GetPrefab<T>(prefabResourcesPath);
            if (prefab == null) { return null; };
            return Object.Instantiate(prefab, position, rotation);
        }

        public static T Spawn<T>(string prefabResourcesPath, Vector3 position, Quaternion rotation, Transform parent) where T : Object
        {
            T prefab = GetPrefab<T>(prefabResourcesPath);
            if (prefab == null) { return null; };
            return Object.Instantiate(prefab, position, rotation, parent);
        }
    }
}