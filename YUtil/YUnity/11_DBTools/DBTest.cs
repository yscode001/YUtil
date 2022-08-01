using UnityEngine;
using YUnity;

public class DBTest : MonoBehaviour
{
    private void Start()
    {
        // 创建数据库：YSDB.db
        DB db = new DB("data source=" + Application.streamingAssetsPath + "/YSDB.db", (logstr) =>
        {
            Debug.Log(logstr);
        });

        // 创建数据表
        db.CreateTable("user", new string[] { "name", "password" }, new string[] { "text", "text" });

        // 添加数据
        db.Insert("user", new string[] { "ys", "111111" });

        // 查询
        db.Select("user");
        while (db.Reader.Read())
        {
            Debug.Log(db.Reader.GetString(db.Reader.GetOrdinal("name")) + db.Reader.GetString(db.Reader.GetOrdinal("password")));
        }

        // 关闭数据库连接
        db.CloseDBConnection();
    }
}