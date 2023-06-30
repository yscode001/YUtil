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
    public class ABBuiledBundleFileList
    {
        public List<ABBuiledBundle> BundleList = new List<ABBuiledBundle>();
        public ABBuiledBundleFileList() { }

        public void Add(ABBuiledBundle builedBundle)
        {
            if (string.IsNullOrWhiteSpace(builedBundle.BundleName) || builedBundle.FileSize <= 0 || string.IsNullOrWhiteSpace(builedBundle.FileMD5) || BundleList.Contains(builedBundle))
            {
                return;
            }
            BundleList.Add(builedBundle);
        }
        public string Serialize()
        {
            if (BundleList == null || BundleList.Count <= 0)
            {
                throw new Exception("ABBuiledBundleFileList-Serialize：没有要保存的bundle清单，无法序列化");
            }
            return JsonConvert.SerializeObject(this);
        }
    }
}