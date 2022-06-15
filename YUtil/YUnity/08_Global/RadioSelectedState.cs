// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-6-15
// ------------------------------
using System;

namespace YUnity
{
    /// <summary>
    /// 单选状态(常用于ListView)
    /// </summary>
    public class RadioSelectedState
    {
        public RadioSelectedState() { }

        #region 事件
        public event Action<int> Event_CurrentIntValueChanged;

        public event Action<string> Event_CurrentStringValueChanged;

        public event Action<bool> Event_CurrentBoolValueChanged;
        #endregion

        #region 当前值
        private int _currentIntValue = 0;
        public int CurrentIntValue
        {
            get => _currentIntValue;
            set
            {
                if (_currentIntValue == value) { return; }
                _currentIntValue = value;
                try
                {
                    Event_CurrentIntValueChanged?.Invoke(_currentIntValue);
                }
                catch (Exception ex)
                {
                    if (GameRootMag.Instance != null) // 说明初始化过了
                    {
                        this.Error($"RadioSelectedState Event_CurrentIntValueChanged Error：{ex}");
                    }
                }
            }
        }

        private string _currentStringValue = "";
        public string CurrentStringValue
        {
            get => _currentStringValue;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || _currentStringValue == value) { return; }
                _currentStringValue = value;
                try
                {
                    Event_CurrentStringValueChanged?.Invoke(_currentStringValue);
                }
                catch (Exception ex)
                {
                    if (GameRootMag.Instance != null) // 说明初始化过了
                    {
                        this.Error($"RadioSelectedState Event_CurrentStringValueChanged Error：{ex}");
                    }
                }
            }
        }

        private bool _currentBoolValue = false;
        public bool CurrentBoolValue
        {
            get => _currentBoolValue;
            set
            {
                if (_currentBoolValue == value) { return; }
                _currentBoolValue = value;
                try
                {
                    Event_CurrentBoolValueChanged?.Invoke(_currentBoolValue);
                }
                catch (Exception ex)
                {
                    if (GameRootMag.Instance != null) // 说明初始化过了
                    {
                        this.Error($"RadioSelectedState Event_CurrentBoolValueChanged Error：{ex}");
                    }
                }
            }
        }
        #endregion
    }
}