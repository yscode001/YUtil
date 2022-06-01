using System;
using UnityEngine;

namespace YFramework
{
    [Serializable]
    public class CameraAdaptConfig
    {
        public float orthographicSize;
    }
    public class CameraAdapt : AdaptBase<CameraAdaptConfig, Camera>
    {
        protected override void ApplyConfig(CameraAdaptConfig config)
        {
            base.ApplyConfig(config);
            MComponent.orthographicSize = config.orthographicSize;
        }

        protected override void CopyProperty(CameraAdaptConfig config)
        {
            base.CopyProperty(config);
            config.orthographicSize = MComponent.orthographicSize;
        }
    }
}