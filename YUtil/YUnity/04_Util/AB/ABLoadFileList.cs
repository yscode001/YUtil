// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-20
// ------------------------------

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace YUnity
{
    [Serializable]
    public class ABLoadFile
    {
        public string BundleName;
        public List<string> AssetsName;
    }

    [Serializable]
    public class ABLoadFileList
    {
        public List<ABLoadFile> FileList = new List<ABLoadFile>();

        public string Serialize()
        {
            if (FileList == null || FileList.Count <= 0)
            {
                return null;
            }
            return JsonConvert.SerializeObject(this);
        }
    }
}