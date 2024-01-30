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
        /// 深copy一个在源数组的基础上打乱顺序后的新数组，源数组不受任何影响
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random">随机对象</param>
        /// <param name="array">源数组</param>
        /// <returns>深copy一个在源数组的基础上打乱顺序后的新数组</returns>
        /// <exception cref="Exception"></exception>
        public static T[] DeepCopyRandomArray<T>(this Random random, T[] array)
        {
            if (random == null)
            {
                throw new Exception("random不能为空");
            }
            if (array == null || array.Length <= 0)
            {
                throw new Exception("array不能为空");
            }
            T[] newArray = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }
            RandomArray(random, newArray);
            return newArray;
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
        /// 深copy一个在源集合的基础上打乱顺序后的新集合，源集合不受任何影响
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random">随机对象</param>
        /// <param name="list">源集合</param>
        /// <returns>深copy一个在源集合的基础上打乱顺序后的新集合</returns>
        /// <exception cref="Exception"></exception>
        public static List<T> DeepCopyRandomList<T>(this Random random, List<T> list)
        {
            if (random == null)
            {
                throw new Exception("random不能为空");
            }
            if (list == null || list.Count <= 0)
            {
                throw new Exception("list不能为空");
            }
            List<T> newList = new List<T>();
            foreach (var item in list)
            {
                newList.Add(item);
            }
            RandomList(random, newList);
            return newList;
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

        /// <summary>
        /// 随机一个bool值
        /// </summary>
        /// <param name="random">随机对象</param>
        /// <returns></returns>
        public static bool RandomBool(this Random random)
        {
            if (random == null)
            {
                throw new Exception("random不能为空");
            }
            return random.Next(0, 11) % 2 == 0;
        }

        /// <summary>
        /// 根据权重随机出一个权重的索引
        /// </summary>
        /// <param name="random">随机对象</param>
        /// <param name="weightArray">权重数组</param>
        /// <returns>随机出权重数组中的索引</returns>
        /// <exception cref="Exception"></exception>
        public static int GetWeightIndex(this Random random, int[] weightArray)
        {
            if (random == null)
            {
                throw new Exception("random不能为空");
            }
            if (weightArray == null || weightArray.Length <= 0)
            {
                throw new Exception("array不能为空");
            }
            int maxValue = 0;
            foreach (var weightValue in weightArray)
            {
                if (weightValue > 0)
                {
                    maxValue += weightValue;
                }
            }
            if (maxValue > 0)
            {
                int curMin = 0;
                int curMax = 0;
                int randomValue = random.NextValue(0, maxValue);
                for (int i = 0; i < weightArray.Length; i++)
                {
                    int weightValue = weightArray[i];
                    if (weightValue > 0)
                    {
                        curMin += 1;
                        curMax += weightValue;
                        if (curMin <= randomValue && randomValue <= curMax)
                        {
                            return i;
                        }
                        curMin = curMax;
                    }
                }
            }
            return random.Next(0, weightArray.Length);
        }

        /// <summary>
        /// 根据权重随机出一个权重的索引
        /// </summary>
        /// <param name="random">随机对象</param>
        /// <param name="weightList">权重集合</param>
        /// <returns>随机出权重集合中的索引</returns>
        /// <exception cref="Exception"></exception>
        public static int GetWeightIndex(this Random random, List<int> weightList)
        {
            if (random == null)
            {
                throw new Exception("random不能为空");
            }
            if (weightList == null || weightList.Count <= 0)
            {
                throw new Exception("list不能为空");
            }
            int maxValue = 0;
            foreach (var weightValue in weightList)
            {
                if (weightValue > 0)
                {
                    maxValue += weightValue;
                }
            }
            if (maxValue > 0)
            {
                int curMin = 0;
                int curMax = 0;
                int randomValue = random.NextValue(0, maxValue);
                for (int i = 0; i < weightList.Count; i++)
                {
                    int weightValue = weightList[i];
                    if (weightValue > 0)
                    {
                        curMin += 1;
                        curMax += weightValue;
                        if (curMin <= randomValue && randomValue <= curMax)
                        {
                            return i;
                        }
                        curMin = curMax;
                    }
                }
            }
            return random.Next(0, weightList.Count);
        }
    }
}