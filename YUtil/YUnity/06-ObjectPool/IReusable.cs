namespace YUnity
{
    /// <summary>
    /// 定义协议，需要回收的Object需遵守此协议
    /// </summary>
    public interface IReusable
    {
        /// <summary>
        /// 当游戏物体从池中取出时，进行的一些操作(如初始化)
        /// </summary>
        void OnSpawn();

        /// <summary>
        /// 当游戏物体被放入池中时，进行的一些操作(如回收释放)
        /// </summary>
        void OnUnSpawn();

        /// <summary>
        /// 调用对象池，将游戏物体进行回收
        /// </summary>
        void UnSpawnFromObjectPool();

        /// <summary>
        /// 调用对象池，将游戏物体进行释放
        /// </summary>
        /// <param name="immediage">是否立即释放</param>
        void ReleaseFromObjectPool(bool immediage = false);
    }
}