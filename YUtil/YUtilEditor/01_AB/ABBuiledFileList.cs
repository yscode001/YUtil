// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-20
// ------------------------------

/*
 打进bundle的文件清单
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace YUtilEditor
{
    [Serializable]
    public class ABBuiledFile
    {
        public string BundleName;
        public List<string> AssetsName;

        public ABBuiledFile(string bundleName)
        {
            BundleName = bundleName;
            AssetsName = new List<string>();
        }
    }

    [Serializable]
    public class ABBuiledFileList
    {
        public ABBuiledFileList()
        {
            FileList = new List<ABBuiledFile>();
        }

        public List<ABBuiledFile> FileList = new List<ABBuiledFile>();

        public void Add(string abBundleName, string[] assetsName)
        {
            if (string.IsNullOrWhiteSpace(abBundleName) || assetsName == null || assetsName.Length <= 0)
            {
                return;
            }
            ABBuiledFile builedFile = FileList.FirstOrDefault(m => m.BundleName == abBundleName);
            if (builedFile == null || string.IsNullOrWhiteSpace(builedFile.BundleName))
            {
                builedFile = new ABBuiledFile(abBundleName);
                foreach (var item in assetsName)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        builedFile.AssetsName.Add(item);
                    }
                }
                FileList.Add(builedFile);
            }
            else
            {
                foreach (var item in assetsName)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        builedFile.AssetsName.Add(item);
                    }
                }
            }
        }

        public string Serialize()
        {
            if (FileList == null || FileList.Count <= 0)
            {
                throw new Exception("ABBuiledFileList-Serialize：没有要保存的file清单，无法序列化");
            }
            return JsonConvert.SerializeObject(this);
        }
    }
}