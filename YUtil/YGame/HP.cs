using System;

namespace YGame
{
    public partial class HP
    {
        public float Init { get; private set; }
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
                if (Cur <= 0) { return 0; }
                if (Cur >= Max) { return 1; }
                return Cur / Max;
            }
        }
    }
    #region 直接设置血量
    public partial class HP
    {
        /// <summary>
        /// 初始血量、最大血量、当前血量
        /// </summary>
        /// <param name="initHP"></param>
        /// <exception cref="Exception"></exception>
        public void SetupInitHP(float initHP)
        {
            if (initHP <= 0)
            {
                throw new Exception("error");
            }
            else
            {
                Init = Max = Cur = initHP;
            }
        }

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

        public void SetupCurHP(float curHP)
        {
            if (curHP < 0)
            {
                throw new Exception("error");
            }
            else
            {
                Cur = Math.Min(curHP, Max);
            }
        }

        public void SetupMaxAndCurHP(float maxHP, float curHP)
        {
            if (maxHP <= 0 || curHP < 0)
            {
                throw new Exception("error");
            }
            else
            {
                Max = maxHP;
                Cur = Math.Min(curHP, maxHP);
            }
        }
    }
    #endregion
    #region 秒杀、受伤、回血
    public partial class HP
    {
        /// <summary>
        /// 秒杀(当前血量直接重置为0)
        /// </summary>
        public void BeSecKilled()
        {
            Cur = 0;
        }

        /// <summary>
        /// 受伤
        /// </summary>
        /// <param name="damage">需大于等于0</param>
        /// <returns></returns>
        public (bool isAlive, bool isChanged) BeInjured(float damage)
        {
            if (damage < 0)
            {
                throw new Exception("error");
            }
            else
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
        }

        /// <summary>
        /// 回血
        /// </summary>
        /// <param name="blood">需大于等于0</param>
        /// <returns></returns>
        public (bool isAlive, bool isChanged) BloodReturning(float blood)
        {
            if (blood < 0)
            {
                throw new Exception("error");
            }
            else
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
    #endregion
}