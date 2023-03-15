using System;
using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 目标追踪曲线弹道子弹
    /// </summary>
    public partial class EffectTargetCurveBullet : MonoBehaviour
    {
        /// <summary>
        /// 目标Target
        /// </summary>
        private Transform TargetTransform = null;

        /// <summary>
        /// 目标位置(优先使用这个，zero表示使用TargetTransform)
        /// </summary>
        private Vector3 TargetPos = Vector3.zero;

        /// <summary>
        /// 当距目标小于等于这个距离时，就算达到
        /// </summary>
        private float LimitReachDis = 0.2f;

        /// <summary>
        /// 飞行过程中目标被销毁了(如被其他玩家干掉了，不会在执行complete)
        /// </summary>
        private Action targetDestroyAction = null;

        /// <summary>
        /// 达到目标位置后的回调
        /// </summary>
        private Action complete = null;

        /// <summary>
        /// 子弹速度
        /// </summary>
        private float MoveSpeed = 5;

        /// <summary>
        /// 子弹是否正在飞
        /// </summary>
        private bool IsMoving = false;

        private Transform SelfT = null;
        private CharacterController CC = null;
    }
    public partial class EffectTargetCurveBullet
    {
        /// <summary>
        /// 开始飞行
        /// </summary>
        /// <param name="IsUseCurveDir">是否使用弹道曲线</param>
        /// <param name="CurveDir">弹道曲线，zero表示随机弹道曲线(仅在IsUseCurveDir为true时有意义)</param>
        /// <param name="CurveRandomSeed">随机弹道方向种子</param>
        /// <param name="TargetTransform">目标Target</param>
        /// <param name="TargetPos">目标位置(优先使用这个，zero表示使用TargetTransform)</param>
        /// <param name="StartPos">开始位置，zero表示使用当前位置</param>
        /// <param name="LimitReachDis">当距目标小于等于这个距离时，就算达到</param>
        /// <param name="targetDestroyAction">飞行过程中目标被销毁了(如被其他玩家干掉了，不会在执行complete)</param>
        /// <param name="complete">达到目标位置后的回调</param>
        /// <param name="MoveSpeed">子弹速度</param>
        public void Play(bool IsUseCurveDir, Vector3 CurveDir, int CurveRandomSeed, Transform TargetTransform, Vector3 TargetPos, Vector3 StartPos, float LimitReachDis, Action targetDestroyAction, Action complete, float MoveSpeed)
        {
            if ((TargetPos == Vector3.zero && TargetTransform == null) ||
                LimitReachDis < 0 ||
                MoveSpeed <= 0 ||
                (IsUseCurveDir && CurveDir == Vector3.zero && CurveRandomSeed <= 0))
            {
                IsMoving = false;
            }
            else
            {
                IsMoving = false;

                this.TargetTransform = TargetTransform;
                this.TargetPos = TargetPos;
                this.LimitReachDis = LimitReachDis;
                this.targetDestroyAction = targetDestroyAction;
                this.complete = complete;
                this.MoveSpeed = MoveSpeed;
                /***/
                CC = gameObject.GetComponent<CharacterController>();
                SelfT = transform;
                /***/
                if (StartPos != Vector3.zero)
                {
                    if (CC != null)
                    {
                        CC.Move(StartPos - SelfT.position);
                    }
                    else
                    {
                        SelfT.Translate(StartPos - SelfT.position, Space.World);
                    }
                }
                /***/
                curdir = Vector3.zero;
                if (IsUseCurveDir)
                {
                    if (CurveDir == Vector3.zero)
                    {
                        // 随机曲线弹道
                        System.Random ran = new System.Random(CurveRandomSeed);
                        Vector3 tpos = (TargetPos == Vector3.zero) ? TargetTransform.position : TargetPos;
                        Vector3 v1 = Vector3.Cross(tpos - SelfT.position, Vector3.up).normalized;
                        v1 *= ran.Next(-100, 100);
                        Vector3 v2 = Vector3.up * ran.Next(0, 100);
                        curdir = (v1 + v2).normalized * 0.3f;
                    }
                    else
                    {
                        // 指定曲线弹道
                        curdir = CurveDir.normalized * 0.3f;
                    }
                }
                /***/
                IsMoving = true;
            }
        }
    }
    public partial class EffectTargetCurveBullet
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
            TargetPos = Vector3.zero;
            LimitReachDis = 0.2f;
            targetDestroyAction = null;
            complete = null;
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
            if (TargetPos == Vector3.zero && TargetTransform == null)
            {
                targetDestroyAction?.Invoke();
                Clear();
                return;
            }
            // 子弹到达了目标
            Vector3 tpos = (TargetPos == Vector3.zero) ? TargetTransform.position : TargetPos;
            if (Vector3.Distance(tpos, SelfT.position) <= LimitReachDis)
            {
                complete?.Invoke();
                Clear();
                return;
            }
            // 子弹正在飞行
            Vector3 tdir = (tpos - SelfT.position).normalized;
            if (curdir != Vector3.zero)
            {
                tdir = (tdir + curdir).normalized;
            }
            if (CC != null)
            {
                CC.Move(MoveSpeed * Time.deltaTime * tdir);
            }
            else
            {
                SelfT.Translate(MoveSpeed * Time.deltaTime * tdir);
            }
        }
    }
}