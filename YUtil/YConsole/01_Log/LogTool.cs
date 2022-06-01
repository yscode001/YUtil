// Author：yaoshuai
// Email：yscode@126.com
// Date：2021-12-21
// ------------------------------

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace YConsole
{
    #region 初始化
    public partial class LogTool
    {
        private static LogConfig logConfig;
        private static StreamWriter logFileWriter = null;

        public static void InitSettings(LogConfig logConfig = null)
        {
            if (logConfig == null) { logConfig = new LogConfig(); }
            LogTool.logConfig = logConfig;
            if (!logConfig.IsEnableSave) { return; }
            if (logConfig.IsEnableSaveCover)
            {
                try
                {
                    if (Directory.Exists(logConfig.LogDirectoryPath))
                    {
                        if (File.Exists(logConfig.LogDefaultFileFullPath))
                        {
                            File.Delete(logConfig.LogDefaultFileFullPath);
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(logConfig.LogDirectoryPath);
                    }
                    logFileWriter = File.AppendText(logConfig.LogDefaultFileFullPath);
                    logFileWriter.AutoFlush = true;
                }
                catch
                {
                    logFileWriter = null;
                }
            }
            else
            {
                string prefix = DateTime.Now.ToString("yyyyMMdd@HH-mm-ss");
                string path = logConfig.LogDirectoryPath + prefix + logConfig.LogFileName;
                try
                {
                    if (!Directory.Exists(logConfig.LogDirectoryPath))
                    {
                        Directory.CreateDirectory(logConfig.LogDirectoryPath);
                    }
                    logFileWriter = File.AppendText(path);
                    logFileWriter.AutoFlush = true;
                }
                catch
                {
                    logFileWriter = null;
                }
            }
        }
    }
    #endregion
    #region 日志修饰
    public partial class LogTool
    {
        private static string DecorateLog(string message, bool isTrace = false)
        {
            StringBuilder sb = new StringBuilder(logConfig.Prefix, 100);
            if (logConfig.IsEnableTime)
            {
                sb.AppendFormat(" {0}", DateTime.Now.ToString("HH:mm:ss--fff"));
            }
            if (logConfig.IsEnableTreadID)
            {
                sb.AppendFormat(" {0}", Thread.CurrentThread.ManagedThreadId);
            }
            sb.AppendFormat(" {0} {1}", logConfig.Separator, message);
            if (isTrace)
            {
                sb.AppendFormat(" \n    StackTrace：{0}", GetLogTrace());
            }
            return sb.ToString();
        }
        private static string GetLogTrace()
        {
            StackTrace st = new StackTrace(3, true);
            StringBuilder traceinfo = new StringBuilder();
            for (int i = 0; i < st.FrameCount; i++)
            {
                StackFrame sf = st.GetFrame(i);
                traceinfo.AppendFormat("\n      {0}::{1} line:{2}", sf.GetFileName(), sf.GetMethod(), sf.GetFileLineNumber());
            }
            return traceinfo.ToString();
        }
        private static void WriteToFile(string msg)
        {
            if (logConfig.IsEnableSave && logFileWriter != null)
            {
                try
                {
                    logFileWriter.WriteLine(msg);
                }
                catch
                {
                    logFileWriter = null;
                    return;
                }
            }
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
            WriteToFile("[Log]" + message);
        }
        public static void Log(object obj)
        {
            if (!logConfig.IsEnable) { return; }
            string message = DecorateLog(obj.ToString());
            PrintLog(message, LogColor.None);
            WriteToFile("[Log]" + message);
        }
        public static void Trace(string message, params object[] args)
        {
            if (!logConfig.IsEnable) { return; }
            message = DecorateLog(string.Format(message, args), true);
            PrintLog(message, LogColor.Yellow);
            WriteToFile("[Trace]" + message);
        }
        public static void Trace(object obj)
        {
            if (!logConfig.IsEnable) { return; }
            string message = DecorateLog(obj.ToString(), true);
            PrintLog(message, LogColor.Yellow);
            WriteToFile("[Trace]" + message);
        }
        public static void ColorLog(LogColor color, string message, params object[] args)
        {
            if (!logConfig.IsEnable) { return; }
            message = DecorateLog(string.Format(message, args));
            PrintLog(message, color);
            WriteToFile("[ColorLog]" + message);
        }
        public static void ColorLog(LogColor color, object obj)
        {
            if (!logConfig.IsEnable) { return; }
            string message = DecorateLog(obj.ToString());
            PrintLog(message, color);
            WriteToFile("[ColorLog]" + message);
        }
        public static void Warning(string message, params object[] args)
        {
            if (!logConfig.IsEnable) { return; }
            message = DecorateLog(string.Format(message, args));
            PrintLog(message, LogColor.Yellow);
            WriteToFile("[Warning]" + message);
        }
        public static void Warning(object obj)
        {
            if (!logConfig.IsEnable) { return; }
            string message = DecorateLog(obj.ToString());
            PrintLog(message, LogColor.Yellow);
            WriteToFile("[Warning]" + message);
        }
        public static void Error(string message, params object[] args)
        {
            if (!logConfig.IsEnable) { return; }
            message = DecorateLog(string.Format(message, args));
            PrintLog(message, LogColor.Red);
            WriteToFile("[Error]" + message);
        }
        public static void Error(object obj)
        {
            if (!logConfig.IsEnable) { return; }
            string message = DecorateLog(obj.ToString());
            PrintLog(message, LogColor.Red);
            WriteToFile("[Error]" + message);
        }
    }
}