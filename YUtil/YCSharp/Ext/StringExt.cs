// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-10
// ------------------------------
using System;
using System.Security.Cryptography;
using System.Text;

namespace YCSharp
{
    public static class StringExt
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static byte[] ToByteArray(this string str, Encoding encoding)
        {
            if (string.IsNullOrEmpty(str)) { return null; }
            return encoding.GetBytes(str);
        }

        public static string MD5(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return "";
            }
            byte[] result = Encoding.Default.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }
    }
}