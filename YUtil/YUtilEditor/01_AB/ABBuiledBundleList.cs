// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-20
// ------------------------------

/*
 打成的bundle清单
 */

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace YUtilEditor
{
    [Serializable]
    public class ABBuiledBundle
    {
        public string BundleName;

        /// <summary>
        /// 文件大小(单位字节)
        /// </summary>
        public long FileSize;

        public string FileMD5;

        public ABBuiledBundle() { }
        public ABBuiledBundle(string bundleName, long fileSize, string fileMD5)
        {
            BundleName = bundleName;
            FileSize = fileSize;
            FileMD5 = fileMD5;
        }
    }

    [Serializable]
    public class ABBuiledBundleList
    {
        public ABBuiledBundleList()
        {
            BundleList = new List<ABBuiledBundle>();
        }

        public List<ABBuiledBundle> BundleList = new List<ABBuiledBundle>();

        public void Add(ABBuiledBundle builedBundle)
        {
            if (builedBundle == null || string.IsNullOrWhiteSpace(builedBundle.BundleName) || builedBundle.FileSize <= 0 || string.IsNullOrWhiteSpace(builedBundle.FileMD5) || BundleList.Contains(builedBundle))
            {
                return;
            }
            BundleList.Add(builedBundle);
        }

        public string Serialize()
        {
            if (BundleList == null || BundleList.Count <= 0)
            {
                throw new Exception("ABBuiledBundleList-Serialize：没有要保存的bundle清单，无法序列化");
            }
            return JsonConvert.SerializeObject(this);
        }
    }
}