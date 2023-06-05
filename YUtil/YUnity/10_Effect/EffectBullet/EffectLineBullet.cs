using System;
using UnityEngine;

namespace YUnity
{
    public enum EffectLineBulletType
    {
        Time,
        Distance,
    }

    /// <summary>
    /// 直线子弹，达到时间或距离后销毁
    /// </summary>
    public partial class EffectLineBullet : MonoBehaviourBaseY
    {
        private float MaxSeconds = 0;
        private float MaxDistance = 0;
        private EffectLineBulletType Type = EffectLineBulletType.Distance;

        private Vector3 Direction = Vector3.zero;

        /// <summary>
        /// 达到目标位置后的回调
        /// </summary>
        private Action Complete = null;

        /// <summary>
        /// 子弹速度
        /// </summary>
        private float MoveSpeed = 5;

        /// <summary>
        /// 子弹是否正在飞
        /// </summary>
        private bool IsMoving = false;

        private float CurTime = 0;
        private Vector3 StartPos = Vector3.zero;

        private void Clear()
        {
            IsMoving = false;
            MoveSpeed = 5;
            MaxSeconds = MaxDistance = 0;
            CurTime = 0;
            StartPos = Vector3.zero;
            Direction = Vector3.zero;
            Complete = null;
        }
    }
    public partial class EffectLineBullet
    {
        /// <summary>
        /// 开始飞行
        /// </summary>
        /// <param name="type">类型(时间还是距离)</param>
        /// <param name="maxSeconds">最大秒数</param>
        /// <param name="maxDistance">最大距离</param>
        /// <param name="moveSpeed">移动速度</param>
        /// <param name="complete">完成回调</param>
        public void Play(EffectLineBulletType type, Vector3 direction, float maxSeconds, float maxDistance, float moveSpeed, Action complete)
        {
            Clear();
            if ((direction == Vector3.zero) || moveSpeed <= 0 ||
                (type == EffectLineBulletType.Distance && maxDistance <= 0) ||
                (type == EffectLineBulletType.Time && maxSeconds <= 0))
            {
                // 设置的数据不对，啥也不做
                return;
            }
            Type = type;
            Direction = direction.normalized;
            MaxSeconds = maxSeconds;
            MaxDistance = maxDistance;
            MoveSpeed = moveSpeed;
            Complete = complete;
            StartPos = TransformY.position;
            IsMoving = true;
        }
    }
    public partial class EffectLineBullet
    {
        private void Update()
        {
            // 子弹未发射
            if (IsMoving == false)
            {
                return;
            }
            TransformY.Translate(MoveSpeed * Time.deltaTime * Direction);
            if (Type == EffectLineBulletType.Distance)
            {
                if (Vector3.Distance(TransformY.position, StartPos) >= MaxDistance)
                {
                    Complete?.Invoke();
                    Clear();
                }
            }
            else
            {
                CurTime += Time.deltaTime;
                if (CurTime >= MaxSeconds)
                {
                    Complete?.Invoke();
                    Clear();
                }
            }
        }
    }
}