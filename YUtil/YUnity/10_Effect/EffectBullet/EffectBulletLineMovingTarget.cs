using System;
using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 子弹直线飞行追踪移动目标
    /// </summary>
    public partial class EffectBulletLineMovingTarget : MonoBehaviourBaseY
    {
        /// <summary>
        /// 移动目标
        /// </summary>
        private Transform TargetTransform;

        /// <summary>
        /// 子弹速度
        /// </summary>
        private float MoveSpeed = 0;

        /// <summary>
        /// 最大误差距离
        /// </summary>
        private float MaxErrorDistance = 0;

        /// <summary>
        /// 飞行过程中目标死亡了(如被其他玩家干掉了，不会再执行ReachedComplete)
        /// </summary>
        private Action TargetDeathWhenFlying = null;

        /// <summary>
        /// 达到目标位置后的回调
        /// </summary>
        private Action ReachedComplete = null;

        // 下面是辅助属性
        private bool IsMoving = false; // 是否正在移动

        private void Clear()
        {
            IsMoving = false;

            TargetTransform = null;
            MoveSpeed = MaxErrorDistance = 0;
            TargetDeathWhenFlying = ReachedComplete = null;
        }
    }
    public partial class EffectBulletLineMovingTarget
    {
        /// <summary>
        /// 子弹开始飞行
        /// </summary>
        /// <param name="targetTransform">移动目标</param>
        /// <param name="moveSpeed">飞行速度</param>
        /// <param name="maxErrorDistance">最大误差距离</param>
        /// <param name="targetDeathWhenFlying">飞行过程中目标死亡了(如被其他玩家干掉了，不会再执行ReachedComplete)</param>
        /// <param name="reachedComplete">达到目标位置后的回调</param>
        public void BeginFlying(Transform targetTransform, float moveSpeed, float maxErrorDistance, Action targetDeathWhenFlying, Action reachedComplete)
        {
            Clear();
            if (targetTransform == null || !targetTransform.gameObject.activeSelf || moveSpeed <= 0 || maxErrorDistance < 0)
            {
                // 设置的数据不对，啥也不做，直接返回
                return;
            }
            TargetTransform = targetTransform;
            MoveSpeed = moveSpeed;
            MaxErrorDistance = maxErrorDistance;
            TargetDeathWhenFlying = targetDeathWhenFlying;
            ReachedComplete = reachedComplete;

            IsMoving = true; // 开始飞行
        }
    }
    public partial class EffectBulletLineMovingTarget
    {
        private void Update()
        {
            if (IsMoving == false)
            {
                return;
            }
            if (TargetTransform == null || !TargetTransform.gameObject.activeSelf)
            {
                IsMoving = false;
                TargetDeathWhenFlying?.Invoke();
                Clear();
                return;
            }
            Vector3 targetPosition = TargetTransform.position;
            float distance = Vector3.Distance(targetPosition, TransformY.position);
            if (distance <= MaxErrorDistance)
            {
                TransformY.position = targetPosition;
                IsMoving = false;
                ReachedComplete?.Invoke();
                Clear();
                return;
            }
            Vector3 willMove = MoveSpeed * Time.deltaTime * (targetPosition - TransformY.position).normalized;
            if (Vector3.Distance(TransformY.position, TransformY.position + willMove) > distance)
            {
                TransformY.position = targetPosition;
                IsMoving = false;
                ReachedComplete?.Invoke();
                Clear();
                return;
            }
            TransformY.Translate(willMove, Space.World);
        }
    }
}