// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-10
// ------------------------------
using System;

namespace YCSharp
{
    public static class BoolExt
    {
        public static void Invoke(this bool boolValue, Action trueAct, Action falseAct)
        {
            if (boolValue)
            {
                trueAct?.Invoke();
            }
            else
            {
                falseAct?.Invoke();
            }
        }
    }
}