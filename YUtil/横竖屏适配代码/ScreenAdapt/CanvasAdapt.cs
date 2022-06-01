using System;
using UnityEngine;
using UnityEngine.UI;

namespace YFramework
{
    [Serializable]
    public class CanvasAdaptConfig
    {
        public Vector2 referenceResolution;
        public float matchWidthOrHeight;
    }

    public class CanvasAdapt : AdaptBase<CanvasAdaptConfig, CanvasScaler>
    {
        protected override void ApplyConfig(CanvasAdaptConfig config)
        {
            base.ApplyConfig(config);
            MComponent.referenceResolution = config.referenceResolution;
            MComponent.matchWidthOrHeight = config.matchWidthOrHeight;
        }

        protected override void CopyProperty(CanvasAdaptConfig config)
        {
            base.CopyProperty(config);
            config.matchWidthOrHeight = MComponent.matchWidthOrHeight;
            config.referenceResolution = MComponent.referenceResolution;
        }
    }
}