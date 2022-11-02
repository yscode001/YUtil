using UnityEngine;

namespace YUnity
{
    public static class Vector3Ext
    {
        /// <summary>
        /// 先执行WorldToViewportPoint，再判断是否在相机的视口坐标范围内
        /// </summary>
        /// <param name="v3"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool IsInCamera_After_WorldToViewportPoint(this Vector3 v3, Camera camera)
        {
            Vector3 wtvp = camera.WorldToViewportPoint(v3);
            return wtvp.x >= 0 && wtvp.x <= 1 && wtvp.y >= 0 && wtvp.y <= 1 && wtvp.z >= camera.nearClipPlane && wtvp.z <= camera.farClipPlane;
        }

        /// <summary>
        /// 先执行ScreenToViewportPoint，再判断是否在相机的视口坐标范围内
        /// </summary>
        /// <param name="v3"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool IsInCamera_After_ScreenToViewportPoint(this Vector3 v3, Camera camera)
        {
            Vector3 stvp = camera.ScreenToViewportPoint(v3);
            return stvp.x >= 0 && stvp.x <= 1 && stvp.y >= 0 && stvp.y <= 1 && stvp.z >= camera.nearClipPlane && stvp.z <= camera.farClipPlane;
        }

        /// <summary>
        /// 当前V3已经是转换后的ViewportPoint，判断其是否在相机的视口坐标范围内
        /// </summary>
        /// <param name="v3"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool IsInCamera_CurrentIsViewportPoint(this Vector3 v3, Camera camera)
        {
            return v3.x >= 0 && v3.x <= 1 && v3.y >= 0 && v3.y <= 1 && v3.z >= camera.nearClipPlane && v3.z <= camera.farClipPlane;
        }
    }
}