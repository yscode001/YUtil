// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-10
// ------------------------------
using System;

namespace YCSharp
{
    public static class ActionExt
    {
        public static void InvokeSafety(this Action act)
        {
            if (act == null) { return; }
            act.Invoke();
        }

        public static void InvokeSafety<T>(this Action<T> act, T t)
        {
            if (act == null) { return; }
            act.Invoke(t);
        }

        public static void InvokeSafety<T1, T2>(this Action<T1, T2> act, T1 t1, T2 t2)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2);
        }

        public static void InvokeSafety<T1, T2, T3>(this Action<T1, T2, T3> act, T1 t1, T2 t2, T3 t3)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3);
        }

        public static void InvokeSafety<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> act, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3, t4);
        }

        public static void InvokeSafety<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> act, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3, t4, t5);
        }

        public static void InvokeSafety<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> act, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3, t4, t5, t6);
        }

        public static void InvokeSafety<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7> act, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3, t4, t5, t6, t7);
        }

        public static void InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> act, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3, t4, t5, t6, t7, t8);
        }

        public static void InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> act, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9);
        }

        public static void InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> act, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
        }

        public static void InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> act, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
        }

        public static void InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> act, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);
        }

        public static void InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> act, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13);
        }

        public static void InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> act, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
        }

        public static void InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> act, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15);
        }

        public static void InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> act, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16)
        {
            if (act == null) { return; }
            act.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16);
        }
    }
}