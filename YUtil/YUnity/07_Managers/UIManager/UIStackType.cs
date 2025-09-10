// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-5-19
// ------------------------------

namespace YUnity
{
    public enum TargetPageType
    {
        NewPage,
        Dialog,
    }

    public enum PopType
    {
        /// <summary>
        /// 普通的pop掉栈顶元素，只pop 1次
        /// </summary>
        Pop,

        /// <summary>
        /// 指定次数的pop，即pop掉指定个数的栈顶元素
        /// </summary>
        PopCount,

        /// <summary>
        /// pop至栈中只剩一个元素
        /// </summary>
        PopToRoot,

        /// <summary>
        /// pop掉全部元素
        /// </summary>
        PopAll,
    }

    public enum PopReason
    {
        Close,
        Cancel,
        Back,
        Exit,
        Destroy,
        Submit,
        Delete,
        Done,
        Send,
        Confirm,
    }
}