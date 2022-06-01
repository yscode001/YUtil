using Mono.Data.Sqlite;

namespace YUnity
{
    public partial class DB
    {
        /// <summary>
        /// 删除表所有的内容
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns></returns>
        public SqliteDataReader Delete(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new SqliteException("DB，Delete Error：tableName canot be null");
            }
            string query = "DELETE FROM " + tableName;
            return Execute(query);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="cols">列集合(Or的关系)</param>
        /// <param name="values">值集合</param>
        /// <returns></returns>
        public SqliteDataReader DeleteOr(string tableName, string[] cols, string[] values)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new SqliteException("DB，Delete Error：tableName canot be null");
            }
            if (cols == null || cols.Length <= 0 ||
                values == null || values.Length <= 0 ||
                cols.Length != values.Length)
            {
                throw new SqliteException("DB，Delete Error：cols or values is not correct");
            }
            string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + values[0];
            for (int i = 1; i < values.Length; ++i)
            {
                query += " or " + cols[i] + " = " + values[i];
            }
            return Execute(query);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="cols">列集合(And的关系)</param>
        /// <param name="values">值集合</param>
        /// <returns></returns>
        public SqliteDataReader DeleteAnd(string tableName, string[] cols, string[] values)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new SqliteException("DB，Delete Error：tableName canot be null");
            }
            if (cols == null || cols.Length <= 0 ||
                values == null || values.Length <= 0 ||
                cols.Length != values.Length)
            {
                throw new SqliteException("DB，Delete Error：cols or values is not correct");
            }
            string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + values[0];
            for (int i = 1; i < values.Length; ++i)
            {
                query += " and " + cols[i] + " = " + values[i];
            }
            return Execute(query);
        }
    }
}