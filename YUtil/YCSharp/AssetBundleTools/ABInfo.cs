using System;

namespace YCSharp
{
    [Serializable]
    public struct ABInfo
    {
        /// <summary>
        /// bundle包的名字(小写带扩展名，如：audio.unity3d)
        /// </summary>
        public string AssetBundleName;

        /// <summary>
        /// 文件大小(单位字节)
        /// </summary>
        public long FileSize;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="assetBundleName">bundle包的名字</param>
        /// <param name="fileSize">文件大小(单位字节)</param>
        public ABInfo(String assetBundleName, long fileSize)
        {
            AssetBundleName = ABHelper.GetAssetBundleName(assetBundleName);
            FileSize = fileSize;
        }

        public void EditFileInfo(long fileSize)
        {
            FileSize = fileSize;
        }

        public bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(AssetBundleName) || FileSize <= 0;
        }

        public static bool operator ==(ABInfo lhs, ABInfo rhs)
        {
            return lhs.AssetBundleName == rhs.AssetBundleName &&
                   lhs.FileSize == rhs.FileSize;
        }
        public static bool operator !=(ABInfo lhs, ABInfo rhs)
        {
            return lhs.AssetBundleName != rhs.AssetBundleName ||
                   lhs.FileSize != rhs.FileSize;
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