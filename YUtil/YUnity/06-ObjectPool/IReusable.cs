using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 定义协议，需要回收的Object需遵守此协议
    /// </summary>
    public interface IReusable
    {
        /// <summary>
        /// 当游戏物体从池中取出或放入池中时，执行的一些操作(如初始化)
        /// </summary>
        /// <param name="isSpawn">是否是Spawn</param>
        void OnSpawnOrUnSpawn(bool isSpawn);

        /// <summary>
        /// 调用对象池，将游戏物体进行回收
        /// </summary>
        /// <param name="delaySeconds">延迟回收秒数</param>
        Coroutine UnSpawnFromObjectPool(float delaySeconds = 0);

        /// <summary>
        /// 调用对象池，将游戏物体进行释放
        /// </summary>
        /// <param name="delaySeconds">延迟释放秒数</param>
        /// <param name="immediage">到时间后是否立即释放</param>
        Coroutine ReleaseFromObjectPool(float delaySeconds = 0, bool immediage = false);
    }
}