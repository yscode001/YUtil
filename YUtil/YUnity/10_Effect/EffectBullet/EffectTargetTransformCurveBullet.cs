using System;
using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 目标游戏物体追踪曲线弹道子弹
    /// </summary>
    public partial class EffectTargetTransformCurveBullet : MonoBehaviourBaseY
    {
        /// <summary>
        /// 目标Target
        /// </summary>
        private Transform TargetTransform = null;

        /// <summary>
        /// 当距目标小于等于这个距离时，就算达到
        /// </summary>
        private float LimitReachDis = 1;

        /// <summary>
        /// 飞行过程中目标被销毁了(如被其他玩家干掉了，不会再执行reachedTargetComplete)
        /// </summary>
        private Action TargetDestroyWhenFlying = null;

        /// <summary>
        /// 达到目标位置后的回调
        /// </summary>
        private Action ReachedTargetComplete = null;

        /// <summary>
        /// 子弹速度
        /// </summary>
        private float MoveSpeed = 5;

        /// <summary>
        /// 子弹是否正在飞
        /// </summary>
        private bool IsMoving = false;
    }
    public partial class EffectTargetTransformCurveBullet
    {
        /// <summary>
        /// 开始飞行
        /// </summary>
        /// <param name="isUseCurveDir">是否使用弹道曲线，false则表示使用直线</param>
        /// <param name="curveDir">弹道曲线，zero表示随机弹道曲线(仅在isUseCurveDir为true时有意义)</param>
        /// <param name="curveRandomSeed">随机弹道方向种子，仅在curveDir为zero时有意义</param>
        /// <param name="targetTransform">目标Target</param>
        /// <param name="startPos">开始位置，zero表示使用当前位置</param>
        /// <param name="moveSpeed">子弹速度</param>
        /// <param name="limitReachDis">当距目标小于等于这个距离时，就算达到</param>
        /// <param name="targetDestroyWhenFlying">飞行过程中目标被销毁了(如被其他玩家干掉了，不会再执行reachedTargetComplete)</param>
        /// <param name="reachedTargetComplete">达到目标位置后的回调</param>
        public void Play(bool isUseCurveDir, Vector3 curveDir, int curveRandomSeed, Transform targetTransform, Vector3 startPos, float moveSpeed, float limitReachDis, Action targetDestroyWhenFlying, Action reachedTargetComplete)
        {
            IsMoving = false;
            if (targetTransform == null || !targetTransform.gameObject.activeSelf ||
                moveSpeed <= 0 ||
                limitReachDis < 0 ||
                (isUseCurveDir && curveDir == Vector3.zero && curveRandomSeed <= 0))
            {
                // 设置的数据不对，啥也不做
            }
            else
            {
                TargetTransform = targetTransform;
                LimitReachDis = limitReachDis;
                TargetDestroyWhenFlying = targetDestroyWhenFlying;
                ReachedTargetComplete = reachedTargetComplete;
                MoveSpeed = moveSpeed;
                /***/
                if (startPos != Vector3.zero)
                {
                    // 重置开始位置
                    TransformY.Translate(startPos - TransformY.position, Space.World);
                }
                /***/
                curdir = Vector3.zero;
                if (isUseCurveDir)
                {
                    if (curveDir == Vector3.zero)
                    {
                        // 随机曲线弹道
                        System.Random ran = new System.Random(curveRandomSeed);
                        Vector3 v1 = Vector3.Cross(targetTransform.position - TransformY.position, Vector3.up).normalized;
                        v1 *= ran.Next(-100, 100);
                        Vector3 v2 = Vector3.up * ran.Next(0, 100);
                        curdir = (v1 + v2).normalized * 0.3f;
                    }
                    else
                    {
                        // 指定曲线弹道
                        curdir = curveDir.normalized * 0.3f;
                    }
                }
                /***/
                IsMoving = true;
            }
        }
    }
    public partial class EffectTargetTransformCurveBullet
    {
        /// <summary>
        /// 弹道曲线，辅助属性
        /// </summary>
        private Vector3 curdir = Vector3.zero;

        private void Clear()
        {
            IsMoving = false;

            curdir = Vector3.zero;

            TargetTransform = null;
            LimitReachDis = 1;
            TargetDestroyWhenFlying = null;
            ReachedTargetComplete = null;
            MoveSpeed = 5;
        }

        private void Update()
        {
            // 子弹未发射
            if (IsMoving == false)
            {
                return;
            }
            // 目标被别人提前干掉了
            if (TargetTransform == null || !TargetTransform.gameObject.activeSelf)
            {
                TargetDestroyWhenFlying?.Invoke();
                Clear();
                return;
            }
            // 子弹到达了目标
            if (Vector3.Distance(TargetTransform.position, TransformY.position) <= LimitReachDis)
            {
                ReachedTargetComplete?.Invoke();
                Clear();
                return;
            }
            // 子弹正在飞行
            Vector3 tdir = (TargetTransform.position - TransformY.position).normalized;
            if (curdir != Vector3.zero)
            {
                tdir = (tdir + curdir).normalized;
            }
            TransformY.Translate(MoveSpeed * Time.deltaTime * tdir);
        }
    }
}