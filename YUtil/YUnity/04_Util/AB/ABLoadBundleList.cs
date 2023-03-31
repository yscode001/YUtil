// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-20
// ------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

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

        public static bool operator ==(ABLoadBundle lhs, ABLoadBundle rhs)
        {
            return lhs != null && rhs != null &&
                   lhs.BundleName == rhs.BundleName &&
                   lhs.FileSize == rhs.FileSize &&
                   lhs.FileMD5 == rhs.FileMD5;
        }
        public static bool operator !=(ABLoadBundle lhs, ABLoadBundle rhs)
        {
            return lhs == null || rhs != null ||
                   lhs.BundleName != rhs.BundleName ||
                   lhs.FileSize != rhs.FileSize ||
                   lhs.FileMD5 != rhs.FileMD5;
        }
    }

    [Serializable]
    public partial class ABLoadBundleList
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

        public string Serialize()
        {
            if (BundleList == null || BundleList.Count <= 0)
            {
                return null;
            }
            return JsonConvert.SerializeObject(this);
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
                if (result.Contains(remoteItem)) { continue; }
                var searchItem = local.BundleList.FirstOrDefault(m => m == remoteItem);
                if (searchItem != null && !string.IsNullOrWhiteSpace(searchItem.BundleName) && !string.IsNullOrWhiteSpace(searchItem.FileMD5) && searchItem.FileSize > 0)
                {
                    // 搜到了，说明本地已存在，直接跳过
                    continue;
                }
                result.Add(remoteItem);
            }
            return result;
        }
    }
}