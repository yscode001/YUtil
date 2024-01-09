// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-12-7
// ------------------------------
using System;
using System.Collections.Generic;

namespace YCSharp
{
    public static class RandomExt
    {
        /// <summary>
        /// 获取包含上限与下限的随机数
        /// </summary>
        /// <param name="random">随机对象</param>
        /// <param name="includeMin">包含下限</param>
        /// <param name="includeMax">包含上限</param>
        /// <returns></returns>
        public static int NextValue(this Random random, int includeMin, int includeMax)
        {
            if (random == null)
            {
                throw new Exception("random不能为空");
            }
            return random.Next(includeMin, includeMax + 1);
        }

        /// <summary>
        /// 获取包含上限与下限的随机数
        /// </summary>
        /// <param name="random">随机对象</param>
        /// <param name="includeMin">包含下限</param>
        /// <param name="includeMax">包含上限</param>
        /// <param name="exceptValue">随机结果排除某个值，如果是这个值，则重新随机</param>
        /// <returns></returns>
        public static int NextValue(this Random random, int includeMin, int includeMax, int exceptValue)
        {
            if (random == null)
            {
                throw new Exception("random不能为空");
            }
            int result = random.Next(includeMin, includeMax + 1);
            while (result == exceptValue)
            {
                result = random.Next(includeMin, includeMax + 1);
            }
            return result;
        }

        /// <summary>
        /// 随机数是否在最大概率以内
        /// </summary>
        /// <param name="random">随机对象</param>
        /// <param name="maxPercent">最大概率，0-100的整数值</param>
        /// <returns></returns>
        public static bool Percent(this Random random, int maxPercent)
        {
            if (random == null)
            {
                throw new Exception("random不能为空");
            }
            if (maxPercent <= 0) { return false; }
            else if (maxPercent >= 100) { return true; }
            else
            {
                return random.Next(1, 100) <= maxPercent;
            }
        }

        /// <summary>
        /// 随机数是否在最大概率以内
        /// </summary>
        /// <param name="random">随机对象</param>
        /// <param name="maxPercent">最大概率，0-100的浮点数</param>
        /// <returns></returns>
        public static bool Percent(this Random random, float maxPercent)
        {
            if (random == null)
            {
                throw new Exception("random不能为空");
            }
            if (maxPercent <= 0) { return false; }
            else if (maxPercent >= 100) { return true; }
            else
            {
                return random.Next(1, 10000) <= (maxPercent * 100);
            }
        }

        /// <summary>
        /// 将数组打乱后重新排列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random">随机对象</param>
        /// <param name="array">数组</param>
        public static void RandomArray<T>(this Random random, T[] array)
        {
            if (random == null)
            {
                throw new Exception("random不能为空");
            }
            if (array == null || array.Length <= 1)
            {
                return;
            }
            for (int i = 0; i < array.Length; i++)
            {
                int randomIndex = random.Next(array.Length);
                T temp = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
        }

        /// <summary>
        /// 随机获取数组里的一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random">随机对象</param>
        /// <param name="array">数组</param>
        /// <returns></returns>
        public static T RandomElement<T>(this Random random, T[] array)
        {
            if (random == null)
            {
                throw new Exception("random不能为空");
            }
            if (array == null || array.Length <= 0)
            {
                throw new Exception("array不能为空");
            }
            if (array.Length == 1)
            {
                return array[0];
            }
            return array[random.Next(0, array.Length)];
        }

        /// <summary>
        /// 将集合打乱后重新排列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random">随机对象</param>
        /// <param name="list">集合</param>
        public static void RandomList<T>(this Random random, List<T> list)
        {
            if (random == null)
            {
                throw new Exception("random不能为空");
            }
            if (list == null || list.Count <= 1)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = random.Next(list.Count);
                T temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        /// <summary>
        /// 随机获取集合里的一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random">随机对象</param>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static T RandomElement<T>(this Random random, List<T> list)
        {
            if (random == null)
            {
                throw new Exception("random不能为空");
            }
            if (list == null || list.Count <= 0)
            {
                throw new Exception("list不能为空");
            }
            if (list.Count == 1)
            {
                return list[0];
            }
            return list[random.Next(0, list.Count)];
        }
    }
}