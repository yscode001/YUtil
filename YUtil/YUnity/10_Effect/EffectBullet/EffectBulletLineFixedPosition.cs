﻿using System;
using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 子弹直线飞行追踪固定位置
    /// </summary>
    public partial class EffectBulletLineFixedPosition : MonoBehaviourBaseY
    {
        /// <summary>
        /// 目标位置
        /// </summary>
        private Vector3 TargetPosition;

        /// <summary>
        /// 子弹速度
        /// </summary>
        private float MoveSpeed = 0;

        /// <summary>
        /// 最大误差距离
        /// </summary>
        private float MaxErrorDistance = 0;

        /// <summary>
        /// 达到目标位置后的回调
        /// </summary>
        private Action ReachedComplete = null;

        // 下面是辅助属性
        private bool IsMoving = false; // 是否正在移动

        private void Clear()
        {
            IsMoving = false;

            TargetPosition = Vector3.zero;
            MoveSpeed = MaxErrorDistance = 0;
            ReachedComplete = null;
        }
    }
    public partial class EffectBulletLineFixedPosition
    {
        /// <summary>
        /// 子弹开始飞行
        /// </summary>
        /// <param name="targetPosition">目标位置</param>
        /// <param name="moveSpeed">飞行速度</param>
        /// <param name="maxErrorDistance">最大误差距离</param>
        /// <param name="reachedComplete">达到目标位置后的回调</param>
        public void BeginFlying(Vector3 targetPosition, float moveSpeed, float maxErrorDistance, Action reachedComplete)
        {
            Clear();
            if (moveSpeed <= 0 || maxErrorDistance < 0)
            {
                // 设置的数据不对，啥也不做，直接返回
                return;
            }
            TargetPosition = targetPosition;
            MoveSpeed = moveSpeed;
            MaxErrorDistance = maxErrorDistance;
            ReachedComplete = reachedComplete;

            IsMoving = true; // 开始飞行
        }
    }
    public partial class EffectBulletLineFixedPosition
    {
        private void Update()
        {
            if (IsMoving == false)
            {
                return;
            }
            float distance = Vector3.Distance(TargetPosition, TransformY.position);
            if (distance <= MaxErrorDistance)
            {
                TransformY.position = TargetPosition;
                IsMoving = false;
                ReachedComplete?.Invoke();
                Clear();
                return;
            }
            Vector3 willMove = MoveSpeed * Time.deltaTime * (TargetPosition - TransformY.position).normalized;
            if (Vector3.Distance(TransformY.position, TransformY.position + willMove) > distance)
            {
                TransformY.position = TargetPosition;
                IsMoving = false;
                ReachedComplete?.Invoke();
                Clear();
                return;
            }
            TransformY.Translate(willMove, Space.World);
        }
    }
}