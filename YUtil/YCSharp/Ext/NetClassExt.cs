// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-10
// ------------------------------

namespace YCSharp
{
    public static class NetClassExt
    {
        public static bool IsNull<T>(this T obj) where T : class
        {
            return obj == null;
        }

        public static bool IsNotNull<T>(this T obj) where T : class
        {
            return obj != null;
        }
    }
}