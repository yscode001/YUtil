using System;
using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 直线穿透子弹类型
    /// </summary>
    public enum EffectBulletLinePenetrateType
    {
        /// <summary>
        /// 飞行达到最长时间消失
        /// </summary>
        Time,

        /// <summary>
        /// 飞行超过最远距离小时
        /// </summary>
        Distance,
    }

    /// <summary>
    /// 直线穿透子弹，飞行达到最长时间或超过最远距离后消失
    /// </summary>
    public partial class EffectBulletLinePenetrate : MonoBehaviourBaseY
    {
        /// <summary>
        /// 子弹类型
        /// </summary>
        private EffectBulletLinePenetrateType Type = EffectBulletLinePenetrateType.Distance;

        /// <summary>
        /// 飞行方向
        /// </summary>
        private Vector3 Direction = Vector3.zero;

        /// <summary>
        /// 最长飞行秒数
        /// </summary>
        private float MaxSeconds = 0;

        /// <summary>
        /// 最远飞行距离
        /// </summary>
        private float MaxDistance = 0;

        /// <summary>
        /// 子弹速度
        /// </summary>
        private float MoveSpeed = 0;

        /// <summary>
        /// 当距敌人小于等于这个距离时，就算伤害了敌人
        /// </summary>
        private float LimitReachDis = 0;

        /// <summary>
        /// 路径上的敌人集合
        /// </summary>
        private List<Transform> AllEnemyList;

        /// <summary>
        /// 对路径上的敌人造成伤害
        /// </summary>
        private Action<Transform, bool> Damage;

        /// <summary>
        /// 完成回调
        /// </summary>
        private Action Complete = null;

        // 下面是辅助属性
        private bool IsMoving = false; // 是否正在移动
        private float CurDeltaTime = 0; // 辅助子弹类型为时间类型
        private Vector3 StartPos = Vector3.zero; // 辅助子弹类型为距离类型

        private void Clear()
        {
            IsMoving = false;

            Direction = Vector3.zero;
            MaxSeconds = MaxDistance = MoveSpeed = LimitReachDis = 0;
            AllEnemyList = null;
            Damage = null;
            Complete = null;

            CurDeltaTime = 0;
            StartPos = Vector3.zero;
        }
    }
    public partial class EffectBulletLinePenetrate
    {
        /// <summary>
        /// 子弹开始飞行
        /// </summary>
        /// <param name="type">子弹类型</param>
        /// <param name="direction">飞行方向</param>
        /// <param name="maxValue">最长飞行秒数或最远飞行距离</param>
        /// <param name="moveSpeed">飞行速度</param>
        /// <param name="limitReachDis">当距敌人小于等于这个距离时，就算伤害了敌人</param>
        /// <param name="allEnemyList">路径上的敌人集合</param>
        /// <param name="damage">对路径上的敌人造成伤害</param>
        /// <param name="complete">完成回调</param>
        public void BeginFlying(EffectBulletLinePenetrateType type, Vector3 direction, float maxValue, float moveSpeed, float limitReachDis, List<Transform> allEnemyList, Action<Transform, bool> damage, Action complete)
        {
            Clear();
            if (direction == Vector3.zero || moveSpeed <= 0 || maxValue <= 0 || limitReachDis < 0)
            {
                // 设置的数据不对，啥也不做，直接返回
                return;
            }
            Type = type;
            Direction = direction;
            if (type == EffectBulletLinePenetrateType.Distance) { MaxDistance = maxValue; StartPos = TransformY.position; }
            else { MaxSeconds = maxValue; }
            MoveSpeed = moveSpeed;
            LimitReachDis = limitReachDis;
            AllEnemyList = allEnemyList;
            Damage = damage;
            Complete = complete;

            IsMoving = true; // 开始飞行
        }
    }
    public partial class EffectBulletLinePenetrate
    {
        private void DamageEnemy()
        {
            if (Damage == null || AllEnemyList == null || AllEnemyList.Count <= 0) { return; }
            int index = 0;
            for (int i = AllEnemyList.Count - 1; i >= 0; i--)
            {
                Transform enemyTransform = AllEnemyList[i];
                if (enemyTransform != null && Vector3.Distance(enemyTransform.position, TransformY.position) <= LimitReachDis)
                {
                    Damage?.Invoke(enemyTransform, index == 0);
                    AllEnemyList.RemoveAt(i);
                    index += 1;
                }
            }
        }
        private void Update()
        {
            if (IsMoving == false)
            {
                return;
            }
            DamageEnemy();
            if ((Type == EffectBulletLinePenetrateType.Distance && Vector3.Distance(TransformY.position, StartPos) >= MaxDistance) ||
                (Type == EffectBulletLinePenetrateType.Time && CurDeltaTime >= MaxSeconds))
            {
                IsMoving = false;
                Complete?.Invoke();
                Clear();
                return;
            }
            if (Type == EffectBulletLinePenetrateType.Time)
            {
                CurDeltaTime += Time.deltaTime;
            }
            TransformY.Translate(MoveSpeed * Time.deltaTime * Direction, Space.World);
        }
    }
}