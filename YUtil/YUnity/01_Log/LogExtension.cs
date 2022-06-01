// Author：yaoshuai
// Email：yscode@126.com
// Date：2021-11-25
// ------------------------------

/// <summary>
/// 方便调用
/// </summary>
public static class YLogExtension
{
    #region 普通打印
    public static void Log(this object obj, string log, params object[] args)
    {
        YUnity.LogTool.Log(log, args);
    }
    public static void Log(this object obj, object log)
    {
        YUnity.LogTool.Log(log);
    }
    #endregion
    #region 打印堆栈
    public static void Trace(this object obj, string log, params object[] args)
    {
        YUnity.LogTool.Trace(log, args);
    }
    public static void Trace(this object obj, object log)
    {
        YUnity.LogTool.Trace(log);
    }
    #endregion
    #region 彩色打印
    public static void ColorLog(this object obj, LogColor color, string log, params object[] args)
    {
        YUnity.LogTool.ColorLog(color, log, args);
    }
    public static void ColorLog(this object obj, LogColor color, object log)
    {
        YUnity.LogTool.ColorLog(color, log);
    }
    #endregion
    #region 警告
    public static void Warning(this object obj, string log, params object[] args)
    {
        YUnity.LogTool.Warning(log, args);
    }
    public static void Warning(this object obj, object log)
    {
        YUnity.LogTool.Warning(log);
    }
    #endregion
    #region 错误
    public static void Error(this object obj, string log, params object[] args)
    {
        YUnity.LogTool.Error(log, args);
    }
    public static void Error(this object obj, object log)
    {
        YUnity.LogTool.Error(log);
    }
    #endregion
}