using System;
using Mono.Data.Sqlite;

namespace YUnity
{
    public partial class DB
    {
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        public SqliteDataReader Execute(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new SqliteException("DB，Execute Sql Error：sql canot be null");
            }
            LogMethod?.Invoke($"DB，Execute Sql：{sql}");
            try
            {
                Command = Connection.CreateCommand();
                Command.CommandText = sql;
                Reader = Command.ExecuteReader();
                return Reader;
            }
            catch (Exception ex)
            {
                LogMethod?.Invoke($"DB，Execute Sql Error：{sql}");
                LogMethod?.Invoke($"DB，Execute Sql Error：{ex}");
                return null;
            }
        }
    }
}