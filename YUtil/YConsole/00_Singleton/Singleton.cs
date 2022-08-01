// Author：yaoshuai
// Email：yscode@126.com
// Date：2021-12-21
// ------------------------------

namespace YConsole
{
    public abstract class Singleton<T> where T : new()
    {
        private static T _instance;

        /// <summary>
        /// 单例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init() { }

        /// <summary>
        /// 反初始化
        /// </summary>
        public virtual void UnInit() { }

        /// <summary>
        /// 驱动
        /// </summary>
        public virtual void Tick() { }
    }
}