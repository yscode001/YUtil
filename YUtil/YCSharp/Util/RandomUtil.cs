// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-12-7
// ------------------------------
using System;
using System.Collections.Generic;

namespace YCSharp
{
    /// <summary>
    /// 随机数工具
    /// </summary>
    public partial class RandomUtil
    {
        private static Random _random;
        private static Random Random
        {
            get
            {
                if (_random == null)
                {
                    _random = new Random();
                }
                return _random;
            }
        }
    }
    public partial class RandomUtil
    {
        /// <summary>
        /// 获取包含上限与下限的随机数
        /// </summary>
        /// <param name="includeMin">包含下限</param>
        /// <param name="includeMax">包含上限</param>
        /// <returns></returns>
        public static int NextValue(int includeMin, int includeMax)
        {
            return Random.NextValue(includeMin, includeMax + 1);
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
            return Random.NextValue(includeMin, includeMax, exceptValue);
        }

        /// <summary>
        /// 随机数是否在最大概率以内
        /// </summary>
        /// <param name="maxPercent">最大概率，0-100的整数值</param>
        /// <returns></returns>
        public static bool Percent(int maxPercent)
        {
            return Random.Percent(maxPercent);
        }

        /// <summary>
        /// 随机数是否在最大概率以内
        /// </summary>
        /// <param name="maxPercent">最大概率，0-100的浮点数</param>
        /// <returns></returns>
        public static bool Percent(float maxPercent)
        {
            return Random.Percent(maxPercent);
        }

        /// <summary>
        /// 获取随机索引数组
        /// </summary>
        /// <param name="length">数组的长度</param>
        /// <returns></returns>
        public static int[] GetRandomIndexArray(int length)
        {
            return Random.GetRandomIndexArray(length);
        }

        /// <summary>
        /// 将数组打乱后重新排列
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="array">数组</param>
        public static void RandomArray<T>(T[] array)
        {
            Random.RandomArray(array);
        }

        /// <summary>
        /// 将集合打乱后重新排列
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">集合</param>
        public static void RandomList<T>(List<T> list)
        {
            Random.RandomList(list);
        }
    }
}