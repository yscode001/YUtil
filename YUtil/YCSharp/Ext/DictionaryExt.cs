// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-10
// ------------------------------
using System.Collections.Generic;

namespace YCSharp
{
    public static class DictionaryExt
    {
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