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
                throw new Exception("ABBuiledBundleFileList-Serialize：没有要保存的bundle清单，无法序列化");
            }
            return JsonConvert.SerializeObject(this);
        }
    }
}