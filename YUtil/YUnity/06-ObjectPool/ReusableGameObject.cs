using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 定义抽象类，需要回收的Object需继承此抽象类
    /// </summary>
    public class ReusableGameObject : MonoBehaviourBaseY, IReusable
    {
        /// <summary>
        /// 当游戏物体从池中取出或放入池中时，执行的一些操作(如初始化)
        /// </summary>
        /// <param name="isSpawn">是否是spawn</param>
        public virtual void OnSpawnOrUnSpawn(bool isSpawn) { }

        private Coroutine UnSpawnCoroutine;

        /// <summary>
        /// 调用对象池，将游戏物体进行回收
        /// </summary>
        /// <param name="delaySeconds">延迟回收秒数</param>
        public Coroutine UnSpawnFromObjectPool(float delaySeconds = 0)
        {
            if (UnSpawnCoroutine != null)
            {
                StopCoroutine(UnSpawnCoroutine);
            }
            UnSpawnCoroutine = DoAfterDelay(delaySeconds, () =>
            {
                ObjectPool.UnSpawn(GameObjectY);
            });
            return UnSpawnCoroutine;
        }

        private Coroutine ReleaseCoroutine;

        /// <summary>
        /// 调用对象池，将游戏物体进行释放
        /// </summary>
        /// <param name="delaySeconds">延迟释放秒数</param>
        /// <param name="immediage">到时间后是否立即释放</param>
        public Coroutine ReleaseFromObjectPool(float delaySeconds = 0, bool immediage = false)
        {
            if (ReleaseCoroutine != null)
            {
                StopCoroutine(ReleaseCoroutine);
            }
            ReleaseCoroutine = DoAfterDelay(delaySeconds, () =>
            {
                ObjectPool.Release(GameObjectY, immediage);
            });
            return ReleaseCoroutine;
        }
    }
}