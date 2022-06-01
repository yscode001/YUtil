using System;
using UnityEngine;
using UnityEngine.UI;

namespace YFramework
{
    [Serializable]
    public class GridLayoutGroupAdaptConfig
    {
        public RectOffset padding;
        public Vector2 cellSize;
        public Vector2 spacing;
        public GridLayoutGroup.Corner startCorner;
        public GridLayoutGroup.Axis startAxis;
        public TextAnchor childAlignment;
        public GridLayoutGroup.Constraint constraint;
        public int constraintCount;
    }

    public class GridLayoutGroupAdapt : AdaptBase<GridLayoutGroupAdaptConfig, GridLayoutGroup>
    {
        protected override void ApplyConfig(GridLayoutGroupAdaptConfig config)
        {
            base.ApplyConfig(config);
            MComponent.padding = config.padding;
            MComponent.cellSize = config.cellSize;
            MComponent.spacing = config.spacing;
            MComponent.startCorner = config.startCorner;
            MComponent.startAxis = config.startAxis;
            MComponent.childAlignment = config.childAlignment;
            MComponent.constraint = config.constraint;
            MComponent.constraintCount = config.constraintCount;
        }

        protected override void CopyProperty(GridLayoutGroupAdaptConfig config)
        {
            base.CopyProperty(config);
            config.padding = MComponent.padding;
            config.cellSize = MComponent.cellSize;
            config.spacing = MComponent.spacing;
            config.startCorner = MComponent.startCorner;
            config.startAxis = MComponent.startAxis;
            config.childAlignment = MComponent.childAlignment;
            config.constraint = MComponent.constraint;
            config.constraintCount = MComponent.constraintCount;
        }
    }
}