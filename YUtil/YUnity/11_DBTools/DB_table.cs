using Mono.Data.Sqlite;

namespace YUnity
{
    public partial class DB
    {
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="cols">列集合</param>
        /// <param name="colTypes">列类型集合</param>
        /// <returns></returns>
        public SqliteDataReader CreateTable(string tableName, string[] cols, string[] colTypes)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new SqliteException("DB，Create Table Error：tableName canot be null");
            }
            if (cols == null || cols.Length <= 0 ||
                colTypes == null || colTypes.Length <= 0 ||
                cols.Length != colTypes.Length)
            {
                throw new SqliteException("DB，Create Table Error：cols or coltypes is not correct");
            }
            string query = "CREATE TABLE " + tableName + " (" + cols[0] + " " + colTypes[0];
            for (int i = 1; i < cols.Length; ++i)
            {
                query += ", " + cols[i] + " " + colTypes[i];
            }
            query += ")";
            return Execute(query);
        }
    }
}