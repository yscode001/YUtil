using System;
using UnityEngine;

namespace YFramework
{
    [Serializable]
    public class RectTransformAdaptConfig
    {
        [Header("横竖屏切换时，是否启用下面的isActive属性")]
        public bool useIsActive = true;

        [Header("横竖屏切换时控制物体的激活状态")]
        public bool isActive = true;

        [Header("下面是具体的布局属性")]
        public Vector2 anchorMin;
        public Vector2 anchorMax;
        public Vector2 pivot;
        public Vector2 sizeDelta;
        public Vector3 localScale;
        public Vector3 localPosition;
        public Vector3 anchoredPosition;
        public Vector3 localEulerAngles;
    }

    public class RectTransformAdapt : AdaptBase<RectTransformAdaptConfig, RectTransform>
    {
        protected override void ApplyConfig(RectTransformAdaptConfig config)
        {
            base.ApplyConfig(config);
            if (config.useIsActive)
            {
                gameObject.SetActive(config.isActive);
            }
            MComponent.anchorMin = config.anchorMin;
            MComponent.anchorMax = config.anchorMax;
            MComponent.pivot = config.pivot;
            MComponent.sizeDelta = config.sizeDelta;
            MComponent.localScale = config.localScale;
            MComponent.localPosition = config.localPosition;
            MComponent.anchoredPosition = config.anchoredPosition;
            MComponent.localEulerAngles = config.localEulerAngles;
        }

        protected override void CopyProperty(RectTransformAdaptConfig config)
        {
            base.CopyProperty(config);
            config.isActive = gameObject.activeSelf;
            config.anchorMin = MComponent.anchorMin;
            config.anchorMax = MComponent.anchorMax;
            config.pivot = MComponent.pivot;
            config.sizeDelta = MComponent.sizeDelta;
            config.localScale = MComponent.localScale;
            config.localPosition = MComponent.localPosition;
            config.anchoredPosition = MComponent.anchoredPosition;
            config.localEulerAngles = MComponent.localEulerAngles;
        }
    }
}