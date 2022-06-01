// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-10
// ------------------------------
using System.Collections.Generic;
using System.ComponentModel;

namespace YCSharp
{
    public static class DictionaryExt
    {
        /// <summary>
        /// 根据key获取value
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue TryGet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, [DefaultValue(null)] TValue defaultValue)
        {
            if (dictionary == null) { return defaultValue; }
            TValue val;
            if (dictionary.TryGetValue(key, out val))
            {
                return val;
            }
            return defaultValue;
        }

        /// <summary>
        /// 存在key不添加，否则添加
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AddIfNotExistKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null) { return false; }
            if (dictionary.ContainsKey(key))
            {
                return false;
            }
            dictionary.Add(key, value);
            return true;
        }

        /// <summary>
        /// 存在key则更新，不存在直接返回
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool UpdateIfExistKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null) { return false; }
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 存在key则更新，不存在则添加
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null) { return; }
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}