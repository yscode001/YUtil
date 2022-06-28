using System;
using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// UI栈结构管理器，挂在Scene的某个GO上
    /// </summary>
    public partial class UIStackMag : MonoBehaviourBaseY
    {
        private UIStackMag() { }
        public static UIStackMag Instance { get; private set; } = null;

        private Stack<RectTransform> rtStack;
        private Stack<RectTransform> RTStack
        {
            get
            {
                if (rtStack == null)
                {
                    rtStack = new Stack<RectTransform>();
                }
                return rtStack;
            }
        }

        internal void Init()
        {
            this.Log("初始化(YFramework)：UI栈结构管理器(UIStackMag)");
            Instance = this;
        }

        private void OnDestroy()
        {
            this.Log("释放(YFramework)：UI栈结构管理器(UIStackMag)");
            Instance = null;
        }
    }
    #region push
    public partial class UIStackMag
    {
        /// <summary>
        /// 压栈，把RT放入栈中
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="parent"></param>
        /// <param name="pushType"></param>
        public void Push(RectTransform rt, Transform parent, UIStackPushType pushType)
        {
            if (rt == null || RTStack.Contains(rt) || parent == null)
            {
                return;
            }
            rt.SetParent(parent, false);
            rt.SetAct(true);
            RectTransform bottomRT = null;
            if (RTStack.Count > 0)
            {
                bottomRT = RTStack.Peek();
                bottomRT.GetOrAddComponent<UIStackBaseWnd>()?.OnPause(rt);
            }
            rt.GetOrAddComponent<UIStackBaseWnd>()?.OnPush(pushType, bottomRT);
            RTStack.Push(rt);
        }
        /// <summary>
        /// 压栈，把RT放入栈中
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="parent"></param>
        /// <param name="pushType"></param>
        /// <param name="before">压栈流程开始之前执行</param>
        /// <param name="complete">压栈流程完成之后执行</param>
        public void Push(RectTransform rt, Transform parent, UIStackPushType pushType, Action<RectTransform> before, Action<RectTransform> complete)
        {
            if (rt == null || RTStack.Contains(rt) || parent == null)
            {
                return;
            }
            before?.Invoke(rt);
            Push(rt, parent, pushType);
            complete?.Invoke(rt);
        }
    }
    #endregion
    #region 私有API:pop工具方法
    public partial class UIStackMag
    {
        private void PrivatePop(UIStackPopMode popMode, UIStackPopType popType, int popCount, Action before, Action complete)
        {
            if (popCount <= 0 || RTStack.Count <= 0) { return; };
            int pc = popCount;
            switch (popMode)
            {
                case UIStackPopMode.Pop: pc = 1; break;
                case UIStackPopMode.PopCount: pc = Mathf.Clamp(popCount, 0, RTStack.Count); break;
                case UIStackPopMode.PopToRoot: pc = RTStack.Count - 1; break;
                case UIStackPopMode.PopAll: pc = RTStack.Count; break;
                default: break;
            }
            if (pc <= 0) { return; }
            before?.Invoke();
            UIStackPopMode model = pc == 1 ? UIStackPopMode.Pop : popMode;

            List<RectTransform> willPopRTList = new List<RectTransform>();
            for (int times = 1; times <= pc && RTStack.Count > 0; times++)
            {
                RectTransform willPopRT = RTStack.Pop();
                if (willPopRT != null)
                {
                    willPopRTList.Add(willPopRT);
                }
            }
            if (RTStack.Count > 0)
            {
                RectTransform topRT = RTStack.Peek();
                if (topRT != null)
                {
                    RectTransform popFirstRT = null;
                    if (willPopRTList.Count > 0) { popFirstRT = willPopRTList[0]; }
                    topRT.GetOrAddComponent<UIStackBaseWnd>()?.OnResume(popFirstRT);
                }
            }
            foreach (RectTransform rt in willPopRTList)
            {
                rt.GetOrAddComponent<UIStackBaseWnd>()?.OnExit(popMode, popType);
            }
            complete?.Invoke();
        }
    }
    #endregion
    #region pop
    public partial class UIStackMag
    {
        /// <summary>
        /// 出栈，把栈顶元素pop掉
        /// </summary>
        /// <param name="popType"></param>
        public void Pop(UIStackPopType popType)
        {
            PrivatePop(UIStackPopMode.Pop, popType, 1, null, null);
        }
        /// <summary>
        /// 出栈，把栈顶元素pop掉
        /// </summary>
        /// <param name="popType"></param>
        /// <param name="before">出栈流程开始之前执行</param>
        /// <param name="complete">出栈流程完成之后执行</param>
        public void Pop(UIStackPopType popType, Action before, Action complete)
        {
            PrivatePop(UIStackPopMode.Pop, popType, 1, before, complete);
        }
    }
    #endregion
    #region popCount
    public partial class UIStackMag
    {
        /// <summary>
        /// 指定次数的pop，即pop掉指定个数的栈顶元素
        /// </summary>
        /// <param name="popType"></param>
        /// <param name="popCount"></param>
        public void PopCount(UIStackPopType popType, int popCount)
        {
            PrivatePop(UIStackPopMode.PopCount, popType, popCount, null, null);
        }
        /// <summary>
        /// 指定次数的pop，即pop掉指定个数的栈顶元素
        /// </summary>
        /// <param name="popType"></param>
        /// <param name="popCount"></param>
        /// <param name="before">出栈流程开始之前执行</param>
        /// <param name="complete">出栈流程完成之后执行</param>
        public void PopCount(UIStackPopType popType, int popCount, Action before, Action complete)
        {
            PrivatePop(UIStackPopMode.PopCount, popType, popCount, before, complete);
        }
    }
    #endregion
    #region popToRoot
    public partial class UIStackMag
    {
        /// <summary>
        /// 出栈至栈中只剩一个元素
        /// </summary>
        /// <param name="popType"></param>
        public void PopToRoot(UIStackPopType popType)
        {
            PrivatePop(UIStackPopMode.PopToRoot, popType, RTStack.Count - 1, null, null);
        }
        /// <summary>
        /// 出栈至栈中只剩一个元素
        /// </summary>
        /// <param name="popType"></param>
        /// <param name="before">出栈流程开始之前执行</param>
        /// <param name="complete">出栈流程完成之后执行</param>
        public void PopToRoot(UIStackPopType popType, Action before, Action complete)
        {
            PrivatePop(UIStackPopMode.PopToRoot, popType, RTStack.Count - 1, before, complete);
        }
    }
    #endregion
    #region popAll
    public partial class UIStackMag
    {
        /// <summary>
        /// pop掉全部元素
        /// </summary>
        /// <param name="popType"></param>
        public void PopAll(UIStackPopType popType)
        {
            PrivatePop(UIStackPopMode.PopAll, popType, RTStack.Count, null, null);
        }
        /// <summary>
        /// pop掉全部元素
        /// </summary>
        /// <param name="popType"></param>
        /// <param name="before">出栈流程开始之前执行</param>
        /// <param name="complete">出栈流程完成之后执行</param>
        public void PopAll(UIStackPopType popType, Action before, Action complete)
        {
            PrivatePop(UIStackPopMode.PopAll, popType, RTStack.Count, before, complete);
        }
    }
    #endregion
    #region 简单pop方法
    public partial class UIStackMag
    {
        public void Pop_destroy() { Pop(UIStackPopType.Destroy); }
        public void Pop_exit() { Pop(UIStackPopType.Exit); }
        public void Pop_close() { Pop(UIStackPopType.Close); }
        public void Pop_cancel() { Pop(UIStackPopType.Cancel); }
        public void Pop_back() { Pop(UIStackPopType.Back); }
        public void Pop_submit() { Pop(UIStackPopType.Submit); }
        public void Pop_delete() { Pop(UIStackPopType.Delete); }
        public void Pop_done() { Pop(UIStackPopType.Done); }
        public void Pop_send() { Pop(UIStackPopType.Send); }
        public void Pop_confirm() { Pop(UIStackPopType.Confirm); }

        public void PopToRoot_destroy() { PopToRoot(UIStackPopType.Destroy); }
        public void PopToRoot_exit() { PopToRoot(UIStackPopType.Exit); }
        public void PopToRoot_close() { PopToRoot(UIStackPopType.Close); }
        public void PopToRoot_cancel() { PopToRoot(UIStackPopType.Cancel); }
        public void PopToRoot_back() { PopToRoot(UIStackPopType.Back); }
        public void PopToRoot_submit() { PopToRoot(UIStackPopType.Submit); }
        public void PopToRoot_delete() { PopToRoot(UIStackPopType.Delete); }
        public void PopToRoot_done() { PopToRoot(UIStackPopType.Done); }
        public void PopToRoot_send() { PopToRoot(UIStackPopType.Send); }
        public void PopToRoot_confirm() { PopToRoot(UIStackPopType.Confirm); }

        public void PopAll_destroy() { PopAll(UIStackPopType.Destroy); }
        public void PopAll_exit() { PopAll(UIStackPopType.Exit); }
        public void PopAll_close() { PopAll(UIStackPopType.Close); }
        public void PopAll_cancel() { PopAll(UIStackPopType.Cancel); }
        public void PopAll_back() { PopAll(UIStackPopType.Back); }
        public void PopAll_submit() { PopAll(UIStackPopType.Submit); }
        public void PopAll_delete() { PopAll(UIStackPopType.Delete); }
        public void PopAll_done() { PopAll(UIStackPopType.Done); }
        public void PopAll_send() { PopAll(UIStackPopType.Send); }
        public void PopAll_confirm() { PopAll(UIStackPopType.Confirm); }
    }
    #endregion
    #region other
    public partial class UIStackMag
    {
        /// <summary>
        /// 仅仅清空栈里面所有的元素(不会执行元素的Destroy方法)
        /// </summary>
        public void ClearStackElementsButNotExecuteDestroyElementMethod()
        {
            RTStack.Clear();
        }
        /// <summary>
        /// 栈里面元素的个数
        /// </summary>
        public int StackElementsCount => RTStack.Count;

        /// <summary>
        /// 获取栈顶元素
        /// </summary>
        public RectTransform TopElement => RTStack.Peek();
    }
    #endregion
}