using System;
using System.Reflection;

namespace YUnity
{
    /// <summary>
    /// 单例基本(不继承MonoBehaviour)
    /// </summary>
    public abstract class BaseSingleton<T> where T : BaseSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) { return _instance; }
                var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                if (ctor == null)
                {
                    throw new Exception("Non-public ctor() not found");
                }
                _instance = ctor.Invoke(null) as T;
                return _instance;
            }
        }

        protected BaseSingleton() { }
    }
}