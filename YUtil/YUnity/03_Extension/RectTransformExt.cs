using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// UI位置枚举
    /// </summary>
    public enum RectTransformPosEnum
    {
        /// <summary>
        /// 左下角
        /// </summary>
        LeftBottom,

        /// <summary>
        /// 左中角
        /// </summary>
        LeftCenter,

        /// <summary>
        /// 左上角
        /// </summary>
        LeftTop,

        /// <summary>
        /// 上中角
        /// </summary>
        TopCenter,

        /// <summary>
        /// 右上角
        /// </summary>
        RightTop,

        /// <summary>
        /// 右中角
        /// </summary>
        RightCenter,

        /// <summary>
        /// 右下角
        /// </summary>
        RightBottom,

        /// <summary>
        /// 下中角
        /// </summary>
        BottomCenter,

        /// <summary>
        /// 中心点
        /// </summary>
        Center,
    }

    public static class RectTransformExt
    {
        /// <summary>
        /// 获取在Canvas上的中心点，以Canvas左下角原点为参考点
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="rectPosEnum">rect的那个点</param>
        /// <param name="cam"></param>
        /// <param name="canvasRT">以Canvas左下角原点为参考点</param>
        /// <returns></returns>
        public static Vector2 GetCenterPosInCanvas(this RectTransform rect, RectTransformPosEnum rectPosEnum, Camera cam, RectTransform canvasRT)
        {
            if (rect == null || canvasRT == null) { return Vector2.zero; }
            Vector3[] _corners = new Vector3[4];
            rect.GetWorldCorners(_corners); //获得对象的四个角坐标

            Vector3 worldPos = Vector3.zero;
            switch (rectPosEnum)
            {
                case RectTransformPosEnum.LeftBottom:
                    worldPos = _corners[0];
                    break;
                case RectTransformPosEnum.LeftCenter:
                    float x1 = _corners[0].x;
                    float y1 = _corners[0].y + ((_corners[1].y - _corners[0].y) / 2f);
                    worldPos = new Vector3(x1, y1, 0);
                    break;
                case RectTransformPosEnum.LeftTop:
                    worldPos = _corners[1];
                    break;
                case RectTransformPosEnum.TopCenter:
                    float x2 = _corners[0].x + ((_corners[3].x - _corners[0].x) / 2f);
                    float y2 = _corners[1].y;
                    worldPos = new Vector3(x2, y2, 0);
                    break;
                case RectTransformPosEnum.RightTop:
                    worldPos = _corners[2];
                    break;
                case RectTransformPosEnum.RightCenter:
                    float x3 = _corners[3].x;
                    float y3 = _corners[0].y + ((_corners[1].y - _corners[0].y) / 2f);
                    worldPos = new Vector3(x3, y3, 0);
                    break;
                case RectTransformPosEnum.RightBottom:
                    worldPos = _corners[3];
                    break;
                case RectTransformPosEnum.BottomCenter:
                    float x4 = _corners[0].x + ((_corners[3].x - _corners[0].x) / 2f);
                    float y4 = _corners[0].y;
                    worldPos = new Vector3(x4, y4, 0);
                    break;
                case RectTransformPosEnum.Center:
                    float x = _corners[0].x + ((_corners[3].x - _corners[0].x) / 2f);
                    float y = _corners[0].y + ((_corners[1].y - _corners[0].y) / 2f);
                    worldPos = new Vector3(x, y, 0);
                    break;
            }
            return worldPos.WorldToCanvasPos(cam, canvasRT);
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