// Author：yaoshuai
// Email：yscode@126.com
// Date：2021-11-25
// ------------------------------
using System;
using System.IO;
using System.Text;

namespace YUnity
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
                sb.AppendFormat(" {0}", DateTime.Now.ToString("HH:mm:ss:fff"));
            }
            sb.AppendFormat(" {0} {1}", logConfig.Separator, message);
            return sb.ToString();
        }
    }
    #endregion
    public partial class LogTool
    {
        private static string GetColorMessage(string message, LogColor color = LogColor.None)
        {
            switch (color)
            {
                case LogColor.Red: return string.Format("<color=#FF0000>{0}</color>", message);
                case LogColor.Green: return string.Format("<color=#00FF00>{0}</color>", message);
                case LogColor.Blue: return string.Format("<color=#0000FF>{0}</color>", message);
                case LogColor.Cyan: return string.Format("<color=#00FFFF>{0}</color>", message);
                case LogColor.Magenta: return string.Format("<color=#FF00FF>{0}</color>", message);
                case LogColor.Yellow: return string.Format("<color=#FFFF00>{0}</color>", message);
                default: return message;
            }
        }
        public static void Log(string message, params object[] args)
        {
            if (!logConfig.IsEnable) { return; }
            message = DecorateLog(string.Format(message, args));
            UnityEngine.Debug.Log(GetColorMessage(message));
        }
        public static void Log(object obj)
        {
            if (!logConfig.IsEnable) { return; }
            string message = DecorateLog(obj.ToString());
            UnityEngine.Debug.Log(GetColorMessage(message));
        }
        public static void ColorLog(LogColor color, string message, params object[] args)
        {
            if (!logConfig.IsEnable) { return; }
            message = DecorateLog(string.Format(message, args));
            UnityEngine.Debug.Log(GetColorMessage(message, color));
        }
        public static void ColorLog(LogColor color, object obj)
        {
            if (!logConfig.IsEnable) { return; }
            string message = DecorateLog(obj.ToString());
            UnityEngine.Debug.Log(GetColorMessage(message, color));
        }
        public static void Warning(string message, params object[] args)
        {
            if (!logConfig.IsEnable) { return; }
            message = DecorateLog(string.Format(message, args));
            UnityEngine.Debug.Log(GetColorMessage(message, LogColor.Yellow));
        }
        public static void Warning(object obj)
        {
            if (!logConfig.IsEnable) { return; }
            string message = DecorateLog(obj.ToString());
            UnityEngine.Debug.Log(GetColorMessage(message, LogColor.Yellow));
        }
        public static void Error(string message, params object[] args)
        {
            if (!logConfig.IsEnable) { return; }
            message = DecorateLog(string.Format(message, args));
            UnityEngine.Debug.Log(GetColorMessage(message, LogColor.Red));
        }
        public static void Error(object obj)
        {
            if (!logConfig.IsEnable) { return; }
            string message = DecorateLog(obj.ToString());
            UnityEngine.Debug.Log(GetColorMessage(message, LogColor.Red));
        }
    }
}