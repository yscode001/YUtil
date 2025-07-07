// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-6-15
// ------------------------------
using System;

namespace YCSharp
{
    /// <summary>
    /// 单选状态
    /// </summary>
    public class RadioState
    {
        #region 构造函数
        public RadioState() { }

        public RadioState(int initValue, Action<int> immediateTrigger = null)
        {
            _currentIntValue = initValue;
            if (immediateTrigger != null)
            {
                Event_CurrentIntValueChanged += immediateTrigger;
                Event_CurrentIntValueChanged?.Invoke(_currentIntValue);
            }
        }

        public RadioState(float initValue, Action<float> immediateTrigger = null)
        {
            _currentFloatValue = initValue;
            if (immediateTrigger != null)
            {
                Event_CurrentFloatValueChanged += immediateTrigger;
                Event_CurrentFloatValueChanged?.Invoke(_currentFloatValue);
            }
        }

        public RadioState(string initValue, Action<string> immediateTrigger = null)
        {
            if (!string.IsNullOrWhiteSpace(initValue))
            {
                _currentStringValue = initValue;
            }
            if (immediateTrigger != null)
            {
                Event_CurrentStringValueChanged += immediateTrigger;
                Event_CurrentStringValueChanged?.Invoke(_currentStringValue);
            }
        }

        public RadioState(bool initValue, Action<bool> immediateTrigger = null)
        {
            _currentBoolValue = initValue;
            if (immediateTrigger != null)
            {
                Event_CurrentBoolValueChanged += immediateTrigger;
                Event_CurrentBoolValueChanged?.Invoke(_currentBoolValue);
            }
        }

        public RadioState(object initValue, Action<object> immediateTrigger = null)
        {
            if (initValue != null)
            {
                _currentObjectValue = initValue;
            }
            if (immediateTrigger != null)
            {
                Event_CurrentObjectValueChanged += immediateTrigger;
                Event_CurrentObjectValueChanged?.Invoke(_currentObjectValue);
            }
        }
        #endregion

        #region 事件声明
        public event Action<int> Event_CurrentIntValueChanged;

        public event Action<float> Event_CurrentFloatValueChanged;

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

        private float _currentFloatValue = 0;
        public float CurrentFloatValue
        {
            get => _currentFloatValue;
            set
            {
                if (_currentFloatValue == value) { return; }
                _currentFloatValue = value;
                Event_CurrentFloatValueChanged?.Invoke(_currentFloatValue);
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