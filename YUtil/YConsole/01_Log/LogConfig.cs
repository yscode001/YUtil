// Author：yaoshuai
// Email：yscode@126.com
// Date：2021-12-21
// ------------------------------

/// <summary>
/// Color放在命名空间外面，便于扩展使用
/// </summary>
public enum LogColor
{
    None, Red, Green, Blue, Cyan, Magenta, Yellow
}

namespace YConsole
{
    public class LogConfig
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable = true;

        /// <summary>
        /// 日志前缀
        /// </summary>
        public string Prefix = "#";

        /// <summary>
        /// 是否显示当前时间
        /// </summary>
        public bool IsEnableTime = true;

        /// <summary>
        /// 分隔符
        /// </summary>
        public string Separator = ">>";
    }
}