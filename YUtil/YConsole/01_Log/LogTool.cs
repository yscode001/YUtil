// Author：yaoshuai
// Email：yscode@126.com
// Date：2021-12-21
// ------------------------------

using System;
using System.IO;
using System.Text;

namespace YConsole
{
    #region 初始化
    public partial class LogTool
    {
        private static LogConfig logConfig;
        private static StreamWriter logFileWriter = null;

        public static void InitSettings(LogConfig logConfig = null)
        {
            if (logConfig == null)
            {
                logConfig = new LogConfig();
            }
            LogTool.logConfig = logConfig;
        }
    }
    #endregion
    #region 日志修饰
    public partial class LogTool
    {
        private static string DecorateLog(string message)
        {
            StringBuilder sb = new StringBuilder(logConfig.Prefix, 100);
            if (logConfig.IsEnableTime)
            {
                sb.AppendFormat(" {0}", DateTime.Now.ToString("HH:mm:ss--fff"));
            }
            sb.AppendFormat(" {0} {1}", logConfig.Separator, message);
            return sb.ToString();
        }
    }
    #endregion
    public partial class LogTool
    {
        private static void PrintLog(string msg, LogColor color)
        {
            switch (color)
            {
                case LogColor.Red:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case LogColor.Green:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case LogColor.Blue:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case LogColor.Cyan:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case LogColor.Magenta:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case LogColor.Yellow:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case LogColor.None:
                default:
                    Console.WriteLine(msg);
                    break;
            }
        }
        public static void Log(string message, params object[] args)
        {
            if (!logConfig.IsEnable) { return; }
            message = DecorateLog(string.Format(message, args));
            PrintLog(message, LogColor.None);
        }
        public static void Log(object obj)
        {
            if (!logConfig.IsEnable) { return; }
            string message = DecorateLog(obj.ToString());
            PrintLog(message, LogColor.None);
        }
        public static void ColorLog(LogColor color, string message, params object[] args)
        {
            if (!logConfig.IsEnable) { return; }
            message = DecorateLog(string.Format(message, args));
            PrintLog(message, color);
        }
        public static void ColorLog(LogColor color, object obj)
        {
            if (!logConfig.IsEnable) { return; }
            string message = DecorateLog(obj.ToString());
            PrintLog(message, color);
        }
        public static void Warning(string message, params object[] args)
        {
            if (!logConfig.IsEnable) { return; }
            message = DecorateLog(string.Format(message, args));
            PrintLog(message, LogColor.Yellow);
        }
        public static void Warning(object obj)
        {
            if (!logConfig.IsEnable) { return; }
            string message = DecorateLog(obj.ToString());
            PrintLog(message, LogColor.Yellow);
        }
        public static void Error(string message, params object[] args)
        {
            if (!logConfig.IsEnable) { return; }
            message = DecorateLog(string.Format(message, args));
            PrintLog(message, LogColor.Red);
        }
        public static void Error(object obj)
        {
            if (!logConfig.IsEnable) { return; }
            string message = DecorateLog(obj.ToString());
            PrintLog(message, LogColor.Red);
        }
    }
}