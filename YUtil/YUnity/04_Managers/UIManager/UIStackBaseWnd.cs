using System;
using UnityEngine;

namespace YUnity
{
    [RequireComponent(typeof(CanvasGroup))]
    public partial class UIStackBaseWnd : MonoBehaviourBaseY
    {
        private CanvasGroup cvsGroup;
        public CanvasGroup CvsGroup
        {
            get
            {
                if (cvsGroup == null)
                {
                    cvsGroup = gameObject.GetComponent<CanvasGroup>();
                }
                return cvsGroup;
            }
        }

        /// <summary>
        /// 元素被放入栈中，可以交互
        /// </summary>
        /// <param name="pushType"></param>
        public virtual void OnPush(UIStackPushType pushType)
        {
            CvsGroup.alpha = 1;
            CvsGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// 有新的元素入栈，此界面暂停，失去交互
        /// </summary>
        /// <param name="topRT">被push在自己上面的游戏物体</param>
        public virtual void OnPause(RectTransform topRT)
        {
            CvsGroup.blocksRaycasts = false;
        }

        /// <summary>
        /// 自己上面的元素被pop掉，此界面恢复，可以交互
        /// </summary>
        public virtual void OnResume()
        {
            CvsGroup.alpha = 1;
            CvsGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// 自己被pop后执行退出方法(此方法里面会执行Destroy)
        /// </summary>
        /// <param name="popMode">自己被pop掉的方式</param>
        /// <param name="popType">自己被pop掉的理由</param>
        public virtual void OnExit(UIStackPopMode popMode, UIStackPopType popType)
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// 自己当前的alpha
        /// </summary>
        public virtual float AlphaValue
        {
            get => CvsGroup.alpha;
            set
            {
                CvsGroup.alpha = Mathf.Clamp(value, 0, 1);
            }
        }

        /// <summary>
        /// 是否可以交互
        /// </summary>
        public bool IsInteractive
        {
            get => CvsGroup.blocksRaycasts;
            set
            {
                CvsGroup.blocksRaycasts = value;
            }
        }
    }
    #region pop
    public partial class UIStackBaseWnd
    {
        /// <summary>
        /// Pop掉栈顶元素
        /// </summary>
        /// <param name="popType"></param>
        public virtual void PopStackTopElement(UIStackPopType popType)
        {
            UIStackMag.Instance.Pop(popType);
        }

        /// <summary>
        /// Pop掉栈顶元素
        /// </summary>
        /// <param name="popType"></param>
        /// <param name="before">出栈流程开始之前执行</param>
        /// <param name="complete">出栈流程完成之后执行</param>
        public virtual void PopStackTopElement(UIStackPopType popType, Action before, Action complete)
        {
            UIStackMag.Instance.Pop(popType, before, complete);
        }
    }
    #endregion
    #region popCount
    public partial class UIStackBaseWnd
    {
        /// <summary>
        /// pop掉栈中固定个数的栈顶元素
        /// </summary>
        /// <param name="popType"></param>
        /// <param name="popCount">固定个数的栈顶元素</param>
        public virtual void PopStackFixedCountElements(UIStackPopType popType, int popCount)
        {
            UIStackMag.Instance.PopCount(popType, popCount);
        }
        /// <summary>
        /// pop掉栈中固定个数的栈顶元素
        /// </summary>
        /// <param name="popType"></param>
        /// <param name="popCount">固定个数的栈顶元素</param>
        /// <param name="before">出栈流程开始之前执行</param>
        /// <param name="complete">出栈流程完成之后执行</param>
        public virtual void PopStackFixedCountElements(UIStackPopType popType, int popCount, Action before, Action complete)
        {
            UIStackMag.Instance.PopCount(popType, popCount, before, complete);
        }
    }
    #endregion
    #region popToRoot
    public partial class UIStackBaseWnd
    {
        /// <summary>
        /// 出栈至栈中只剩一个元素
        /// </summary>
        /// <param name="popType"></param>
        public virtual void PopStackToRootElement(UIStackPopType popType)
        {
            UIStackMag.Instance.PopToRoot(popType);
        }

        /// <summary>
        /// 出栈至栈中只剩一个元素
        /// </summary>
        /// <param name="popType"></param>
        /// <param name="before">出栈流程开始之前执行</param>
        /// <param name="complete">出栈流程完成之后执行</param>
        public virtual void PopStackToRootElement(UIStackPopType popType, Action before, Action complete)
        {
            UIStackMag.Instance.PopToRoot(popType, before, complete);
        }
    }
    #endregion
    #region popAll
    public partial class UIStackBaseWnd
    {
        /// <summary>
        /// pop掉栈中的所有元素
        /// </summary>
        /// <param name="popType"></param>
        public virtual void PopStackAllElements(UIStackPopType popType)
        {
            UIStackMag.Instance.PopAll(popType);
        }
        /// <summary>
        /// pop掉栈中的所有元素
        /// </summary>
        /// <param name="popType"></param>
        /// <param name="before">出栈流程开始之前执行</param>
        /// <param name="complete">出栈流程完成之后执行</param>
        public virtual void PopStackAllElements(UIStackPopType popType, Action before, Action complete)
        {
            UIStackMag.Instance.PopAll(popType, before, complete);
        }
    }
    #endregion
    #region 简单pop方法
    public partial class UIStackBaseWnd
    {
        public void PopStackTopElement_destroy() { PopStackTopElement(UIStackPopType.Destroy); }
        public void PopStackTopElement_exit() { PopStackTopElement(UIStackPopType.Exit); }
        public void PopStackTopElement_close() { PopStackTopElement(UIStackPopType.Close); }
        public void PopStackTopElement_cancel() { PopStackTopElement(UIStackPopType.Cancel); }
        public void PopStackTopElement_back() { PopStackTopElement(UIStackPopType.Back); }
        public void PopStackTopElement_submit() { PopStackTopElement(UIStackPopType.Submit); }
        public void PopStackTopElement_delete() { PopStackTopElement(UIStackPopType.Delete); }
        public void PopStackTopElement_done() { PopStackTopElement(UIStackPopType.Done); }
        public void PopStackTopElement_send() { PopStackTopElement(UIStackPopType.Send); }
        public void PopStackTopElement_confirm() { PopStackTopElement(UIStackPopType.Confirm); }

        public void PopStackToRootElement_destroy() { PopStackToRootElement(UIStackPopType.Destroy); }
        public void PopStackToRootElement_exit() { PopStackToRootElement(UIStackPopType.Exit); }
        public void PopStackToRootElement_close() { PopStackToRootElement(UIStackPopType.Close); }
        public void PopStackToRootElement_cancel() { PopStackToRootElement(UIStackPopType.Cancel); }
        public void PopStackToRootElement_back() { PopStackToRootElement(UIStackPopType.Back); }
        public void PopStackToRootElement_submit() { PopStackToRootElement(UIStackPopType.Submit); }
        public void PopStackToRootElement_delete() { PopStackToRootElement(UIStackPopType.Delete); }
        public void PopStackToRootElement_done() { PopStackToRootElement(UIStackPopType.Done); }
        public void PopStackToRootElement_send() { PopStackToRootElement(UIStackPopType.Send); }
        public void PopStackToRootElement_confirm() { PopStackToRootElement(UIStackPopType.Confirm); }

        public void PopStackAllElements_destroy() { PopStackAllElements(UIStackPopType.Destroy); }
        public void PopStackAllElements_exit() { PopStackAllElements(UIStackPopType.Exit); }
        public void PopStackAllElements_close() { PopStackAllElements(UIStackPopType.Close); }
        public void PopStackAllElements_cancel() { PopStackAllElements(UIStackPopType.Cancel); }
        public void PopStackAllElements_back() { PopStackAllElements(UIStackPopType.Back); }
        public void PopStackAllElements_submit() { PopStackAllElements(UIStackPopType.Submit); }
        public void PopStackAllElements_delete() { PopStackAllElements(UIStackPopType.Delete); }
        public void PopStackAllElements_done() { PopStackAllElements(UIStackPopType.Done); }
        public void PopStackAllElements_send() { PopStackAllElements(UIStackPopType.Send); }
        public void PopStackAllElements_confirm() { PopStackAllElements(UIStackPopType.Confirm); }
    }
    #endregion
    #region 其他
    public partial class UIStackBaseWnd
    {
        /// <summary>
        /// 获取栈顶元素
        /// </summary>
        public RectTransform StackTopElement => UIStackMag.Instance.TopElement;

        /// <summary>
        /// 自己是否是栈顶元素
        /// </summary>
        public bool IsStackTopElement => this == StackTopElement;
    }
    #endregion
}