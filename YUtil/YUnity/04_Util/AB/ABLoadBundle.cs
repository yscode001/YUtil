// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-6-30
// ------------------------------

using System;

namespace YUnity
{
    [Serializable]
    public struct ABLoadBundle
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

        public static bool operator ==(ABLoadBundle lhs, ABLoadBundle rhs)
        {
            return lhs.BundleName == rhs.BundleName &&
                   lhs.FileSize == rhs.FileSize &&
                   lhs.FileMD5 == rhs.FileMD5;
        }
        public static bool operator !=(ABLoadBundle lhs, ABLoadBundle rhs)
        {
            return lhs.BundleName != rhs.BundleName ||
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