// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-6-30
// ------------------------------

using System;

namespace YUtilEditor
{
    [Serializable]
    public struct ABBuiledBundle
    {
        /// <summary>
        /// bundle包的名字(带扩展名)
        /// </summary>
        public string BundleName;

        /// <summary>
        /// 文件大小(单位字节)
        /// </summary>
        public long FileSize;

        /// <summary>
        /// 文件的md5值
        /// </summary>
        public string FileMD5;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="bundleName">bundle包的名字(带扩展名)</param>
        /// <param name="fileSize">文件大小(单位字节)</param>
        /// <param name="fileMD5">文件的md5值</param>
        public ABBuiledBundle(String bundleName, long fileSize, string fileMD5)
        {
            BundleName = bundleName;
            FileSize = fileSize;
            FileMD5 = fileMD5;
        }
    }
}