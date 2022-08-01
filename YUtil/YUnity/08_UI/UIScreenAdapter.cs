// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-1
// ------------------------------
using UnityEngine;
using UnityEngine.UI;

namespace YUnity
{
    public enum UILayoutAdaptEnum
    {
        ScreenWidth,
        ScreenHeight,
        ScreenWidthAndHeight,
    }
    public enum UIFontAdaptEnum
    {
        ScreenWidth,
        ScreenHeight,
    }

    public class UIScreenAdapter : MonoBehaviour
    {
        private RectTransform rt;
        private bool adapted = false;

        private float AdaptWidthRate = 1;
        private float AdaptHeightRate = 1;
        private float FontAdaptRate = 1;

        [Header("适配规则：布局")]
        [SerializeField] private UILayoutAdaptEnum LayoutAdaptCondition = UILayoutAdaptEnum.ScreenHeight;

        [Header("适配规则：文字")]
        [SerializeField] private UIFontAdaptEnum FontAdaptCondition = UIFontAdaptEnum.ScreenHeight;

        [Header("适应于：布局")]
        [SerializeField] private bool AnchoredPosition = true;
        [SerializeField] private bool SizeDelta = true;
        [SerializeField] private bool OffsetMinMax = true;

        [Header("适用于：文字")]
        [SerializeField] private bool TextFontSize = true;

        private void Awake()
        {
            rt = gameObject.GetComponent<RectTransform>();
            if (rt == null) { return; }
            switch (LayoutAdaptCondition)
            {
                case UILayoutAdaptEnum.ScreenWidth:
                    AdaptWidthRate = AdaptHeightRate = ScreenCfg.WidthRate;
                    break;
                case UILayoutAdaptEnum.ScreenHeight:
                    AdaptWidthRate = AdaptHeightRate = ScreenCfg.HeightRate;
                    break;
                case UILayoutAdaptEnum.ScreenWidthAndHeight:
                    AdaptWidthRate = ScreenCfg.WidthRate;
                    AdaptHeightRate = ScreenCfg.HeightRate;
                    break;
            }
            switch (FontAdaptCondition)
            {
                case UIFontAdaptEnum.ScreenWidth:
                    FontAdaptRate = ScreenCfg.WidthRate;
                    break;
                case UIFontAdaptEnum.ScreenHeight:
                    FontAdaptRate = ScreenCfg.HeightRate;
                    break;
            }
        }

        private void OnEnable()
        {
            if (adapted || rt == null) { return; }
            if (LayoutAdaptCondition == UILayoutAdaptEnum.ScreenWidth && AdaptWidthRate == 1) { return; }
            if (LayoutAdaptCondition == UILayoutAdaptEnum.ScreenHeight && AdaptHeightRate == 1) { return; }
            if (LayoutAdaptCondition == UILayoutAdaptEnum.ScreenWidthAndHeight && AdaptWidthRate == 1 && AdaptHeightRate == 1) { return; }
            adapted = true;

            if (rt.anchorMin == rt.anchorMax)
            {
                // 锚点
                if (AnchoredPosition)
                {
                    Vector3 ap3 = rt.anchoredPosition3D;
                    ap3.x *= AdaptWidthRate;
                    ap3.y *= AdaptHeightRate;
                    rt.anchoredPosition3D = ap3;
                }
                if (SizeDelta)
                {
                    Vector2 s = rt.sizeDelta;
                    s.x *= AdaptWidthRate;
                    s.y *= AdaptHeightRate;
                    rt.sizeDelta = s;
                }
            }
            else if (rt.anchorMin.x != rt.anchorMax.x && rt.anchorMin.y != rt.anchorMax.y)
            {
                // 纯锚框
                if (OffsetMinMax)
                {
                    Vector2 min = rt.offsetMin;
                    Vector2 max = rt.offsetMax;
                    min.x *= AdaptWidthRate;
                    max.x *= AdaptWidthRate;
                    min.y *= AdaptHeightRate;
                    max.y *= AdaptHeightRate;
                    rt.offsetMin = min;
                    rt.offsetMax = max;
                }
            }
            else
            {
                // 半锚框
                // 锚点
                if (AnchoredPosition)
                {
                    Vector3 ap3 = rt.anchoredPosition3D;
                    ap3.x *= AdaptWidthRate;
                    ap3.y *= AdaptHeightRate;
                    rt.anchoredPosition3D = ap3;
                }
                // 锚框
                if (OffsetMinMax)
                {
                    Vector2 min = rt.offsetMin;
                    Vector2 max = rt.offsetMax;
                    min.x *= AdaptWidthRate;
                    max.x *= AdaptWidthRate;
                    min.y *= AdaptHeightRate;
                    max.y *= AdaptHeightRate;
                    rt.offsetMin = min;
                    rt.offsetMax = max;
                }
                // 尺寸
                if (SizeDelta)
                {
                    Vector2 s = rt.sizeDelta;
                    s.x *= AdaptWidthRate;
                    s.y *= AdaptHeightRate;
                    rt.sizeDelta = s;
                }
            }
            if (TextFontSize)
            {
                // 文字
                Text txt = gameObject.GetComponent<Text>();
                try
                {
                    // 这里无法做txt的为空判断，所以写个try catch
                    float siz = txt.fontSize * FontAdaptRate;
                    txt.fontSize = (int)siz;
                }
                catch { }
            }
        }
    }
}