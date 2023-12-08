using System;
using Mono.Data.Sqlite;

namespace YUnity
{
    #region 属性与构造函数
    public partial class DB
    {
        public SqliteConnection Connection { get; private set; } = null;
        public SqliteCommand Command { get; private set; } = null;
        public SqliteDataReader Reader { get; private set; } = null;

        /// <summary>
        /// 日志打印
        /// </summary>
        private Action<string> LogMethod;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logAction">日志打印</param>
        public DB(Action<string> logAction = null)
        {
            LogMethod = logAction;
        }

        /// <summary>
        /// 构造函数，并自动建立数据库连接
        /// </summary>
        /// <param name="connection">数据库连接字符串</param>
        /// <param name="logAction">日志打印</param>
        public DB(string connection, Action<string> logAction = null)
        {
            LogMethod = logAction;
            OpenDBConnection(connection);
        }
    }
    #endregion
    #region 打开、关闭数据库连接
    public partial class DB
    {
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <param name="connection">数据库连接字符串</param>
        public void OpenDBConnection(string connection)
        {
            try
            {
                Connection = new SqliteConnection(connection);
                Connection.Open();
                LogMethod?.Invoke("DB，连接成功");
            }
            catch (Exception ex)
            {
                LogMethod?.Invoke($"DB，连接失败：{ex}");
            }
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseDBConnection()
        {
            if (Command != null)
            {
                Command.Dispose();
                Command = null;
            }
            if (Reader != null)
            {
                Reader.Dispose();
                Reader = null;
            }
            if (Connection != null)
            {
                Connection.Close();
                Connection = null;
            }
            LogMethod?.Invoke("DB，断开连接");
        }
    }
    #endregion
}