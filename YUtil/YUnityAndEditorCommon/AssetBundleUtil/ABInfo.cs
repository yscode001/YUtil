// Author：yaoshuai
// Email：yscode@126.com
// Date：2024-3-12
// ------------------------------

using System;

namespace YUnityAndEditorCommon
{
    [Serializable]
    public struct ABInfo
    {
        /// <summary>
        /// bundle包的名字(带扩展名)
        /// </summary>
        public string AssetBundleName { get; private set; }

        /// <summary>
        /// 文件大小(单位字节)
        /// </summary>
        public long FileSize { get; private set; }

        /// <summary>
        /// 文件的md5值
        /// </summary>
        public string FileMD5 { get; private set; }

        public bool IsEmpty => string.IsNullOrWhiteSpace(AssetBundleName) || FileSize <= 0 || string.IsNullOrWhiteSpace(FileMD5);

        public bool IsNotEmpty => !IsEmpty;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="assetBundleName">bundle包的名字(带扩展名)</param>
        /// <param name="fileSize">文件大小(单位字节)</param>
        /// <param name="fileMD5">文件的md5值</param>
        public ABInfo(String assetBundleName, long fileSize, string fileMD5)
        {
            AssetBundleName = assetBundleName;
            FileSize = fileSize;
            FileMD5 = fileMD5;
        }

        public void EditFileInfo(long fileSize, string fileMD5)
        {
            FileSize = fileSize;
            FileMD5 = fileMD5;
        }

        public static bool operator ==(ABInfo lhs, ABInfo rhs)
        {
            return lhs.AssetBundleName == rhs.AssetBundleName &&
                   lhs.FileSize == rhs.FileSize &&
                   lhs.FileMD5 == rhs.FileMD5;
        }
        public static bool operator !=(ABInfo lhs, ABInfo rhs)
        {
            return lhs.AssetBundleName != rhs.AssetBundleName ||
                   lhs.FileSize != rhs.FileSize ||
                   lhs.FileMD5 != rhs.FileMD5;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}