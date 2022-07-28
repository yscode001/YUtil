// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-7-28
// ------------------------------

using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 截图工具
    /// </summary>
    public class ScreenshotUtil
    {
        /// <summary>
        /// 针对指定相机进行截屏
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Texture2D CaptureScreen(Camera camera, Rect rect)
        {
            if (camera == null || rect.width <= 0 || rect.height <= 0) { return null; }
            RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 0);
            camera.targetTexture = rt;
            camera.Render();

            RenderTexture.active = rt;
            Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);

            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();

            camera.targetTexture = null;
            RenderTexture.active = null;
            GameObject.Destroy(rt);
            return screenShot;
        }

        /// <summary>
        /// 自定义截图(包括UI)
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Texture2D CaptureScreen(Rect rect)
        {
            if (rect.width <= 0 || rect.height <= 0) { return null; }
            Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();
            return screenShot;
        }

        /// <summary>
        /// 截取全屏
        /// </summary>
        /// <param name="fullFilePath">图片保存的完整路径(包括后缀名)</param>
        /// <returns></returns>
        public static bool CapruerScreen(string fullFilePath)
        {
            if (string.IsNullOrWhiteSpace(fullFilePath)) { return false; }
            ScreenCapture.CaptureScreenshot(fullFilePath);
            return true;
        }
    }
}