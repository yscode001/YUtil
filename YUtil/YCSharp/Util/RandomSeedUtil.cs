// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-12-8
// ------------------------------
using System;
using System.Collections.Generic;

namespace YCSharp
{
    /// <summary>
    /// 随机数工具(种子)
    /// </summary>
    public partial class RandomSeedUtil
    {
        private static Random _randomSeed;
        private static Random RandomSeed
        {
            get
            {
                if (_randomSeed == null)
                {
                    _randomSeed = new Random(0);
                }
                return _randomSeed;
            }
        }

        public static void Init(int seed)
        {
            _randomSeed = new Random(seed);
        }
    }
    public partial class RandomSeedUtil
    {
        /// <summary>
        /// 获取包含上限与下限的随机数
        /// </summary>
        /// <param name="includeMin">包含下限</param>
        /// <param name="includeMax">包含上限</param>
        /// <returns></returns>
        public static int NextValue(int includeMin, int includeMax)
        {
            return RandomSeed.NextValue(includeMin, includeMax);
        }

        /// <summary>
        /// 获取包含上限与下限的随机数
        /// </summary>
        /// <param name="includeMin">包含下限</param>
        /// <param name="includeMax">包含上限</param>
        /// <param name="exceptValue">随机结果排除某个值，如果是这个值，则重新随机</param>
        /// <returns></returns>
        public static int NextValue(int includeMin, int includeMax, int exceptValue)
        {
            return RandomSeed.NextValue(includeMin, includeMax, exceptValue);
        }

        /// <summary>
        /// 随机数是否在最大概率以内
        /// </summary>
        /// <param name="maxPercent">最大概率，0-100的整数值</param>
        /// <returns></returns>
        public static bool Percent(int maxPercent)
        {
            return RandomSeed.Percent(maxPercent);
        }

        /// <summary>
        /// 随机数是否在最大概率以内
        /// </summary>
        /// <param name="maxPercent">最大概率，0-100的浮点数</param>
        /// <returns></returns>
        public static bool Percent(float maxPercent)
        {
            return RandomSeed.Percent(maxPercent);
        }

        /// <summary>
        /// 将数组打乱后重新排列
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="array">数组</param>
        public static void RandomArray<T>(T[] array)
        {
            RandomSeed.RandomArray(array);
        }

        /// <summary>
        /// 随机获取数组里的一个元素
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="array">数组</param>
        /// <returns></returns>
        public static T RandomElement<T>(T[] array)
        {
            return RandomSeed.RandomElement(array);
        }

        /// <summary>
        /// 将集合打乱后重新排列
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">集合</param>
        public static void RandomList<T>(List<T> list)
        {
            RandomSeed.RandomList(list);
        }

        /// <summary>
        /// 随机获取集合里的一个元素
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static T RandomElement<T>(List<T> list)
        {
            return RandomSeed.RandomElement(list);
        }
    }
}