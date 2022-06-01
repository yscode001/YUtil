using System;
using UnityEngine.UI;

namespace YFramework
{
    [Serializable]
    public class ScrollRectConfig
    {
        public bool vertical;
        public bool horizontal;
    }

    public class ScrollRectAdapt : AdaptBase<ScrollRectConfig, ScrollRect>
    {
        protected override void ApplyConfig(ScrollRectConfig Config)
        {
            base.ApplyConfig(Config);
            MComponent.vertical = Config.vertical;
            MComponent.horizontal = Config.horizontal;
        }

        protected override void CopyProperty(ScrollRectConfig config)
        {
            base.CopyProperty(config);
            config.vertical = MComponent.vertical;
            config.horizontal = MComponent.horizontal;
        }
    }
}