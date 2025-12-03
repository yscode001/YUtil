using UnityEngine;

namespace YUnity
{
    #region 属性
    [RequireComponent(typeof(CanvasGroup))]
    public partial class UIStackBaseWnd : MonoBehaviourBaseY
    {
        public PageState PageState { get; private set; } = PageState.OnPush;
    }
    #endregion
    #region 自定义生命周期函数
    public partial class UIStackBaseWnd
    {
        public virtual void OnPush()
        {
            this.SetAct(true);
            CanvasGroupY.alpha = 1;
            CanvasGroupY.blocksRaycasts = true;
            PageState = PageState.OnPush;
        }
        public virtual void OnPause()
        {
            CanvasGroupY.blocksRaycasts = false;
            PageState = PageState.OnPause;
        }

        public virtual void OnResume()
        {
            this.SetAct(true);
            CanvasGroupY.alpha = 1;
            CanvasGroupY.blocksRaycasts = true;
            PageState = PageState.OnResume;
        }

        public virtual void OnExit(float delaySecondsThenDestroy)
        {
            PageState = PageState.OnExit;
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