namespace YUnity
{
    /// <summary>
    /// 定义抽象类，需要回收的Object需继承此抽象类
    /// </summary>
    public abstract class ReusableObject : MonoBehaviourBaseY, IReusable
    {
        /// <summary>
        /// 从池中取出，进行一些初始化操作
        /// </summary>
        public abstract void OnSpawn();

        /// <summary>
        /// 放入池中，进行一些回收释放操作
        /// </summary>
        public abstract void OnUnSpawn();
    }
}