// Author：yaoshuai
// Email：yscode@126.com
// Date：2025-9-9
// ------------------------------

using System;

namespace YCSharp
{
    public class BoolObservable
    {
        #region 事件声明
        public event Action<bool> Event_ValueChanged1;
        public event Action Event_ValueChanged2;
        #endregion

        #region 存储值声明
        private bool _value = false;
        #endregion

        #region 属性声明及其改变触发事件
        public bool Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    Event_ValueChanged1?.Invoke(_value);
                    Event_ValueChanged2?.Invoke();
                }
            }
        }
        #endregion

        #region 强制触发事件
        public void ForceTriggerValueChangeEvent()
        {
            Event_ValueChanged1?.Invoke(_value);
            Event_ValueChanged2?.Invoke();
        }
        #endregion

        #region 私有化构造函数
        private BoolObservable() { }
        private BoolObservable(bool initValue)
        {
            _value = initValue;
        }
        #endregion

        #region 静态构造函数
        public static BoolObservable Create()
        {
            return new BoolObservable();
        }
        public static BoolObservable Create(bool initValue)
        {
            return new BoolObservable(initValue);
        }
        public static BoolObservable Create(bool initValue, Action<bool> immediateTrigger)
        {
            BoolObservable obs = new BoolObservable(initValue);
            if (immediateTrigger != null)
            {
                obs.Event_ValueChanged1 += immediateTrigger;
                obs.Event_ValueChanged1?.Invoke(obs._value);
            }
            return obs;
        }
        public static BoolObservable Create(bool initValue, Action immediateTrigger)
        {
            BoolObservable obs = new BoolObservable(initValue);
            if (immediateTrigger != null)
            {
                obs.Event_ValueChanged2 += immediateTrigger;
                obs.Event_ValueChanged2?.Invoke();
            }
            return obs;
        }
        public static BoolObservable Create(bool initValue, Action<bool> immediateTrigger1, Action immediateTrigger2)
        {
            BoolObservable obs = new BoolObservable(initValue);
            if (immediateTrigger1 != null)
            {
                obs.Event_ValueChanged1 += immediateTrigger1;
                obs.Event_ValueChanged1?.Invoke(obs._value);
            }
            if (immediateTrigger2 != null)
            {
                obs.Event_ValueChanged2 += immediateTrigger2;
                obs.Event_ValueChanged2?.Invoke();
            }
            return obs;
        }
        #endregion
    }
}