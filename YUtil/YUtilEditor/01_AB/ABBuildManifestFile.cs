using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YCSharp;

namespace YUtilEditor
{
    [Serializable]
    public class ABBuildManifestFile
    {
        public List<ABInfo> AssetBundles { get; private set; } = new List<ABInfo>();
        public ABBuildManifestFile() { }

        public void Add(ABInfo abInfo)
        {
            if (abInfo.IsEmpty() || AssetBundles.Contains(abInfo))
            {
                return;
            }
            AssetBundles.Add(abInfo);
        }
        public string Serialize()
        {
            if (AssetBundles == null || AssetBundles.Count == 0)
            {
                throw new Exception("没有要保存的bundle清单，无法序列化");
            }
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}