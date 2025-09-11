using UnityEngine;

namespace YUnity
{
    #region 常用属性
    [RequireComponent(typeof(CanvasGroup))]
    public partial class UIStackBaseWnd : MonoBehaviourBaseY
    {
        private CanvasGroup _cvsGroup;
        public CanvasGroup CvsGroup
        {
            get
            {
                if (_cvsGroup == null)
                {
                    _cvsGroup = gameObject.GetComponent<CanvasGroup>();
                }
                return _cvsGroup;
            }
        }

        /// <summary>
        /// CanvasGroup的alpha
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
        /// 是否可以交互，CanvasGroup的blocksRaycasts
        /// </summary>
        public bool IsInteractive
        {
            get => CvsGroup.blocksRaycasts;
            set
            {
                CvsGroup.blocksRaycasts = value;
            }
        }

        /// <summary>
        /// 获取栈顶元素
        /// </summary>
        public RectTransform GetStackTopElement() => UIStackMag.Instance.TopElement;

        /// <summary>
        /// 自己是否是栈顶元素
        /// </summary>
        public bool IsStackTopElement => this == GetStackTopElement();

        private bool _isPushedOrResumed = false;

        /// <summary>
        /// OnPushOrOnResume后为true，OnPauseOrOnExit后为false
        /// </summary>
        public bool IsPushedOrResumed => _isPushedOrResumed;
    }
    #endregion
    #region 自定义生命周期函数
    public partial class UIStackBaseWnd
    {
        /// <summary>
        /// 元素被放入栈中，可以交互
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="bottomRT">自己底下的元素，即自己从哪个页面进来的</param>
        public virtual void OnPush(PageType pageType, RectTransform bottomRT)
        {
            CvsGroup.alpha = 1;
            CvsGroup.blocksRaycasts = true;
            _isPushedOrResumed = true;
        }

        /// <summary>
        /// 有新的元素入栈，此界面暂停，失去交互
        /// </summary>
        /// <param name="topRT">被push在自己上面的游戏物体</param>
        public virtual void OnPause(RectTransform topRT, PageType topPageType)
        {
            CvsGroup.blocksRaycasts = false;
            _isPushedOrResumed = false;
            if (topPageType == PageType.NewPage && UIStackMag.Instance.MaxPushTransitionSeconds > 0)
            {
                Invoke(nameof(SetActiveFalseAfterOnPause), UIStackMag.Instance.MaxPushTransitionSeconds);
            }
        }
        private void SetActiveFalseAfterOnPause()
        {
            if (!this.IsStackTopElement)
            {
                this.SetAct(false);
            }
        }

        /// <summary>
        /// 自己上面的元素被pop掉，此界面恢复，可以交互
        /// </summary>
        /// <param name="popedRT">从哪个页面发起的pop</param>
        public virtual void OnResume(RectTransform popedRT)
        {
            this.SetAct(true);
            CvsGroup.alpha = 1;
            CvsGroup.blocksRaycasts = true;
            _isPushedOrResumed = true;
        }

        /// <summary>
        /// 自己被pop后执行退出方法(此方法里面会执行Destroy)
        /// </summary>
        /// <param name="popType">自己被pop掉的方式</param>
        /// <param name="popReason">自己被pop掉的理由</param>
        /// <param name="delayThenDestroySeconds">延迟x秒后执行Destroy</param>
        public virtual void OnExit(PopType popType, PopReason popReason, float delayThenDestroySeconds)
        {
            if (delayThenDestroySeconds <= 0 || gameObject.activeInHierarchy == false)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DoAfterDelay(delayThenDestroySeconds, () => { DestroyImmediate(gameObject); });
            }
        }

        /// <summary>
        /// 在OnPush或OnResume之后执行
        /// </summary>
        /// <param name="isAfterOnPush">true为onPush之后执行，false为OnResume之后执行</param>
        public virtual void ExecuteAfterOnPushOrOnResume(bool isAfterOnPush)
        {
            _isPushedOrResumed = true;
        }
    }
    #endregion
}