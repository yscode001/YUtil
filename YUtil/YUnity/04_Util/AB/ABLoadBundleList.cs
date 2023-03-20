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
    }
}