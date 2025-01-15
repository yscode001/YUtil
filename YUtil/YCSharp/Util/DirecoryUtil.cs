using System.IO;

namespace YCSharp
{
    public class DirecoryUtil
    {
        /// <summary>
        /// 获取最后一级文件夹的名字
        /// </summary>
        /// <param name="directoryFullPath">文件夹完整路径(带不带/都可以)</param>
        /// <returns></returns>
        public static string GetLastDirectoryName(string directoryFullPath)
        {
            // 有可能是一个网址目录，所以这里不能判断文件夹是否存在
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryFullPath);
            return directoryInfo.Name;
        }

        /// <summary>
        /// 删除目标文件夹下的所有文件和子文件夹，使之变为一个空文件夹
        /// </summary>
        /// <param name="directoryFullPath"></param>
        public static void ClearAllFilesAndSubDirectorys(string directoryFullPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryFullPath);
            if (!directoryInfo.Exists)
            {
                return;
            }

            // 删除所有文件
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                // 去除文件的只读属性
                file.Attributes = FileAttributes.Normal;
                file.Delete();
            }

            // 删除所有子文件夹
            foreach (DirectoryInfo subDir in directoryInfo.GetDirectories())
            {
                // 去除文件夹和子文件的只读属性
                subDir.Attributes = FileAttributes.Normal & FileAttributes.Directory;
                subDir.Delete();
            }

            // 删除这个空文件夹
            directoryInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            directoryInfo.Delete();
        }
    }
}