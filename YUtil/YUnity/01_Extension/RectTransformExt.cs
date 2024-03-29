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
        /// 获取rect在rectBox中的位置
        /// </summary>
        /// <param name="rect">需要获取位置的rect</param>
        /// <param name="selfReferencePoint">自己的参考点位置</param>
        /// <param name="rectBoxRT">rectBox</param>
        /// <param name="rectBoxReferencePoint">rect以rectBox的那个参考点位置做为参考</param>
        /// <param name="cam"></param>
        /// <returns></returns>
        public static Vector2 GetPosInRectBox(this RectTransform rect, RectTransformPosEnum selfReferencePoint, RectTransform rectBoxRT, RectTransformPosEnum rectBoxReferencePoint, Camera cam)
        {
            if (rect == null || rectBoxRT == null) { return Vector2.zero; }
            Vector3[] _corners = new Vector3[4];
            rect.GetWorldCorners(_corners); //获得对象的四个角坐标

            Vector3 selfWorldPos = Vector3.zero; // 自己的世界坐标
            switch (selfReferencePoint)
            {
                case RectTransformPosEnum.LeftBottom:
                    selfWorldPos = _corners[0];
                    break;
                case RectTransformPosEnum.LeftCenter:
                    float x1 = _corners[0].x;
                    float y1 = _corners[0].y + ((_corners[1].y - _corners[0].y) / 2f);
                    selfWorldPos = new Vector3(x1, y1, 0);
                    break;
                case RectTransformPosEnum.LeftTop:
                    selfWorldPos = _corners[1];
                    break;
                case RectTransformPosEnum.TopCenter:
                    float x2 = _corners[0].x + ((_corners[3].x - _corners[0].x) / 2f);
                    float y2 = _corners[1].y;
                    selfWorldPos = new Vector3(x2, y2, 0);
                    break;
                case RectTransformPosEnum.RightTop:
                    selfWorldPos = _corners[2];
                    break;
                case RectTransformPosEnum.RightCenter:
                    float x3 = _corners[3].x;
                    float y3 = _corners[0].y + ((_corners[1].y - _corners[0].y) / 2f);
                    selfWorldPos = new Vector3(x3, y3, 0);
                    break;
                case RectTransformPosEnum.RightBottom:
                    selfWorldPos = _corners[3];
                    break;
                case RectTransformPosEnum.BottomCenter:
                    float x4 = _corners[0].x + ((_corners[3].x - _corners[0].x) / 2f);
                    float y4 = _corners[0].y;
                    selfWorldPos = new Vector3(x4, y4, 0);
                    break;
                case RectTransformPosEnum.Center:
                    float x = _corners[0].x + ((_corners[3].x - _corners[0].x) / 2f);
                    float y = _corners[0].y + ((_corners[1].y - _corners[0].y) / 2f);
                    selfWorldPos = new Vector3(x, y, 0);
                    break;
                default: break;
            }

            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(cam, selfWorldPos);
            Vector2 pos;
            // 默认以rectBoxRT的中心点为参考点
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectBoxRT, screenPos, cam, out pos);
            float boxWHalf = rectBoxRT.rect.width * 0.5f;
            float boxHHalf = rectBoxRT.rect.height * 0.5f;
            switch (rectBoxReferencePoint)
            {
                case RectTransformPosEnum.LeftBottom:
                    pos.x += boxWHalf;
                    pos.y += boxHHalf;
                    break;
                case RectTransformPosEnum.LeftCenter:
                    pos.x += boxWHalf;
                    break;
                case RectTransformPosEnum.LeftTop:
                    pos.x += boxWHalf;
                    pos.y -= boxHHalf;
                    break;
                case RectTransformPosEnum.TopCenter:
                    pos.y -= boxHHalf;
                    break;
                case RectTransformPosEnum.RightTop:
                    pos.x -= boxWHalf;
                    pos.y -= boxHHalf;
                    break;
                case RectTransformPosEnum.RightCenter:
                    pos.x -= boxWHalf;
                    break;
                case RectTransformPosEnum.RightBottom:
                    pos.x -= boxWHalf;
                    pos.y += boxHHalf;
                    break;
                case RectTransformPosEnum.BottomCenter:
                    pos.y += boxHHalf;
                    break;
                case RectTransformPosEnum.Center:
                    break;
                default:
                    break;
            }
            return pos;
        }

        /// <summary>
        /// 获取尺寸
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Vector2 GetSize(this RectTransform rect)
        {
            return rect.rect.size;
        }

        /// <summary>
        /// 获取宽度
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static float GetWidth(this RectTransform rect)
        {
            return rect.rect.width;
        }

        /// <summary>
        /// 获取高度
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static float GetHeight(this RectTransform rect)
        {
            return rect.rect.height;
        }

        /// <summary>
        /// 获取父物体组件
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static RectTransform GetParent(this RectTransform rect)
        {
            return rect.parent as RectTransform;
        }

        // 直接修改
        public static void ChangeRectTransformSizeDeltaX(this RectTransform rt, float x)
        {
            Vector2 v2 = rt.sizeDelta;
            v2.x = x;
            rt.sizeDelta = v2;
        }

        public static void ChangeRectTransformSizeDeltaY(this RectTransform rt, float y)
        {
            Vector2 v2 = rt.sizeDelta;
            v2.y = y;
            rt.sizeDelta = v2;
        }

        public static void ChangeRectTransformSizeDeltaXY(this RectTransform rt, float x, float y)
        {
            Vector2 v2 = rt.sizeDelta;
            v2.x = x;
            v2.y = y;
            rt.sizeDelta = v2;
        }

        // 通过组件修改
        public static void ChangeRectTransformSizeDeltaX(this Component component, float x)
        {
            RectTransform rt = component.GetComponent<RectTransform>();
            if (rt == null) { return; }
            rt.ChangeRectTransformSizeDeltaX(x);
        }

        public static void ChangeRectTransformSizeDeltaY(this Component component, float y)
        {
            RectTransform rt = component.GetComponent<RectTransform>();
            if (rt == null) { return; }
            rt.ChangeRectTransformSizeDeltaY(y);
        }

        public static void ChangeRectTransformSizeDeltaXY(this Component component, float x, float y)
        {
            RectTransform rt = component.GetComponent<RectTransform>();
            if (rt == null) { return; }
            rt.ChangeRectTransformSizeDeltaXY(x, y);
        }

        // 通过游戏物体修改
        public static void ChangeRectTransformSizeDeltaX(this GameObject go, float x)
        {
            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt == null) { return; }
            rt.ChangeRectTransformSizeDeltaX(x);
        }

        public static void ChangeRectTransformSizeDeltaY(this GameObject go, float y)
        {
            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt == null) { return; }
            rt.ChangeRectTransformSizeDeltaY(y);
        }

        public static void ChangeRectTransformSizeDeltaXY(this GameObject go, float x, float y)
        {
            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt == null) { return; }
            rt.ChangeRectTransformSizeDeltaXY(x, y);
        }
    }
}