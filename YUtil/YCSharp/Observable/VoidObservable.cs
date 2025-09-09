// Author：yaoshuai
// Email：yscode@126.com
// Date：2025-9-9
// ------------------------------

using System;

namespace YCSharp
{
    public class VoidObservable
    {
        #region 事件声明
        public event Action Event_ValueChanged;
        #endregion

        #region 强制触发事件
        public void ForceTriggerValueChangeEvent()
        {
            Event_ValueChanged?.Invoke();
        }
        #endregion

        #region 私有化构造函数
        private VoidObservable() { }
        #endregion

        #region 静态构造函数
        public static VoidObservable Create()
        {
            return new VoidObservable();
        }
        public static VoidObservable Create(Action immediateTrigger)
        {
            VoidObservable obs = new VoidObservable();
            if (immediateTrigger != null)
            {
                obs.Event_ValueChanged += immediateTrigger;
                obs.Event_ValueChanged?.Invoke();
            }
            return obs;
        }
        #endregion
    }
}