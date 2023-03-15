// Author：yaoshuai
// Email：yscode@126.com
// Date：2021-12-25
// ------------------------------

using System;

namespace YUnity
{
    public class RandomMag
    {
        private static Random random;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="seed">种子</param>
        public static void InitRandom(int seed)
        {
            random = new Random(seed);
        }

        /// <summary>
        /// 获取包含上限与下限的随机数
        /// </summary>
        /// <param name="min">包含下限</param>
        /// <param name="max">包含上限</param>
        /// <returns></returns>
        public static int Next(int min, int max)
        {
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
            random = new Random();
            return random.Next(min, max + 1);
        }
    }
}