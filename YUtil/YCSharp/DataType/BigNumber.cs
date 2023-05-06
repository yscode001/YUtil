// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-4-6
// ------------------------------

using System;

namespace YCSharp
{
    /// <summary>
    /// 大数值
    /// </summary>
    [Serializable]
    public partial struct BigNumber
    {
        /// <summary>
        /// 单位
        /// </summary>
        public UInt32 Unit { get; private set; }

        /// <summary>
        /// 数值
        /// </summary>
        public float Value { get; private set; }

        public BigNumber(UInt32 unit, float value)
        {
            Unit = Math.Max(0, unit);
            Value = Math.Max(0, value);
            HandleValue();
        }

        private void HandleValue()
        {
            if (Value <= 0)
            {
                // <= 0，归0
                Unit = Zero.Unit;
                Value = Zero.Value;
            }
            else if (Value >= UnitValue)
            {
                // >= UnitValue，进位
                Unit += 1;
                Value /= UnitValue;
                if (Value >= UnitValue)
                {
                    // 递归进位
                    HandleValue();
                }
            }
            else if (Value < 1)
            {
                if (Unit > 0)
                {
                    // 退位
                    Unit -= 1;
                    Value *= UnitValue;
                    if (Value < 1)
                    {
                        // 递归退位
                        HandleValue();
                    }
                }
                else
                {
                    // 已是最小单位，没发退了，就这样吧
                }
            }
            else
            {
                // 1 <= Value && Value < UnitValue，不做处理
            }
        }
    }

    #region 转换运算及加法减法运算
    public partial struct BigNumber
    {
        /// <summary>
        /// 求转换单位后的值
        /// </summary>
        /// <param name="toUnit"></param>
        /// <returns></returns>
        public BigNumber ConvertTo(UInt32 toUnit)
        {
            if (toUnit < 0 || toUnit == Unit) { return new BigNumber(Unit, Value); }
            double value = Value * Math.Pow(UnitValue, Unit - toUnit);
            return new BigNumber(toUnit, (float)value);
        }

        /// <summary>
        /// 相同单位值相加
        /// </summary>
        /// <param name="value"></param>
        public void Plus(float value)
        {
            if (value == 0) { return; }
            Value += value;
            HandleValue();
        }

        /// <summary>
        /// 相同单位值相减
        /// </summary>
        /// <param name="value"></param>
        public void Subtract(float value)
        {
            Plus(-value);
        }

        /// <summary>
        /// 大数值相加
        /// </summary>
        /// <param name="value"></param>
        public void Plus(BigNumber value)
        {
            if (value.Value == 0) { return; }
            if (value.Unit == Unit)
            {
                Plus(value.Value);
            }
            else if (value.Unit > Unit)
            {
                // 大单位 -> 小单位，再相加
                Plus(value.ConvertTo(Unit));
            }
            else
            {
                // 大单位 -> 小单位，再相加
                ConvertTo(value.Unit).Plus(value);
            }
        }

        /// <summary>
        /// 大数值相减
        /// </summary>
        /// <param name="value"></param>
        public void Subtract(BigNumber value)
        {
            if (value.Value == 0) { return; }
            if (value.Unit == Unit)
            {
                Subtract(value.Value);
            }
            else if (value.Unit > Unit)
            {
                // 大单位 -> 小单位，再相减
                Subtract(value.ConvertTo(Unit));
            }
            else
            {
                // 大单位 -> 小单位，再相减
                ConvertTo(value.Unit).Subtract(value);
            }
        }

        /// <summary>
        /// 求百分比(0-1)，当前值为分子
        /// </summary>
        /// <param name="denominator">分母</param>
        /// <returns></returns>
        public float Percent(BigNumber denominator)
        {
            if (this <= Zero || denominator <= Zero) { return 0; }
            if (this >= denominator) { return 1; }
            if (denominator.Unit - Unit >= 2)
            {
                // 超过2个数量级了，直接返回0
                return 0;
            }
            return Value / denominator.ConvertTo(Unit).Value;
        }
    }
    #endregion

    #region 比较运算符重载
    public partial struct BigNumber
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(BigNumber lhs, BigNumber rhs)
        {
            return lhs.Unit == rhs.Unit && lhs.Value == rhs.Value;
        }
        public static bool operator !=(BigNumber lhs, BigNumber rhs)
        {
            return lhs.Unit != rhs.Unit || lhs.Value != rhs.Value;
        }
        public static bool operator <(BigNumber lhs, BigNumber rhs)
        {
            return lhs.Unit < rhs.Unit ||
                (lhs.Unit == rhs.Unit && lhs.Value < rhs.Value);
        }
        public static bool operator <=(BigNumber lhs, BigNumber rhs)
        {
            return lhs.Unit < rhs.Unit ||
                (lhs.Unit == rhs.Unit && lhs.Value <= rhs.Value);
        }
        public static bool operator >(BigNumber lhs, BigNumber rhs)
        {
            return lhs.Unit > rhs.Unit ||
                (lhs.Unit == rhs.Unit && lhs.Value > rhs.Value);
        }
        public static bool operator >=(BigNumber lhs, BigNumber rhs)
        {
            return lhs.Unit > rhs.Unit ||
                (lhs.Unit == rhs.Unit && lhs.Value >= rhs.Value);
        }
        public static BigNumber operator +(BigNumber lhs, BigNumber rhs)
        {
            lhs.Plus(rhs);
            return new BigNumber(lhs.Unit, lhs.Value);
        }
        public static BigNumber operator -(BigNumber lhs, BigNumber rhs)
        {
            lhs.Subtract(rhs);
            return new BigNumber(lhs.Unit, lhs.Value);
        }
    }
    #endregion

    #region 静态方法和属性
    public partial struct BigNumber
    {
        /// <summary>
        /// Zero
        /// </summary>
        public static BigNumber Zero { get; private set; } = new BigNumber(0, 0);

        /// <summary>
        /// 1个单位的值，即1个大单位等于多少个小单位，默认1000
        /// </summary>
        public static float UnitValue { get; private set; } = 1000;

        /// <summary>
        /// 初始化单位的值，即1个大单位等于多少个小单位，默认1000
        /// </summary>
        /// <param name="unitValue"></param>
        public static void InitUnitValue(float unitValue = 1000)
        {
            UnitValue = Math.Max(1000, unitValue);
        }
    }
    #endregion
}