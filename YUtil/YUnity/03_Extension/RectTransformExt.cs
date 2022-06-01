using UnityEngine;

namespace YUnity
{
    public static class RectTransformExt
    {
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