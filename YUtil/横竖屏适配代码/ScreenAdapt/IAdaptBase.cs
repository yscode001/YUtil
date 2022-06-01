namespace YFramework
{
    public interface IAdaptBase
    {
        /// <summary>
        /// 横竖屏切换事件
        /// </summary>
        void OnChange();

        /// <summary>
        /// 切换为横屏后调用
        /// </summary>
        void OnLandscape();

        /// <summary>
        /// 切换为竖屏后调用
        /// </summary>
        void OnPortrait();
    }
}