// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-10
// ------------------------------
using System.Text;

namespace YCSharp
{
    public static class ByteExt
    {
        public static string ToString(this byte[] bytes, Encoding encoding)
        {
            if (bytes == null || bytes.Length <= 0) { return ""; }
            return encoding.GetString(bytes);
        }
    }
}