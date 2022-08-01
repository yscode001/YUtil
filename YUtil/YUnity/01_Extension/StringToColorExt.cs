// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-6-17
// ------------------------------

using UnityEngine;

namespace YUnity
{
    public static class StringToColorExt
    {
        public static Color Color(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { return new Color(0, 0, 0, 0); }
            if (ColorUtility.TryParseHtmlString(value, out Color col))
            {
                return col;
            }
            else
            {
                return new Color(0, 0, 0, 0);
            }
        }
    }
}