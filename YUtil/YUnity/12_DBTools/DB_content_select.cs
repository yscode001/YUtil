using Mono.Data.Sqlite;

namespace YUnity
{
    #region 查询
    public partial class DB
    {
        /// <summary>
        /// 查询整个表内容
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public SqliteDataReader Select(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new SqliteException("DB，Select Error：tableName canot be null");
            }
            string sql = "SELECT * FROM " + tableName;
            return Execute(sql);
        }
        /// <summary>
        /// 自定义查询表内容
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="items">要查询的结果集(如：Name，Age...)</param>
        /// <param name="cols">条件</param>
        /// <param name="operations">条件与值的关系</param>
        /// <param name="values">条件对应的值</param>
        /// <returns></returns>
        public SqliteDataReader QueryTableWhere(string tableName, string[] items, string[] cols, string[] operations, string[] values)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new SqliteException("DB，Select Error：tableName canot be null");
            }
            if (items == null || items.Length <= 0)
            {
                throw new SqliteException("DB，Select Error：items canot be null");
            }
            if (cols == null || cols.Length <= 0 ||
                operations == null || operations.Length <= 0 ||
              values == null || values.Length <= 0 ||
              cols.Length != operations.Length ||
              cols.Length != values.Length)
            {
                throw new SqliteException("DB，Select Error：cols or operations or values is not correct");
            }
            string query = "SELECT " + items[0];
            for (int i = 1; i < items.Length; ++i)
            {
                query += ", " + items[i];
            }
            query += " FROM " + tableName + " WHERE " + cols[0] + operations[0] + "'" + values[0] + "' ";
            for (int i = 1; i < cols.Length; ++i)
            {
                query += " AND " + cols[i] + operations[i] + "'" + values[0] + "' ";
            }
            return Execute(query);
        }
    }
    #endregion
}