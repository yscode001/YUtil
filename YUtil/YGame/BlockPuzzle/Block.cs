// Author：yaoshuai
// Email：yscode@126.com
// Date：2024-5-24
// ------------------------------
using System;

namespace YGame.BlockPuzzle
{
    public partial class Block
    {
        public bool IsEnable { get; private set; } = true;
        public FillType FillType { get; private set; } = FillType.Empty;
        public FillState FillState { get; private set; } = FillState.Normal;

        public bool IsDisable => !IsEnable;
        public bool IsEmpty => FillType == FillType.Empty;
        public bool IsFilled => FillType != FillType.Empty;

        public event Action<FillType, FillState> Event_FillValueChanged;
    }
    public partial class Block
    {
        public void Init()
        {
            IsEnable = true;
            FillType = FillType.Empty;
            FillState = FillState.Normal;
        }
    }
    public partial class Block
    {
        public void SetupEnableState(bool enable)
        {
            IsEnable = enable;
        }
        public void SetupFillType(FillType fillType)
        {
            if (FillType != fillType)
            {
                FillType = fillType;

                if (FillType == FillType.Empty && FillState != FillState.Normal)
                {
                    FillState = FillState.Normal;
                }
                Event_FillValueChanged?.Invoke(FillType, FillState);
            }
        }
        public void SetupFillState(FillState fillState)
        {
            FillState newState = FillType == FillType.Empty ? FillState.Normal : fillState;
            if (FillState != newState)
            {
                FillState = newState;
                Event_FillValueChanged?.Invoke(FillType, FillState);
            }
        }
        public void SetupFillData(FillType fillType, FillState fillState)
        {
            bool isChanged = false;
            if (FillType != fillType)
            {
                FillType = fillType;
                isChanged = true;
            }
            FillState newState = FillType == FillType.Empty ? FillState.Normal : fillState;
            if (FillState != newState)
            {
                FillState = newState;
                isChanged = true;
            }
            if (isChanged)
            {
                Event_FillValueChanged?.Invoke(FillType, FillState);
            }
        }
    }
}