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
        private static Random random;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="seed">种子</param>
        public static void Init(int seed)
        {
            random = new Random(seed);
        }

        /// <summary>
        /// 获取包含上限与下限的随机数(有种子)
        /// </summary>
        /// <param name="min">包含下限</param>
        /// <param name="max">包含上限</param>
        /// <returns></returns>
        public static int Next(int min, int max)
        {
            if (random == null)
            {
                random = new Random(0);
            }
            return random.Next(min, max + 1);
        }

        /// <summary>
        /// 初始化并获取包含上限与下限的随机数
        /// </summary>
        /// <param name="seed">种子</param>
        /// <param name="min">包含下限</param>
        /// <param name="max">包含上限</param>
        /// <returns></returns>
        public static int InitAndGetNext(int seed, int min, int max)
        {
            random = new Random(seed);
            return random.Next(min, max + 1);
        }

        /// <summary>
        /// 随机数是否在最大概率以内(用种子)
        /// </summary>
        /// <param name="maxPercent">最大概率，0-100的整数值</param>
        /// <returns></returns>
        public static bool RandomPercentWithSeed(int maxPercent)
        {
            int max = maxPercent;
            if (max < 0) { max = 0; }
            else if (max > 100) { max = 100; }

            if (random == null)
            {
                random = new Random(0);
            }
            int randomValue = random.Next(0, 101);

            return 0 <= randomValue && randomValue <= max;
        }
    }
    #endregion
    #region 非种子
    public partial class RandomMag
    {
        /// <summary>
        /// 随机数是否在最大概率以内(不用种子)
        /// </summary>
        /// <param name="maxPercent">最大概率，0-100的整数值</param>
        /// <returns></returns>
        public static bool RandomPercentWithoutSeed(int maxPercent)
        {
            int max = maxPercent;
            if (max < 0) { max = 0; }
            else if (max > 100) { max = 100; }

            Random random = new Random();
            int randomValue = random.Next(0, 101);

            return 0 <= randomValue && randomValue <= max;
        }
    }
    #endregion
}