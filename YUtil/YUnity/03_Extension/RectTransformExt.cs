using UnityEngine;

namespace YUnity
{
    public static class RectTransformExt
    {
        /// <summary>
        /// 获取在Canvas上的中心点
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="cam"></param>
        /// <param name="canvasRT"></param>
        /// <returns></returns>
        public static Vector2 GetCenterPosInCanvas(this RectTransform rect, Camera cam, RectTransform canvasRT)
        {
            if (rect == null || cam == null || canvasRT == null) { return Vector2.zero; }
            Vector3[] _corners = new Vector3[4];
            rect.GetWorldCorners(_corners); //获得对象的四个角坐标
            float x = _corners[0].x + ((_corners[3].x - _corners[0].x) / 2f);
            float y = _corners[0].y + ((_corners[1].y - _corners[0].y) / 2f);
            Vector3 centerWorld = new Vector3(x, y, 0);
            return centerWorld.WorldToCanvasPos(cam, canvasRT);
        }

        /// <summary>
        /// 获取尺寸
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Vector2 GetSize(this RectTransform rect)
        {
            if (rect == null) { return Vector2.zero; }
            return rect.rect.size;
        }

        /// <summary>
        /// 获取宽度
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static float GetWidth(this RectTransform rect)
        {
            if (rect == null) { return 0; }
            return rect.rect.width;
        }

        /// <summary>
        /// 获取高度
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static float GetHeight(this RectTransform rect)
        {
            if (rect == null) { return 0; }
            return rect.rect.height;
        }

        // 直接修改
        public static void ChangeRectTransformSizeDeltaX(this RectTransform rt, float x)
        {
            if (rt == null || x < 0) { return; }
            Vector2 v2 = rt.sizeDelta;
            v2.x = x;
            rt.sizeDelta = v2;
        }

        public static void ChangeRectTransformSizeDeltaY(this RectTransform rt, float y)
        {
            if (rt == null || y < 0) { return; }
            Vector2 v2 = rt.sizeDelta;
            v2.y = y;
            rt.sizeDelta = v2;
        }

        public static void ChangeRectTransformSizeDeltaXY(this RectTransform rt, float x, float y)
        {
            if (rt == null || x < 0 || y < 0) { return; }
            Vector2 v2 = rt.sizeDelta;
            v2.x = x;
            v2.y = y;
            rt.sizeDelta = v2;
        }

        // 通过组件修改
        public static void ChangeRectTransformSizeDeltaX(this Component component, float x)
        {
            if (component == null || x < 0) { return; }
            RectTransform rt = component.GetComponent<RectTransform>();
            if (rt == null) { return; }
            rt.ChangeRectTransformSizeDeltaX(x);
        }

        public static void ChangeRectTransformSizeDeltaY(this Component component, float y)
        {
            if (component == null || y < 0) { return; }
            RectTransform rt = component.GetComponent<RectTransform>();
            if (rt == null) { return; }
            rt.ChangeRectTransformSizeDeltaY(y);
        }

        public static void ChangeRectTransformSizeDeltaXY(this Component component, float x, float y)
        {
            if (component == null || x < 0 || y < 0) { return; }
            RectTransform rt = component.GetComponent<RectTransform>();
            if (rt == null) { return; }
            rt.ChangeRectTransformSizeDeltaXY(x, y);
        }

        // 通过游戏物体修改
        public static void ChangeRectTransformSizeDeltaX(this GameObject go, float x)
        {
            if (go == null || x < 0) { return; }
            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt == null) { return; }
            rt.ChangeRectTransformSizeDeltaX(x);
        }

        public static void ChangeRectTransformSizeDeltaY(this GameObject go, float y)
        {
            if (go == null || y < 0) { return; }
            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt == null) { return; }
            rt.ChangeRectTransformSizeDeltaY(y);
        }

        public static void ChangeRectTransformSizeDeltaXY(this GameObject go, float x, float y)
        {
            if (go == null || x < 0 || y < 0) { return; }
            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt == null) { return; }
            rt.ChangeRectTransformSizeDeltaXY(x, y);
        }
    }
}