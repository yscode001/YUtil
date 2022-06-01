using Mono.Data.Sqlite;

namespace YUnity
{
    public partial class DB
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="values">值集合</param>
        /// <returns></returns>
        public SqliteDataReader Insert(string tableName, string[] values)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new SqliteException("DB，Insert Error：tableName canot be null");
            }
            if (values == null || values.Length <= 0)
            {
                throw new SqliteException("DB，Insert Error：values canot be null");
            }
            string query = "INSERT INTO " + tableName + " VALUES (" + "'" + values[0] + "'";
            for (int i = 1; i < values.Length; ++i)
            {
                query += ", " + "'" + values[i] + "'";
            }
            query += ")";
            return Execute(query);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="cols">列集合</param>
        /// <param name="values">值集合</param>
        /// <returns></returns>
        public SqliteDataReader Insert(string tableName, string[] cols, string[] values)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new SqliteException("DB，Insert Error：tableName canot be null");
            }
            if (cols == null || cols.Length <= 0 ||
                values == null || values.Length <= 0 ||
                cols.Length != values.Length)
            {
                throw new SqliteException("DB，Insert Error：cols or values is not correct");
            }
            string query = "INSERT INTO " + tableName + "(" + cols[0];
            for (int i = 1; i < cols.Length; ++i)
            {
                query += ", " + cols[i];
            }
            query += ") VALUES (" + values[0];
            for (int i = 1; i < values.Length; ++i)
            {
                query += ", " + values[i];
            }
            query += ")";
            return Execute(query);
        }
    }
}