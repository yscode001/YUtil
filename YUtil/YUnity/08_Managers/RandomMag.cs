// Author：yaoshuai
// Email：yscode@126.com
// Date：2021-12-25
// ------------------------------

using System;

namespace YUnity
{
    #region 种子
    public partial class RandomMag
    {
        private static Random randomSeed;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="seed">种子</param>
        public static void Init(int seed)
        {
            randomSeed = new Random(seed);
        }

        /// <summary>
        /// 获取包含上限与下限的随机数(有种子)
        /// </summary>
        /// <param name="includeMin">包含下限</param>
        /// <param name="includeMax">包含上限</param>
        /// <returns></returns>
        public static int NextWithSeed(int includeMin, int includeMax)
        {
            if (randomSeed == null)
            {
                randomSeed = new Random(0);
            }
            return randomSeed.Next(includeMin, includeMax + 1);
        }

        /// <summary>
        /// 初始化并获取包含上限与下限的随机数(有种子)
        /// </summary>
        /// <param name="seed">种子</param>
        /// <param name="includeMin">包含下限</param>
        /// <param name="includeMax">包含上限</param>
        /// <returns></returns>
        public static int InitAndGetNextWithSeed(int seed, int includeMin, int includeMax)
        {
            randomSeed = new Random(seed);
            return randomSeed.Next(includeMin, includeMax + 1);
        }

        /// <summary>
        /// 随机数是否在最大概率以内(用种子)
        /// </summary>
        /// <param name="maxPercent">最大概率，0-100的整数值</param>
        /// <returns></returns>
        public static bool RandomPercentWithSeed(int maxPercent)
        {
            if (maxPercent <= 0) { return false; }
            else if (maxPercent >= 100) { return true; }
            else
            {
                if (randomSeed == null)
                {
                    randomSeed = new Random(0);
                }
                return randomSeed.Next(1, 100) <= maxPercent;
            }
        }
    }
    #endregion
    #region 非种子
    public partial class RandomMag
    {
        private static Random randomNoSeed;

        /// <summary>
        /// 获取包含上限与下限的随机数(不用种子)
        /// </summary>
        /// <param name="includeMin">包含下限</param>
        /// <param name="includeMax">包含上限</param>
        /// <returns></returns>
        public static int NextWithoutSeed(int includeMin, int includeMax)
        {
            if (randomNoSeed == null)
            {
                randomNoSeed = new Random();
            }
            return randomNoSeed.Next(includeMin, includeMax + 1);
        }

        /// <summary>
        /// 随机数是否在最大概率以内(不用种子)
        /// </summary>
        /// <param name="maxPercent">最大概率，0-100的整数值</param>
        /// <returns></returns>
        public static bool RandomPercentWithoutSeed(int maxPercent)
        {
            if (maxPercent <= 0) { return false; }
            else if (maxPercent >= 100) { return true; }
            else
            {
                if (randomNoSeed == null)
                {
                    randomNoSeed = new Random();
                }
                return randomNoSeed.Next(1, 100) <= maxPercent;
            }
        }
    }
    #endregion
}