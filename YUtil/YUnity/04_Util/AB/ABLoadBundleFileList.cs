// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-20
// ------------------------------

using System;
using System.Collections.Generic;

namespace YUnity
{
    [Serializable]
    public partial class ABLoadBundleFileList
    {
        public List<ABLoadBundle> BundleList = new List<ABLoadBundle>();
        public ABLoadBundleFileList() { }

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

    public partial class ABLoadBundleFileList
    {
        /// <summary>
        /// 比较本地和远端的bundle清单文件，获取需要下载的bundle清单
        /// </summary>
        /// <param name="local">本地的bundle清单文件</param>
        /// <param name="remote">远端的bundle清单文件</param>
        /// <returns></returns>
        public static List<ABLoadBundle> CompareAndGetCanDownloadFiles(ABLoadBundleFileList local, ABLoadBundleFileList remote)
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
                if (result.Contains(remoteItem) || Contains(local.BundleList, remoteItem))
                {
                    continue;
                }
                result.Add(remoteItem);
            }
            return result;
        }
        private static bool Contains(List<ABLoadBundle> list, ABLoadBundle item)
        {
            if (list == null || list.Count <= 0 || item == null)
            {
                return false;
            }
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