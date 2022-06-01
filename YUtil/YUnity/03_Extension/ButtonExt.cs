// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-2
// ------------------------------
using UnityEngine.UI;

namespace YUnity
{
    public static class ButtonExt
    {
        /// <summary>
        /// 延迟启用按钮(常用于防止重复点击)
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="delay"></param>
        /// <param name="timeUnit"></param>
        public static void EnableDelay(this Button btn, float delay = 1, TimeUnit timeUnit = TimeUnit.Second)
        {
            if (delay <= 0) { return; }
            try
            {
                btn.interactable = false;
                QueueMag.Instance.RunOnMainQueue(() =>
                {
                    try
                    {
                        btn.interactable = true;
                    }
                    catch { }
                }, delay, timeUnit);
            }
            catch { }
        }
    }
}