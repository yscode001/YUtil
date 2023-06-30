// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-20
// ------------------------------

using System;
using System.Collections.Generic;

namespace YUnity
{
    [Serializable]
    public struct ABLoadBundle
    {
        public string BundleName;

        /// <summary>
        /// 文件大小(单位字节)
        /// </summary>
        public long FileSize;

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

    [Serializable]
    public partial class ABLoadBundleList
    {
        public ABLoadBundleList() { }

        public List<ABLoadBundle> BundleList;

        /// <summary>
        /// 所有的bundle包的大小总和(单位字节)
        /// </summary>
        public long FileSizeSum
        {
            get
            {
                long size = 0;
                if (BundleList != null && BundleList.Count > 0)
                {
                    foreach (var item in BundleList)
                    {
                        if (item != null && !string.IsNullOrWhiteSpace(item.BundleName) && item.FileSize > 0 && !string.IsNullOrWhiteSpace(item.FileMD5))
                        {
                            size += item.FileSize;
                        }
                    }
                }
                return size;
            }
        }
    }

    public partial class ABLoadBundleList
    {
        /// <summary>
        /// 比较本地和远端资源，获取可以下载的文件列表
        /// </summary>
        /// <param name="local"></param>
        /// <param name="remote"></param>
        /// <returns></returns>
        public static List<ABLoadBundle> CompareAndGetCanDownloadFiles(ABLoadBundleList local, ABLoadBundleList remote)
        {
            if (remote == null || remote.BundleList == null || remote.BundleList.Count <= 0)
            {
                // 远端没有资源，直接返回null
                return null;
            }
            if (local == null || local.BundleList == null || local.BundleList.Count <= 0)
            {
                // 本地没有资源，直接返回远端的所有资源
                return remote.BundleList;
            }
            List<ABLoadBundle> result = new List<ABLoadBundle>();
            foreach (var remoteItem in remote.BundleList)
            {
                if (result.Contains(remoteItem) || Contains(local.BundleList, remoteItem)) { continue; }
                result.Add(remoteItem);
            }
            return result;
        }
        private static bool Contains(List<ABLoadBundle> list, ABLoadBundle item)
        {
            foreach (var listItem in list)
            {
                if (listItem == item)
                {
                    return true;
                }
            }
            return false;
        }
    }
}