using Mono.Data.Sqlite;

namespace YUnity
{
    public partial class DB
    {
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="cols">列名称集合</param>
        /// <param name="values">列对应的值集合</param>
        /// <param name="selectkey">查询的key</param>
        /// <param name="selectvalue">查询的value</param>
        /// <returns></returns>
        public SqliteDataReader Update(string tableName, string[] cols, string[] values, string selectkey, string selectvalue)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new SqliteException("DB，Update Error：tableName canot be null");
            }
            if (cols == null || cols.Length <= 0 ||
               values == null || values.Length <= 0 ||
               cols.Length != values.Length)
            {
                throw new SqliteException("DB，Update Error：cols or values is not correct");
            }
            if (string.IsNullOrWhiteSpace(selectkey) || string.IsNullOrWhiteSpace(selectvalue))
            {
                throw new SqliteException("DB，Update Error：selectkey and selectvalue canot be null");
            }
            string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + "'" + values[0] + "'";
            for (int i = 1; i < values.Length; ++i)
            {
                query += ", " + cols[i] + " =" + "'" + values[i] + "'";
            }
            query += " WHERE " + selectkey + " = " + "'" + selectvalue + "'" + " ";
            return Execute(query);
        }
    }
}