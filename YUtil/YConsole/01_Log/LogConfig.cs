// Author：yaoshuai
// Email：yscode@126.com
// Date：2021-12-21
// ------------------------------

using System;

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

        #region 保存日志
        /// <summary>
        /// 是否允许保存日志
        /// </summary>
        public bool IsEnableSave = true;

        /// <summary>
        /// 保存日志时是否覆盖
        /// </summary>
        public bool IsEnableSaveCover = true;

        /// <summary>
        /// 日志保存目录
        /// </summary>
        public string LogDirectoryPath => AppDomain.CurrentDomain.BaseDirectory + "/YLogs/";

        /// <summary>
        /// 日志文件名称
        /// </summary>
        public string LogFileName => "YLog.txt";

        /// <summary>
        /// 日志保存默认完整路径
        /// </summary>
        public string LogDefaultFileFullPath => LogDirectoryPath + LogFileName;
        #endregion
    }
}