﻿using System;
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
        /// 当距目标小于等于这个距离时，就算达到
        /// </summary>
        private float LimitReachDis = 0;

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
            MoveSpeed = LimitReachDis = 0;
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
        /// <param name="limitReachDis">当距目标小于等于这个距离时，就算达到</param>
        /// <param name="targetDeathWhenFlying">飞行过程中目标死亡了(如被其他玩家干掉了，不会再执行ReachedComplete)</param>
        /// <param name="reachedComplete">达到目标位置后的回调</param>
        public void BeginFlying(Transform targetTransform, float moveSpeed, float limitReachDis, Action targetDeathWhenFlying, Action reachedComplete)
        {
            Clear();
            if (targetTransform == null || !targetTransform.gameObject.activeSelf || moveSpeed <= 0 || limitReachDis < 0)
            {
                // 设置的数据不对，啥也不做，直接返回
                return;
            }
            TargetTransform = targetTransform;
            MoveSpeed = moveSpeed;
            LimitReachDis = limitReachDis;
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
            if (Vector3.Distance(targetPosition, TransformY.position) <= LimitReachDis)
            {
                IsMoving = false;
                ReachedComplete?.Invoke();
                Clear();
                return;
            }
            TransformY.Translate(MoveSpeed * Time.deltaTime * (targetPosition - TransformY.position).normalized);
        }
    }
}