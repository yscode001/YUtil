// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-6-15
// ------------------------------
using System;

namespace YCSharp
{
    /// <summary>
    /// 单选状态(常用于ListView)
    /// </summary>
    public class RadioSelectedState
    {
        #region 构造函数
        public RadioSelectedState() { }
        public RadioSelectedState(int initValue)
        {
            _currentIntValue = initValue;
        }
        public RadioSelectedState(string initValue)
        {
            if (!string.IsNullOrWhiteSpace(initValue))
            {
                _currentStringValue = initValue;
            }
        }
        public RadioSelectedState(bool initValue)
        {
            _currentBoolValue = initValue;
        }
        public RadioSelectedState(object initValue)
        {
            if (initValue != null)
            {
                _currentObjectValue = initValue;
            }
        }
        #endregion

        #region 事件声明
        public event Action<int> Event_CurrentIntValueChanged;

        public event Action<string> Event_CurrentStringValueChanged;

        public event Action<bool> Event_CurrentBoolValueChanged;

        public event Action<object> Event_CurrentObjectValueChanged;
        #endregion

        #region 当前值的读与写
        private int _currentIntValue = 0;
        public int CurrentIntValue
        {
            get => _currentIntValue;
            set
            {
                if (_currentIntValue == value) { return; }
                _currentIntValue = value;
                Event_CurrentIntValueChanged?.Invoke(_currentIntValue);
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
                Event_CurrentStringValueChanged?.Invoke(_currentStringValue);
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
                Event_CurrentBoolValueChanged?.Invoke(_currentBoolValue);
            }
        }

        private object _currentObjectValue = null;
        public object CurrentObjectValue
        {
            get => _currentObjectValue;
            set
            {
                if (value == null || _currentObjectValue == value) { return; }
                _currentObjectValue = value;
                Event_CurrentObjectValueChanged?.Invoke(_currentObjectValue);
            }
        }
        #endregion
    }
}