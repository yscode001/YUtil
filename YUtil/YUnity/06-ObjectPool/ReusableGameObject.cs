namespace YUnity
{
    /// <summary>
    /// 定义抽象类，需要回收的Object需继承此抽象类
    /// </summary>
    public abstract class ReusableGameObject : MonoBehaviourBaseY, IReusable
    {
        /// <summary>
        /// 当游戏物体从池中取出时，进行的一些操作(如初始化)
        /// </summary>
        public abstract void OnSpawn();

        /// <summary>
        /// 当游戏物体被放入池中时，进行的一些操作(如回收释放)
        /// </summary>
        public abstract void OnUnSpawn();

        /// <summary>
        /// 调用对象池，将游戏物体进行回收
        /// </summary>
        public void UnSpawnFromObjectPool()
        {
            ObjectPool.UnSpawn(GameObjectY);
        }

        /// <summary>
        /// 调用对象池，将游戏物体进行释放
        /// </summary>
        /// <param name="immediage">是否立即释放</param>
        public void ReleaseFromObjectPool(bool immediage = false)
        {
            ObjectPool.Release(GameObjectY, immediage);
        }
    }
}