using System;
using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 目标位置追踪曲线弹道子弹
    /// </summary>
    public partial class EffectTargetPositionCurveBullet : MonoBehaviourBaseY
    {
        /// <summary>
        /// 目标位置(优先使用这个，zero表示使用TargetTransform)
        /// </summary>
        private Vector3 TargetPos = Vector3.zero;

        /// <summary>
        /// 当距目标小于等于这个距离时，就算达到
        /// </summary>
        private float LimitReachDis = 1;

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
    public partial class EffectTargetPositionCurveBullet
    {
        /// <summary>
        /// 开始飞行
        /// </summary>
        /// <param name="isUseCurveDir">是否使用弹道曲线，false则表示使用直线</param>
        /// <param name="curveDir">弹道曲线，zero表示随机弹道曲线(仅在isUseCurveDir为true时有意义)</param>
        /// <param name="curveRandomSeed">随机弹道方向种子，仅在curveDir为zero时有意义</param>
        /// <param name="targetPos">目标位置</param>
        /// <param name="startPos">开始位置，zero表示使用当前位置</param>
        /// <param name="moveSpeed">子弹速度</param>
        /// <param name="limitReachDis">当距目标小于等于这个距离时，就算达到</param>
        /// <param name="reachedTargetComplete">达到目标位置后的回调</param>
        public void Play(bool isUseCurveDir, Vector3 curveDir, int curveRandomSeed, Vector3 targetPos, Vector3 startPos, float moveSpeed, float limitReachDis, Action reachedTargetComplete)
        {
            IsMoving = false;
            if (moveSpeed <= 0 ||
                limitReachDis < 0 ||
                (isUseCurveDir && curveDir == Vector3.zero && curveRandomSeed <= 0))
            {
                // 设置的数据不对，啥也不做
            }
            else
            {
                TargetPos = targetPos;
                LimitReachDis = limitReachDis;
                ReachedTargetComplete = reachedTargetComplete;
                MoveSpeed = moveSpeed;
                /***/
                if (startPos != Vector3.zero)
                {
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
                        Vector3 v1 = Vector3.Cross(targetPos - TransformY.position, Vector3.up).normalized;
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
    public partial class EffectTargetPositionCurveBullet
    {
        /// <summary>
        /// 弹道曲线，辅助属性
        /// </summary>
        private Vector3 curdir = Vector3.zero;

        private void Clear()
        {
            IsMoving = false;

            curdir = Vector3.zero;

            TargetPos = Vector3.zero;
            LimitReachDis = 1;
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
            // 子弹到达了目标
            if (Vector3.Distance(TargetPos, TransformY.position) <= LimitReachDis)
            {
                ReachedTargetComplete?.Invoke();
                Clear();
                return;
            }
            // 子弹正在飞行
            Vector3 tdir = (TargetPos - TransformY.position).normalized;
            if (curdir != Vector3.zero)
            {
                tdir = (tdir + curdir).normalized;
            }
            TransformY.Translate(MoveSpeed * Time.deltaTime * tdir);
        }
    }
}