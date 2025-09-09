// Author：yaoshuai
// Email：yscode@126.com
// Date：2025-9-9
// ------------------------------

using System;

namespace YCSharp
{
    public class FloatObservable
    {
        #region 事件声明
        public event Action<float> Event_ValueChanged1;
        public event Action Event_ValueChanged2;
        #endregion

        #region 存储值声明
        private float _value = 0;
        #endregion

        #region 属性声明及其改变触发事件
        public float Value
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
        private FloatObservable() { }
        private FloatObservable(float initValue)
        {
            _value = initValue;
        }
        #endregion

        #region 静态构造函数
        public static FloatObservable Create()
        {
            return new FloatObservable();
        }
        public static FloatObservable Create(float initValue)
        {
            return new FloatObservable(initValue);
        }
        public static FloatObservable Create(float initValue, Action<float> immediateTrigger)
        {
            FloatObservable obs = new FloatObservable(initValue);
            if (immediateTrigger != null)
            {
                obs.Event_ValueChanged1 += immediateTrigger;
                obs.Event_ValueChanged1?.Invoke(obs._value);
            }
            return obs;
        }
        public static FloatObservable Create(float initValue, Action immediateTrigger)
        {
            FloatObservable obs = new FloatObservable(initValue);
            if (immediateTrigger != null)
            {
                obs.Event_ValueChanged2 += immediateTrigger;
                obs.Event_ValueChanged2?.Invoke();
            }
            return obs;
        }
        public static FloatObservable Create(float initValue, Action<float> immediateTrigger1, Action immediateTrigger2)
        {
            FloatObservable obs = new FloatObservable(initValue);
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