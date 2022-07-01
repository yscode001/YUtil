// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-2
// ------------------------------
using System.IO;

namespace YCSharp
{
    /// <summary>
    /// 文件工具
    /// </summary>
    public class FileUtil
    {
        /// <summary>
        /// 保存bytes至硬盘
        /// </summary>
        /// <param name="fileFullPath">文件完整路径</param>
        /// <param name="bytes"></param>
        /// <param name="firstDelete">是否先进行删除操作</param>
        /// <returns></returns>
        public static bool SaveBytes(string fileFullPath, byte[] bytes, bool firstDelete = true)
        {
            if (string.IsNullOrWhiteSpace(fileFullPath) || bytes == null || bytes.Length <= 0)
            {
                return false;
            }
            try
            {
                if (firstDelete)
                {
                    DeleteFile(fileFullPath);
                }
                using (FileStream fs = new FileStream(fileFullPath, FileMode.OpenOrCreate))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(bytes);
                        bw.Close();
                        fs.Close();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 从硬盘读取数据
        /// </summary>
        /// <param name="fileFullPath">文件完整路径</param>
        /// <returns></returns>
        public static byte[] ReadBytes(string fileFullPath)
        {
            if (string.IsNullOrWhiteSpace(fileFullPath) || !File.Exists(fileFullPath))
            {
                return null;
            }
            try
            {
                using (FileStream fs = new FileStream(fileFullPath, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        return br.ReadBytes((int)fs.Length);
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileFullPath">文件完整路径</param>
        public static bool DeleteFile(string fileFullPath)
        {
            try
            {
                if (File.Exists(fileFullPath))
                {
                    // 先去除文件的只读属性
                    File.SetAttributes(fileFullPath, FileAttributes.Normal);
                    File.Delete(fileFullPath);
                    return true;
                }
                else
                {
                    // 这里也返回true，文件不存在，一样达到了效果
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除文件夹及其下面的所有文件
        /// </summary>
        /// <param name="directoryFullPath">文件夹路径</param>
        public static bool DeleteDirectory(string directoryFullPath)
        {
            try
            {
                if (Directory.Exists(directoryFullPath))
                {
                    // 去除文件夹和子文件的只读属性
                    DirectoryInfo dirInfo = new DirectoryInfo(directoryFullPath);
                    dirInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                    // 去除文件的只读属性
                    File.SetAttributes(directoryFullPath, FileAttributes.Normal);

                    foreach (string path in Directory.GetFileSystemEntries(directoryFullPath))
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        else
                        {
                            DeleteDirectory(path);
                        }
                    }

                    Directory.Delete(directoryFullPath);
                    return true;
                }
                else
                {
                    // 这里也返回true，文件夹不存在，一样达到了效果
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}