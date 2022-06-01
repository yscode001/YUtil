using System;
using UnityEngine;

namespace YFramework
{
    [Serializable]
    public class TransformAdaptConfig
    {
        public bool updateApply;
        public bool isActive;
        public Vector3 localPosition;
        public Vector3 localEulerAngles;
        public Vector3 localScale;
    }

    public class TransformAdapt : AdaptBase<TransformAdaptConfig, Transform>
    {
        protected override void ApplyConfig(TransformAdaptConfig config)
        {
            base.ApplyConfig(config);
            gameObject.SetActive(config.isActive);
            MComponent.localPosition = config.localPosition;
            MComponent.localEulerAngles = config.localEulerAngles;
            MComponent.localScale = config.localScale;
        }

        protected override void CopyProperty(TransformAdaptConfig config)
        {
            base.CopyProperty(config);
            config.isActive = gameObject.activeSelf;
            config.localPosition = MComponent.localPosition;
            config.localEulerAngles = MComponent.localEulerAngles;
            config.localScale = MComponent.localScale;
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            if (CurrentConfig.updateApply)
            {
                ApplyConfig(CurrentConfig);
            }
        }
    }
}