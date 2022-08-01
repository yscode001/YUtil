// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-1
// ------------------------------

namespace YUnity
{
    public partial class ScreenCfg
    {
        /// <summary>
        /// 标准屏宽
        /// </summary>
        public static uint StandardWidth { get; private set; } = 2160;

        /// <summary>
        /// 标准屏高
        /// </summary>
        public static uint StandardHeight { get; private set; } = 1080;

        /// <summary>
        /// 实际屏宽与标准屏宽的比例
        /// </summary>
        public static float WidthRate { get; private set; } = (UnityEngine.Screen.width * 1f) / (StandardWidth * 1f);

        /// <summary>
        /// 实际屏高与标准屏高的比例
        /// </summary>
        public static float HeightRate { get; private set; } = (UnityEngine.Screen.height * 1f) / (StandardHeight * 1f);
    }
    public partial class ScreenCfg
    {
        internal static void SetupData(uint standardWidth, uint standardHeight)
        {
            StandardWidth = standardWidth;
            StandardHeight = standardHeight;
            WidthRate = (UnityEngine.Screen.width * 1f) / (StandardWidth * 1f);
            HeightRate = (UnityEngine.Screen.height * 1f) / (StandardHeight * 1f);
        }
    }
}