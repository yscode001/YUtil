// Author：yaoshuai
// Email：yscode@126.com
// Date：2025-9-9
// ------------------------------

using System;

namespace YCSharp
{
    public class Bool3Observable
    {
        #region 存储值声明、事件声明、添加监听、移除监听
        private Bool3 _value = Bool3.NotDetermined;
        private event Action<Bool3> Event_ValueChanged1;
        private event Action Event_ValueChanged2;

        public void AddListener1(Action<Bool3> action, bool immediateTrigger = false)
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
        public void RemoveListener1(Action<Bool3> action)
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
        public Bool3 Value
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
        private Bool3Observable() { }
        private Bool3Observable(Bool3 initValue)
        {
            _value = initValue;
        }
        #endregion

        #region 静态构造函数
        public static Bool3Observable Create()
        {
            return new Bool3Observable();
        }
        public static Bool3Observable Create(Bool3 initValue)
        {
            return new Bool3Observable(initValue);
        }
        public static Bool3Observable Create(Bool3 initValue, Action<Bool3> immediateTrigger)
        {
            Bool3Observable obs = new Bool3Observable(initValue);
            if (immediateTrigger != null)
            {
                obs.Event_ValueChanged1 += immediateTrigger;
                obs.Event_ValueChanged1?.Invoke(obs._value);
            }
            return obs;
        }
        public static Bool3Observable Create(Bool3 initValue, Action immediateTrigger)
        {
            Bool3Observable obs = new Bool3Observable(initValue);
            if (immediateTrigger != null)
            {
                obs.Event_ValueChanged2 += immediateTrigger;
                obs.Event_ValueChanged2?.Invoke();
            }
            return obs;
        }
        public static Bool3Observable Create(Bool3 initValue, Action<Bool3> immediateTrigger1, Action immediateTrigger2)
        {
            Bool3Observable obs = new Bool3Observable(initValue);
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