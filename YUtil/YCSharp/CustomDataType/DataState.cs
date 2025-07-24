// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-6-15
// ------------------------------
using System;

namespace YCSharp
{
    /// <summary>
    /// 数据状态
    /// </summary>
    public class DataState
    {
        #region 事件声明
        public event Action<int> Event_IntValueChanged1;
        public event Action Event_IntValueChanged2;

        public event Action<string> Event_StringValueChanged1;
        public event Action Event_StringValueChanged2;

        public event Action<bool> Event_BoolValueChanged1;
        public event Action Event_BoolValueChanged2;

        public event Action<object> Event_ObjectValueChanged1;
        public event Action Event_ObjectValueChanged2;
        #endregion

        #region 存储值声明
        private int _intValue = 0;
        private string _stringValue = null;
        private bool _boolValue = false;
        private object _objectValue = null;
        #endregion

        #region 属性声明及其改变触发事件
        public int IntValue
        {
            get => _intValue;
            set
            {
                if (_intValue != value)
                {
                    _intValue = value;
                    Event_IntValueChanged1?.Invoke(_intValue);
                    Event_IntValueChanged2?.Invoke();
                }
            }
        }
        public string StringValue
        {
            get => _stringValue;
            set
            {
                if (_stringValue != value)
                {
                    _stringValue = value;
                    Event_StringValueChanged1?.Invoke(_stringValue);
                    Event_StringValueChanged2?.Invoke();
                }
            }
        }
        public bool BoolValue
        {
            get => _boolValue;
            set
            {
                if (_boolValue != value)
                {
                    _boolValue = value;
                    Event_BoolValueChanged1?.Invoke(_boolValue);
                    Event_BoolValueChanged2?.Invoke();
                }
            }
        }
        public object ObjectValue
        {
            get => _objectValue;
            set
            {
                if (_objectValue != value)
                {
                    _objectValue = value;
                    Event_ObjectValueChanged1?.Invoke(_objectValue);
                    Event_ObjectValueChanged2?.Invoke();
                }
            }
        }
        #endregion

        #region 强制触发事件
        public void ForceTriggerEvent_IntValueChanged()
        {
            Event_IntValueChanged1?.Invoke(_intValue);
            Event_IntValueChanged2?.Invoke();
        }
        public void ForceTriggerEvent_StringValueChanged()
        {
            Event_StringValueChanged1?.Invoke(_stringValue);
            Event_StringValueChanged2?.Invoke();
        }
        public void ForceTriggerEvent_BoolValueChanged()
        {
            Event_BoolValueChanged1?.Invoke(_boolValue);
            Event_BoolValueChanged2?.Invoke();
        }
        public void ForceTriggerEvent_ObjectValueChanged()
        {
            Event_ObjectValueChanged1?.Invoke(_objectValue);
            Event_ObjectValueChanged2?.Invoke();
        }
        #endregion

        #region 私有化构造函数
        private DataState() { }
        private DataState(int initValue)
        {
            _intValue = initValue;
        }
        private DataState(string initValue)
        {
            _stringValue = initValue;
        }
        private DataState(bool initValue)
        {
            _boolValue = initValue;
        }
        private DataState(object initValue)
        {
            _objectValue = initValue;
        }
        #endregion

        #region 静态构造函数
        public static DataState CreateInt(int initValue)
        {
            return new DataState(initValue);
        }
        public static DataState CreateInt(int initValue, Action<int> immediateTrigger1)
        {
            DataState state = new DataState(initValue);
            if (immediateTrigger1 != null)
            {
                state.Event_IntValueChanged1 += immediateTrigger1;
                state.Event_IntValueChanged1?.Invoke(state._intValue);
            }
            return state;
        }
        public static DataState CreateInt(int initValue, Action immediateTrigger2)
        {
            DataState state = new DataState(initValue);
            if (immediateTrigger2 != null)
            {
                state.Event_IntValueChanged2 += immediateTrigger2;
                state.Event_IntValueChanged2?.Invoke();
            }
            return state;
        }
        public static DataState CreateInt(int initValue, Action<int> immediateTrigger1, Action immediateTrigger2)
        {
            DataState state = new DataState(initValue);
            if (immediateTrigger1 != null)
            {
                state.Event_IntValueChanged1 += immediateTrigger1;
                state.Event_IntValueChanged1?.Invoke(state._intValue);
            }
            if (immediateTrigger2 != null)
            {
                state.Event_IntValueChanged2 += immediateTrigger2;
                state.Event_IntValueChanged2?.Invoke();
            }
            return state;
        }

        public static DataState CreateString(string initValue)
        {
            return new DataState(initValue);
        }
        public static DataState CreateString(string initValue, Action<string> immediateTrigger1)
        {
            DataState state = new DataState(initValue);
            if (immediateTrigger1 != null)
            {
                state.Event_StringValueChanged1 += immediateTrigger1;
                state.Event_StringValueChanged1?.Invoke(state._stringValue);
            }
            return state;
        }
        public static DataState CreateString(string initValue, Action immediateTrigger2)
        {
            DataState state = new DataState(initValue);
            if (immediateTrigger2 != null)
            {
                state.Event_StringValueChanged2 += immediateTrigger2;
                state.Event_StringValueChanged2?.Invoke();
            }
            return state;
        }
        public static DataState CreateString(string initValue, Action<string> immediateTrigger1, Action immediateTrigger2)
        {
            DataState state = new DataState(initValue);
            if (immediateTrigger1 != null)
            {
                state.Event_StringValueChanged1 += immediateTrigger1;
                state.Event_StringValueChanged1?.Invoke(state._stringValue);
            }
            if (immediateTrigger2 != null)
            {
                state.Event_StringValueChanged2 += immediateTrigger2;
                state.Event_StringValueChanged2?.Invoke();
            }
            return state;
        }

        public static DataState CreateBool(bool initValue)
        {
            return new DataState(initValue);
        }
        public static DataState CreateBool(bool initValue, Action<bool> immediateTrigger1)
        {
            DataState state = new DataState(initValue);
            if (immediateTrigger1 != null)
            {
                state.Event_BoolValueChanged1 += immediateTrigger1;
                state.Event_BoolValueChanged1?.Invoke(state._boolValue);
            }
            return state;
        }
        public static DataState CreateBool(bool initValue, Action immediateTrigger2)
        {
            DataState state = new DataState(initValue);
            if (immediateTrigger2 != null)
            {
                state.Event_BoolValueChanged2 += immediateTrigger2;
                state.Event_BoolValueChanged2?.Invoke();
            }
            return state;
        }
        public static DataState CreateBool(bool initValue, Action<bool> immediateTrigger1, Action immediateTrigger2)
        {
            DataState state = new DataState(initValue);
            if (immediateTrigger1 != null)
            {
                state.Event_BoolValueChanged1 += immediateTrigger1;
                state.Event_BoolValueChanged1?.Invoke(state._boolValue);
            }
            if (immediateTrigger2 != null)
            {
                state.Event_BoolValueChanged2 += immediateTrigger2;
                state.Event_BoolValueChanged2?.Invoke();
            }
            return state;
        }

        public static DataState CreateObject(object initValue)
        {
            return new DataState(initValue);
        }
        public static DataState CreateObject(object initValue, Action<object> immediateTrigger1)
        {
            DataState state = new DataState(initValue);
            if (immediateTrigger1 != null)
            {
                state.Event_ObjectValueChanged1 += immediateTrigger1;
                state.Event_ObjectValueChanged1?.Invoke(state._objectValue);
            }
            return state;
        }
        public static DataState CreateObject(object initValue, Action immediateTrigger2)
        {
            DataState state = new DataState(initValue);
            if (immediateTrigger2 != null)
            {
                state.Event_ObjectValueChanged2 += immediateTrigger2;
                state.Event_ObjectValueChanged2?.Invoke();
            }
            return state;
        }
        public static DataState CreateObject(object initValue, Action<object> immediateTrigger1, Action immediateTrigger2)
        {
            DataState state = new DataState(initValue);
            if (immediateTrigger1 != null)
            {
                state.Event_ObjectValueChanged1 += immediateTrigger1;
                state.Event_ObjectValueChanged1?.Invoke(state._objectValue);
            }
            if (immediateTrigger2 != null)
            {
                state.Event_ObjectValueChanged2 += immediateTrigger2;
                state.Event_ObjectValueChanged2?.Invoke();
            }
            return state;
        }
        #endregion
    }
}