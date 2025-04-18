﻿using System;
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
            return Random.NextValue(includeMin, includeMax);
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
        /// 将数组打乱后重新排列
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="array">数组</param>
        public static void RandomArray<T>(T[] array)
        {
            Random.RandomArray(array);
        }

        /// <summary>
        /// 深copy一个在源数组的基础上打乱顺序后的新数组，源数组不受任何影响
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">源数组</param>
        /// <returns>深copy一个在源数组的基础上打乱顺序后的新数组</returns>
        public static T[] DeepCopyRandomArray<T>(T[] array)
        {
            return Random.DeepCopyRandomArray(array);
        }

        /// <summary>
        /// 随机获取数组里的一个元素
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="array">数组</param>
        /// <returns></returns>
        public static T RandomElement<T>(T[] array)
        {
            return Random.RandomElement(array);
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

        /// <summary>
        /// 深copy一个在源集合的基础上打乱顺序后的新集合，源集合不受任何影响
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">源集合</param>
        /// <returns>深copy一个在源集合的基础上打乱顺序后的新集合</returns>
        public static List<T> DeepCopyRandomList<T>(List<T> list)
        {
            return Random.DeepCopyRandomList(list);
        }

        /// <summary>
        /// 随机获取集合里的一个元素
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static T RandomElement<T>(List<T> list)
        {
            return Random.RandomElement(list);
        }

        /// <summary>
        /// 随机一个bool值
        /// </summary>
        /// <returns></returns>
        public static bool RandomBool()
        {
            return Random.RandomBool();
        }

        /// <summary>
        /// 根据权重随机出一个权重的索引
        /// </summary>
        /// <param name="weightArray">权重数组</param>
        /// <returns>随机出权重数组中的索引</returns>
        public static int GetWeightIndex(int[] weightArray)
        {
            return Random.GetWeightIndex(weightArray);
        }

        /// <summary>
        /// 根据权重随机出一个权重的索引
        /// </summary>
        /// <param name="weightList">权重集合</param>
        /// <returns>随机出权重集合中的索引</returns>
        public static int GetWeightIndex(List<int> weightList)
        {
            return Random.GetWeightIndex(weightList);
        }
    }
}