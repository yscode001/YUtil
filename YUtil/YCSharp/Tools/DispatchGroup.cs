using System;

namespace YCSharp
{
    public class DispatchGroup
    {
        public DispatchGroup() { }

        private readonly string counterLock = "counterLock";
        private int counter = 0;
        private Action notifyAction;

        /// <summary>
        /// 进入
        /// </summary>
        public void Enter()
        {
            lock (counterLock)
            {
                counter += 1;
            }
        }
        /// <summary>ni
        /// 离开
        /// </summary>
        public void Leave()
        {
            if (counter > 0)
            {
                lock (counterLock)
                {
                    counter -= 1;
                    if (counter == 0)
                    {
                        notifyAction?.Invoke();
                        notifyAction = null;
                    }
                }
            }
        }
        /// <summary>
        /// 通知执行
        /// </summary>
        /// <param name="action"></param>
        public void Notify(Action action)
        {
            notifyAction = action;
        }
        /// <summary>
        /// 清理
        /// </summary>
        public void Clear()
        {
            lock (counterLock)
            {
                counter = 0;
                notifyAction = null;
            }
        }
    }
}