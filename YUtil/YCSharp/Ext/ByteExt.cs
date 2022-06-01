// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-10
// ------------------------------
namespace YCSharp
{
    public static class ByteExt
    {
        public static string ToString(this byte[] bytes)
        {
            if (bytes == null) { return ""; }
            if (bytes.Length <= 0) { return ""; }
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}