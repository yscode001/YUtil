// Author：yaoshuai
// Email：yscode@126.com
// Date：2024-5-28
// ------------------------------

namespace YGame.BlockPuzzle
{
    /// <summary>
    /// 填充进度
    /// </summary>
    public enum FillProgressType
    {
        /// <summary>
        /// 未填充，全部为空
        /// </summary>
        Empty,

        /// <summary>
        /// 填充部分
        /// </summary>
        Part,

        /// <summary>
        /// 全部填充，填充满了
        /// </summary>
        Full,
    }
}