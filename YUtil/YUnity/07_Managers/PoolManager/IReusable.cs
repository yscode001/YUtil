namespace YUnity
{
    /// <summary>
    /// 定义协议，需要回收的Object需遵守此协议
    /// </summary>
    public interface IReusable
    {
        /// <summary>
        /// 从池中取出，进行一些初始化操作
        /// </summary>
        void OnSpawn();

        /// <summary>
        /// 放入池中，进行一些回收释放操作
        /// </summary>
        void OnUnSpawn();
    }
}