using UnityEngine;

namespace YUnity
{
    #region 常用属性
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

        /// <summary>
        /// 获取栈顶元素
        /// </summary>
        public RectTransform StackTopElement => UIStackMag.Instance.TopElement;

        /// <summary>
        /// 自己是否是栈顶元素
        /// </summary>
        public bool IsStackTopElement => this == StackTopElement;

        private bool _isPageActive = false;

        /// <summary>
        /// 页面是否活跃(OnPushOrOnResume后为true，OnPauseOrOnExit后为false)
        /// </summary>
        public bool IsPageActive => _isPageActive;
    }
    #endregion
    #region 自定义生命周期函数
    public partial class UIStackBaseWnd
    {
        /// <summary>
        /// 元素被放入栈中，可以交互
        /// </summary>
        /// <param name="targetPageType"></param>
        /// <param name="bottomRT">自己底下的元素，即自己从哪个页面进来的</param>
        public virtual void OnPush(TargetPageType targetPageType, RectTransform bottomRT)
        {
            CvsGroup.alpha = 1;
            CvsGroup.blocksRaycasts = true;
            _isPageActive = true;
        }

        /// <summary>
        /// 有新的元素入栈，此界面暂停，失去交互
        /// </summary>
        /// <param name="topRT">被push在自己上面的游戏物体</param>
        public virtual void OnPause(RectTransform topRT)
        {
            CvsGroup.blocksRaycasts = false;
            _isPageActive = false;
        }

        /// <summary>
        /// 自己上面的元素被pop掉，此界面恢复，可以交互
        /// </summary>
        /// <param name="popedRT">从哪个页面发起的pop</param>
        public virtual void OnResume(RectTransform popedRT)
        {
            CvsGroup.alpha = 1;
            CvsGroup.blocksRaycasts = true;
            _isPageActive = true;
        }

        /// <summary>
        /// 自己被pop后执行退出方法(此方法里面会执行Destroy)
        /// </summary>
        /// <param name="popType">自己被pop掉的方式</param>
        /// <param name="popReason">自己被pop掉的理由</param>
        public virtual void OnExit(PopType popType, PopReason popReason)
        {
            _isPageActive = false;
            Destroy(gameObject);
        }

        /// <summary>
        /// 在OnPush或OnResume之后执行
        /// </summary>
        /// <param name="isAfterOnPush">true为onPush之后执行，false为OnResume之后执行</param>
        public virtual void ExecuteAfterOnPushOrOnResume(bool isAfterOnPush)
        {
            _isPageActive = true;
        }
    }
    #endregion
}