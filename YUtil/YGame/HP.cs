using System;

namespace YGame
{
    public partial class HP
    {
        public float Max { get; private set; }
        public float Cur { get; private set; }

        public bool IsAlive => Max > 0 && Cur > 0;
        public bool IsDeath => Max <= 0 || Cur <= 0;

        /// <summary>
        /// 当前血量百分比(0-1)
        /// </summary>
        public float Percent
        {
            get
            {
                if (Cur <= 0 || Max <= 0) { return 0; }
                if (Cur >= Max) { return 1; }
                return Cur / Max;
            }
        }
    }
    public partial class HP
    {
        /// <summary>
        /// 设置初始血量
        /// </summary>
        /// <param name="initHP"></param>
        public void SetupInitHP(float initHP)
        {
            if (initHP <= 0)
            {
                throw new Exception("error");
            }
            else
            {
                Max = Cur = initHP;
            }
        }

        /// <summary>
        /// 设置最大血量
        /// </summary>
        /// <param name="maxHP"></param>
        /// <exception cref="Exception"></exception>
        public void SetupMaxHP(float maxHP)
        {
            if (maxHP <= 0)
            {
                throw new Exception("error");
            }
            else
            {
                if (maxHP > Max)
                {
                    // 提升最大血量
                    Max = maxHP;
                }
                else if (maxHP < Max)
                {
                    // 降低最大血量
                    Max = maxHP;
                    Cur = Math.Min(Cur, Max);
                }
            }
        }
    }
    public partial class HP
    {
        /// <summary>
        /// 被秒杀(血量直接重置为0)
        /// </summary>
        public void BeSecKilled()
        {
            Cur = 0;
        }

        /// <summary>
        /// 受伤
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public (bool isAlive, bool isChanged) BeInjured(float damage)
        {
            if (damage > 0 && Cur > 0)
            {
                Cur = Math.Max(Cur - damage, 0);
                return (IsAlive, true);
            }
            else
            {
                return (IsAlive, false);
            }
        }

        /// <summary>
        /// 回血(回调参数表示血量是否变化了)
        /// </summary>
        /// <param name="blood"></param>
        /// <param name="complete"></param>
        public (bool isAlive, bool isChanged) BloodReturning(float blood)
        {
            if (blood > 0 && Cur > 0 && Cur < Max)
            {
                Cur = Math.Min(Cur + blood, Max);
                return (IsAlive, true);
            }
            else
            {
                return (IsAlive, false);
            }
        }
    }
}