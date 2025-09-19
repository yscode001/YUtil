// Author：yaoshuai
// Email：yscode@126.com
// Date：2025-9-9
// ------------------------------

using System;

namespace YCSharp
{
    public class LongObservable
    {
        #region 存储值声明、事件声明、添加监听、移除监听
        private long _value = 0;
        private event Action<long> Event_ValueChanged1;
        private event Action Event_ValueChanged2;

        public void AddListener1(Action<long> action, bool immediateTrigger = false)
        {
            if (action != null)
            {
                Event_ValueChanged1 += action;
                if (immediateTrigger)
                {
                    action?.Invoke(_value);
                }
            }
        }
        public void AddListener2(Action action, bool immediateTrigger = false)
        {
            if (action != null)
            {
                Event_ValueChanged2 += action;
                if (immediateTrigger)
                {
                    action?.Invoke();
                }
            }
        }
        public void RemoveListener1(Action<long> action)
        {
            if (action != null)
            {
                Event_ValueChanged1 -= action;
            }
        }
        public void RemoveListener2(Action action)
        {
            if (action != null)
            {
                Event_ValueChanged2 -= action;
            }
        }
        #endregion

        #region 属性声明及其改变触发事件
        public long Value
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
        private LongObservable() { }
        private LongObservable(long initValue)
        {
            _value = initValue;
        }
        #endregion

        #region 静态构造函数
        public static LongObservable Create()
        {
            return new LongObservable();
        }
        public static LongObservable Create(long initValue)
        {
            return new LongObservable(initValue);
        }
        public static LongObservable Create(long initValue, Action<long> immediateTrigger)
        {
            LongObservable obs = new LongObservable(initValue);
            if (immediateTrigger != null)
            {
                obs.Event_ValueChanged1 += immediateTrigger;
                obs.Event_ValueChanged1?.Invoke(obs._value);
            }
            return obs;
        }
        public static LongObservable Create(long initValue, Action immediateTrigger)
        {
            LongObservable obs = new LongObservable(initValue);
            if (immediateTrigger != null)
            {
                obs.Event_ValueChanged2 += immediateTrigger;
                obs.Event_ValueChanged2?.Invoke();
            }
            return obs;
        }
        public static LongObservable Create(long initValue, Action<long> immediateTrigger1, Action immediateTrigger2)
        {
            LongObservable obs = new LongObservable(initValue);
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