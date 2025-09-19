// Author：yaoshuai
// Email：yscode@126.com
// Date：2025-9-9
// ------------------------------

using System;

namespace YCSharp
{
    public class StringObservable
    {
        #region 存储值声明、事件声明、添加监听、移除监听
        private string _value = null;
        private event Action<string> Event_ValueChanged1;
        private event Action Event_ValueChanged2;

        public void AddListener1(Action<string> action, bool immediateTrigger = false)
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
        public void RemoveListener1(Action<string> action)
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
        public string Value
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
        private StringObservable() { }
        private StringObservable(string initValue)
        {
            _value = initValue;
        }
        #endregion

        #region 静态构造函数
        public static StringObservable Create()
        {
            return new StringObservable();
        }
        public static StringObservable Create(string initValue)
        {
            return new StringObservable(initValue);
        }
        public static StringObservable Create(string initValue, Action<string> immediateTrigger)
        {
            StringObservable obs = new StringObservable(initValue);
            if (immediateTrigger != null)
            {
                obs.Event_ValueChanged1 += immediateTrigger;
                obs.Event_ValueChanged1?.Invoke(obs._value);
            }
            return obs;
        }
        public static StringObservable Create(string initValue, Action immediateTrigger)
        {
            StringObservable obs = new StringObservable(initValue);
            if (immediateTrigger != null)
            {
                obs.Event_ValueChanged2 += immediateTrigger;
                obs.Event_ValueChanged2?.Invoke();
            }
            return obs;
        }
        public static StringObservable Create(string initValue, Action<string> immediateTrigger1, Action immediateTrigger2)
        {
            StringObservable obs = new StringObservable(initValue);
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