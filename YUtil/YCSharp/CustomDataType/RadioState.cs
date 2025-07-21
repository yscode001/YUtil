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
        public RadioState(int initValue, Action<int> immediateTrigger = null)
        {
            _intValue = initValue;
            if (immediateTrigger != null)
            {
                Event_IntValueChanged += immediateTrigger;
                Event_IntValueChanged?.Invoke(_intValue);
            }
        }

        public RadioState(string initValue, Action<string> immediateTrigger = null)
        {
            if (!string.IsNullOrWhiteSpace(initValue))
            {
                _stringValue = initValue;
            }
            if (immediateTrigger != null)
            {
                Event_StringValueChanged += immediateTrigger;
                Event_StringValueChanged?.Invoke(_stringValue);
            }
        }

        public RadioState(bool initValue, Action<bool> immediateTrigger = null)
        {
            _boolValue = initValue;
            if (immediateTrigger != null)
            {
                Event_BoolValueChanged += immediateTrigger;
                Event_BoolValueChanged?.Invoke(_boolValue);
            }
        }

        public RadioState(object initValue, Action<object> immediateTrigger = null)
        {
            if (initValue != null)
            {
                _objectValue = initValue;
            }
            if (immediateTrigger != null)
            {
                Event_ObjectValueChanged += immediateTrigger;
                Event_ObjectValueChanged?.Invoke(_objectValue);
            }
        }
        #endregion

        #region 事件声明
        public event Action<int> Event_IntValueChanged;

        public event Action<string> Event_StringValueChanged;

        public event Action<bool> Event_BoolValueChanged;

        public event Action<object> Event_ObjectValueChanged;
        #endregion

        #region 当前值的读与写，与触发事件
        private int _intValue = 0;
        public int IntValue
        {
            get => _intValue;
            set
            {
                if (_intValue == value) { return; }
                _intValue = value;
                Event_IntValueChanged?.Invoke(_intValue);
            }
        }

        private string _stringValue = "";
        public string StringValue
        {
            get => _stringValue;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || _stringValue == value) { return; }
                _stringValue = value;
                Event_StringValueChanged?.Invoke(_stringValue);
            }
        }

        private bool _boolValue = false;
        public bool BoolValue
        {
            get => _boolValue;
            set
            {
                if (_boolValue == value) { return; }
                _boolValue = value;
                Event_BoolValueChanged?.Invoke(_boolValue);
            }
        }

        private object _objectValue = null;
        public object ObjectValue
        {
            get => _objectValue;
            set
            {
                if (value == null || _objectValue == value) { return; }
                _objectValue = value;
                Event_ObjectValueChanged?.Invoke(_objectValue);
            }
        }
        #endregion

        #region 强制触发事件
        public void ForceTriggerEvent_IntValueChanged()
        {
            Event_IntValueChanged?.Invoke(_intValue);
        }
        public void ForceTriggerEvent_StringValueChanged()
        {
            Event_StringValueChanged?.Invoke(_stringValue);
        }
        public void ForceTriggerEvent_BoolValueChanged()
        {
            Event_BoolValueChanged?.Invoke(_boolValue);
        }
        public void ForceTriggerEvent_ObjectValueChanged()
        {
            Event_ObjectValueChanged?.Invoke(_objectValue);
        }
        #endregion
    }
}