// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-5-19
// ------------------------------

namespace YUnity
{
    /// <summary>
    /// UI栈结构的push类别
    /// </summary>
    public enum UIStackPushType
    {
        /// <summary>
        /// 新页面
        /// </summary>
        NewPage,

        /// <summary>
        /// 新弹框
        /// </summary>
        Dialog,
    }

    /// <summary>
    /// UI栈结构的pop方式
    /// </summary>
    public enum UIStackPopMode
    {
        /// <summary>
        /// 普通的pop掉栈顶元素，只pop1次
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

    /// <summary>
    /// UI栈结构的pop类别
    /// </summary>
    public enum UIStackPopType
    {
        /// <summary>
        /// 点击销毁按钮进行pop
        /// </summary>
        Destroy,

        /// <summary>
        /// 点击退出按钮进行pop
        /// </summary>
        Exit,

        /// <summary>
        /// 点击关闭按钮进行pop
        /// </summary>
        Close,

        /// <summary>
        /// 点击取消按钮进行pop
        /// </summary>
        Cancel,

        /// <summary>
        /// 点击返回按钮进行pop
        /// </summary>
        Back,

        /// <summary>
        /// 点击提交按钮(进行操作)进行pop
        /// </summary>
        Submit,

        /// <summary>
        /// 点击删除按钮进行pop
        /// </summary>
        Delete,

        /// <summary>
        /// 点击完成按钮进行pop
        /// </summary>
        Done,

        /// <summary>
        /// 点击发送按钮进行pop
        /// </summary>
        Send,

        /// <summary>
        /// 点击确定按钮进行pop
        /// </summary>
        Confirm,
    }
}