using System;
using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 子弹曲线追踪固定位置
    /// </summary>
    public partial class EffectBulletCurveFixedPosition : MonoBehaviourBaseY
    {
        /// <summary>
        /// 目标位置
        /// </summary>
        private Vector3 TargetPos = Vector3.zero;

        /// <summary>
        /// 子弹速度
        /// </summary>
        private float MoveSpeed = 0;

        /// <summary>
        /// 当距目标小于等于这个距离时，就算达到
        /// </summary>
        private float LimitReachDis = 0;

        /// <summary>
        /// 达到目标位置后的回调
        /// </summary>
        private Action ReachedComplete = null;

        // 下面是辅助属性
        private bool IsMoving = false; // 是否正在移动
        private Vector3 CurveDir = Vector3.zero; // 曲线方向

        private void Clear()
        {
            IsMoving = false;

            TargetPos = Vector3.zero;
            MoveSpeed = LimitReachDis = 0;
            ReachedComplete = null;

            CurveDir = Vector3.zero;
        }
    }
    public partial class EffectBulletCurveFixedPosition
    {
        /// <summary>
        /// 开始飞行
        /// </summary>
        /// <param name="curveDir">曲线方向，zero表示随机曲线方向</param>
        /// <param name="curveRandomSeed">随机方向种子，仅在curveDir为zero时有意义</param>
        /// <param name="targetPos">目标位置</param>
        /// <param name="moveSpeed">飞行速度</param>
        /// <param name="limitReachDis">当距目标小于等于这个距离时，就算达到</param>
        /// <param name="reachedComplete">达到目标位置后的回调</param>
        public void Play(Vector3 curveDir, int curveRandomSeed, Vector3 targetPos, float moveSpeed, float limitReachDis, Action reachedComplete)
        {
            Clear();
            if (moveSpeed <= 0 ||
                limitReachDis < 0 ||
                (curveDir == Vector3.zero && curveRandomSeed <= 0))
            {
                // 设置的数据不对，啥也不做，直接返回
                return;
            }
            TargetPos = targetPos;
            MoveSpeed = moveSpeed;
            LimitReachDis = limitReachDis;
            ReachedComplete = reachedComplete;
            if (curveDir == Vector3.zero)
            {
                // 随机曲线弹道
                System.Random ran = new System.Random(curveRandomSeed);
                Vector3 v1 = Vector3.Cross(targetPos - TransformY.position, Vector3.up).normalized;
                v1 *= ran.Next(-100, 100);
                Vector3 v2 = Vector3.up * ran.Next(0, 100);
                // CurrentDir = (v1 + v2).normalized * 0.3f;
                CurveDir = (v1 + v2).normalized;
            }
            else
            {
                // 指定曲线弹道
                // CurrentDir = curveDir.normalized * 0.3f;
                CurveDir = curveDir;
            }
            IsMoving = true; // 开始飞行
        }
    }
    public partial class EffectBulletCurveFixedPosition
    {
        private void Update()
        {
            if (IsMoving == false)
            {
                return;
            }
            if (Vector3.Distance(TargetPos, TransformY.position) <= LimitReachDis)
            {
                IsMoving = false;
                ReachedComplete?.Invoke();
                Clear();
                return;
            }
            Vector3 dir = (TargetPos - TransformY.position).normalized;
            if (CurveDir != Vector3.zero)
            {
                dir = (dir + CurveDir).normalized;
            }
            TransformY.Translate(MoveSpeed * Time.deltaTime * dir);
        }
    }
}