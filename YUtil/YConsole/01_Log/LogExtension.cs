// Author：yaoshuai
// Email：yscode@126.com
// Date：2021-12-21
// ------------------------------

/// <summary>
/// 方便调用
/// </summary>
public static class YLogExtension
{
    #region 普通打印
    public static void Log(this object obj, string log, params object[] args)
    {
        YConsole.LogTool.Log(log, args);
    }
    public static void Log(this object obj, object log)
    {
        YConsole.LogTool.Log(log);
    }
    #endregion
    #region 打印堆栈
    public static void Trace(this object obj, string log, params object[] args)
    {
        YConsole.LogTool.Trace(log, args);
    }
    public static void Trace(this object obj, object log)
    {
        YConsole.LogTool.Trace(log);
    }
    #endregion
    #region 彩色打印
    public static void ColorLog(this object obj, LogColor color, string log, params object[] args)
    {
        YConsole.LogTool.ColorLog(color, log, args);
    }
    public static void ColorLog(this object obj, LogColor color, object log)
    {
        YConsole.LogTool.ColorLog(color, log);
    }
    #endregion
    #region 警告
    public static void Warning(this object obj, string log, params object[] args)
    {
        YConsole.LogTool.Warning(log, args);
    }
    public static void Warning(this object obj, object log)
    {
        YConsole.LogTool.Warning(log);
    }
    #endregion
    #region 错误
    public static void Error(this object obj, string log, params object[] args)
    {
        YConsole.LogTool.Error(log, args);
    }
    public static void Error(this object obj, object log)
    {
        YConsole.LogTool.Error(log);
    }
    #endregion
}