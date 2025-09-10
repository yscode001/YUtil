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

        private Stack<RectTransform> _rtStack;
        private Stack<RectTransform> RTStack
        {
            get
            {
                if (_rtStack == null) { _rtStack = new Stack<RectTransform>(); }
                return _rtStack;
            }
        }

        private List<string> _pushedNames;
        private List<string> PushedNames
        {
            get
            {
                if (_pushedNames == null) { _pushedNames = new List<string>(); }
                return _pushedNames;
            }
        }

        internal void Init()
        {
            Instance = this;
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
        /// <param name="pageType"></param>
        public void Push(RectTransform rt, Transform parent, PageType pageType)
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
            rt.GetOrAddComponent<UIStackBaseWnd>()?.OnPush(pageType, bottomRT);
            rt.GetOrAddComponent<UIStackBaseWnd>().ExecuteAfterOnPushOrOnResume(true);
            RTStack.Push(rt);
            if (!string.IsNullOrWhiteSpace(rt.name) && !PushedNames.Contains(rt.name))
            {
                PushedNames.Add(rt.name);
            }
        }
        /// <summary>
        /// 压栈，把RT放入栈中
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="parent"></param>
        /// <param name="pageType"></param>
        /// <param name="before">压栈流程开始之前执行</param>
        public void Push(RectTransform rt, Transform parent, PageType pageType, Action<RectTransform> before)
        {
            if (rt == null || RTStack.Contains(rt) || parent == null)
            {
                return;
            }
            before?.Invoke(rt);
            Push(rt, parent, pageType);
        }
    }
    #endregion
    #region 私有API:pop工具方法
    public partial class UIStackMag
    {
        private void PrivatePop(PopType popType, PopReason popReason, int popCount)
        {
            if (popCount <= 0 || RTStack.Count <= 0) { return; };
            int pc = popCount;
            switch (popType)
            {
                case PopType.Pop: pc = 1; break;
                case PopType.PopCount: pc = Mathf.Clamp(popCount, 0, RTStack.Count); break;
                case PopType.PopToRoot: pc = RTStack.Count - 1; break;
                case PopType.PopAll: pc = RTStack.Count; break;
                default: break;
            }
            if (pc <= 0) { return; }
            PopType type = pc == 1 ? PopType.Pop : popType;

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
                    topRT.GetOrAddComponent<UIStackBaseWnd>()?.ExecuteAfterOnPushOrOnResume(false);
                }
            }
            foreach (RectTransform rt in willPopRTList)
            {
                rt.GetOrAddComponent<UIStackBaseWnd>()?.OnExit(popType, popReason);
            }
        }
    }
    #endregion
    #region pop
    public partial class UIStackMag
    {
        /// <summary>
        /// 出栈，把栈顶元素pop掉
        /// </summary>
        /// <param name="popReason"></param>
        public void Pop(PopReason popReason)
        {
            PrivatePop(PopType.Pop, popReason, 1);
        }

        /// <summary>
        /// 指定次数的pop，即pop掉指定个数的栈顶元素
        /// </summary>
        /// <param name="popReason"></param>
        /// <param name="popCount"></param>
        public void PopCount(PopReason popReason, int popCount)
        {
            PrivatePop(PopType.PopCount, popReason, popCount);
        }

        /// <summary>
        /// 出栈至栈中只剩一个元素
        /// </summary>
        /// <param name="popReason"></param>
        public void PopToRoot(PopReason popReason)
        {
            PrivatePop(PopType.PopToRoot, popReason, RTStack.Count - 1);
        }

        /// <summary>
        /// pop掉全部元素
        /// </summary>
        /// <param name="popReason"></param>
        public void PopAll(PopReason popReason)
        {
            PrivatePop(PopType.PopAll, popReason, RTStack.Count);
        }
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

        /// <summary>
        /// push过的RectTransform中是否包含名字rectTransformName(哪怕销毁过也算)
        /// </summary>
        /// <param name="rectTransformName">RectTransform的名字</param>
        /// <returns></returns>
        public bool IsPushedOnce(string rectTransformName)
        {
            if (string.IsNullOrWhiteSpace(rectTransformName)) { return false; }
            return PushedNames.Contains(rectTransformName);
        }
    }
    #endregion
}