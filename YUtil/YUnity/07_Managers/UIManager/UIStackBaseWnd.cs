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

        public PageState PageState { get; private set; } = PageState.AfterPush;

        public RectTransform GetStackTopElement() => UIStackMag.Instance.TopElement;

        public bool IsStackTopElement() => this == UIStackMag.Instance.TopElement;
    }
    #endregion
    #region 自定义生命周期函数
    public partial class UIStackBaseWnd
    {
        public virtual void OnPush(PageType pageType, RectTransform bottomRT)
        {
            this.SetAct(true);
            CvsGroup.alpha = 1;
            CvsGroup.blocksRaycasts = true;
            PageState = PageState.AfterPush;
        }
        public virtual void OnPause(RectTransform topRT, PageType topPageType)
        {
            CvsGroup.blocksRaycasts = false;
            PageState = PageState.AfterPause;
            if (topPageType == PageType.NewPage)
            {
                if (UIStackMag.Instance.MaxPushTransitionSeconds <= 0)
                {
                    SetActiveFalseAfterOnPause();
                }
                else
                {
                    Invoke(nameof(SetActiveFalseAfterOnPause), UIStackMag.Instance.MaxPushTransitionSeconds);
                }
            }
        }
        private void SetActiveFalseAfterOnPause()
        {
            if (!this.IsStackTopElement())
            {
                this.SetAct(false);
            }
        }

        public virtual void OnResume(RectTransform popedRT)
        {
            this.SetAct(true);
            CvsGroup.alpha = 1;
            CvsGroup.blocksRaycasts = true;
            PageState = PageState.AfterResume;
        }

        public virtual void OnExit(PopType popType, PopReason popReason, float delaySecondsThenDestroy)
        {
            PageState = PageState.AfterExit;
            if (delaySecondsThenDestroy <= 0 || gameObject.activeInHierarchy == false)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DoAfterDelay(delaySecondsThenDestroy, () => { DestroyImmediate(gameObject); });
            }
        }

        public virtual void ExecuteAfterOnPushOrOnResume(bool isAfterPush)
        {

        }
    }
    #endregion
}