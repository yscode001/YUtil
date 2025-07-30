// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-10
// ------------------------------
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace YCSharp
{
    public static class DictionaryExt
    {
        public static bool HasElements<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return dictionary != null && dictionary.Count > 0;
        }
        public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return dictionary == null || dictionary.Count == 0;
        }
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
        public static void For<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Predicate<(TKey key, TValue value)> condition, Action<(TKey key, TValue value)> doAction)
        {
            foreach (var item in dictionary)
            {
                if (condition == null)
                {
                    doAction?.Invoke((item.Key, item.Value));
                }
                else if (condition.Invoke((item.Key, item.Value)))
                {
                    doAction?.Invoke((item.Key, item.Value));
                }
            }
        }
        public static Dictionary<TKey, TValue> DeepCopy<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Predicate<(TKey key, TValue value)> condition)
        {
            Dictionary<TKey, TValue> newDict = new Dictionary<TKey, TValue>();
            dictionary.For(condition, data =>
            {
                newDict.AddOrUpdate(data.key, data.value);
            });
            return newDict;
        }

        public static string ToJsonString<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            if (dict == null || dict.Count == 0)
            {
                return null;
            }
            try
            {
                return JsonConvert.SerializeObject(dict);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static T ToModel<T, TKey, TValue>(this Dictionary<TKey, TValue> dict) where T : class
        {
            if (dict == null || dict.Count == 0)
            {
                return null;
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(dict));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}