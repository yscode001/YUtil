// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-20
// ------------------------------

using System;
using System.Collections.Generic;

namespace YUnity
{
    [Serializable]
    public class ABLoadBundle
    {
        public string BundleName;

        /// <summary>
        /// 文件大小(单位字节)
        /// </summary>
        public long FileSize;

        public string FileMD5;

        public ABLoadBundle() { }
        public ABLoadBundle(string bundleName, long fileSize, string fileMD5)
        {
            BundleName = bundleName;
            FileSize = fileSize;
            FileMD5 = fileMD5;
        }
    }

    [Serializable]
    public class ABLoadBundleList
    {
        public ABLoadBundleList()
        {
            BundleList = new List<ABLoadBundle>();
        }

        public List<ABLoadBundle> BundleList = new List<ABLoadBundle>();

        /// <summary>
        /// 所有的bundle包的大小总和(单位字节)
        /// </summary>
        public long FileSizeSum
        {
            get
            {
                long size = 0;
                foreach (var item in BundleList)
                {
                    if (item != null && !string.IsNullOrWhiteSpace(item.BundleName) && item.FileSize > 0 && !string.IsNullOrWhiteSpace(item.FileMD5))
                    {
                        size += item.FileSize;
                    }
                }
                return size;
            }
        }
    }
}