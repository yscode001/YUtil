using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YCSharp;

namespace YUtilEditor
{
    [Serializable]
    public class ABBuiledBundleFileList
    {
        public List<ABInfo> ABList { get; private set; } = new List<ABInfo>();
        public ABBuiledBundleFileList() { }

        public void Add(ABInfo abInfo)
        {
            if (abInfo.IsEmpty() || ABList.Contains(abInfo))
            {
                return;
            }
            ABList.Add(abInfo);
        }
        public string Serialize()
        {
            if (ABList == null || ABList.Count == 0)
            {
                throw new Exception("没有要保存的bundle清单，无法序列化");
            }
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}