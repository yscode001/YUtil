// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-10
// ------------------------------
using System;

namespace YCSharp
{
    public static class FuncExt
    {
        public static TResult InvokeSafety<TResult>(this Func<TResult> fuc, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke();
        }

        public static TResult InvokeSafety<T, TResult>(this Func<T, TResult> fuc, T t, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t);
        }

        public static TResult InvokeSafety<T1, T2, TResult>(this Func<T1, T2, TResult> fuc, T1 t1, T2 t2, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2);
        }

        public static TResult InvokeSafety<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> fuc, T1 t1, T2 t2, T3 t3, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3);
        }

        public static TResult InvokeSafety<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> fuc, T1 t1, T2 t2, T3 t3, T4 t4, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3, t4);
        }

        public static TResult InvokeSafety<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> fuc, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3, t4, t5);
        }

        public static TResult InvokeSafety<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> fuc, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3, t4, t5, t6);
        }

        public static TResult InvokeSafety<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> fuc, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3, t4, t5, t6, t7);
        }

        public static TResult InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> fuc, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3, t4, t5, t6, t7, t8);
        }

        public static TResult InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> fuc, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9);
        }

        public static TResult InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> fuc, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
        }

        public static TResult InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> fuc, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
        }

        public static TResult InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> fuc, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);
        }

        public static TResult InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> fuc, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13);
        }

        public static TResult InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> fuc, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
        }

        public static TResult InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> fuc, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15);
        }

        public static TResult InvokeSafety<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> fuc, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, TResult defaultVaule)
        {
            if (fuc == null) { return defaultVaule; }
            return fuc.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16);
        }
    }
}