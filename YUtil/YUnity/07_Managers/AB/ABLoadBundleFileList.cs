// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-20
// ------------------------------

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YUnityAndEditorCommon;

namespace YUnity
{
    [Serializable]
    public partial class ABLoadBundleFileList
    {
        public List<ABInfo> ABList = new List<ABInfo>();
        public ABLoadBundleFileList() { }

        public byte[] Serialize()
        {
            if (ABList == null || ABList.Count == 0)
            {
                return null;
            }
            else
            {
                return System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
            }
        }

        /// <summary>
        /// 资源包清单文件保存在硬盘上
        /// </summary>
        public void SaveToHardDisk()
        {
            ABLoader.SaveBundleFileList(this);
        }

        public void AddOrUpdateABInfo(ABInfo abInfo)
        {
            foreach (var item in ABList)
            {
                if (ABHelper.GetAssetBundleName(item.AssetBundleName) == ABHelper.GetAssetBundleName(abInfo.AssetBundleName))
                {
                    item.EditFileInfo(abInfo.FileSize, abInfo.FileMD5);
                    return;
                }
            }
            ABList.Add(abInfo);
        }
        public void DeleteABInfo(string assetBundleName)
        {
            for (int i = ABList.Count - 1; i >= 0; i--)
            {
                ABInfo info = ABList[i];
                if (ABHelper.GetAssetBundleName(info.AssetBundleName) == ABHelper.GetAssetBundleName(assetBundleName))
                {
                    ABList.RemoveAt(i);
                }
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
        public static List<ABInfo> CompareAndGetCanDownloadFiles(ABLoadBundleFileList local, ABLoadBundleFileList remote)
        {
            if (remote == null || remote.ABList == null || remote.ABList.Count <= 0)
            {
                // 远端没有资源，直接返回null
                return null;
            }
            if (local == null || local.ABList == null || local.ABList.Count <= 0)
            {
                // 本地没有资源，直接返回远端的所有资源
                return remote.ABList;
            }
            List<ABInfo> result = new List<ABInfo>();
            foreach (var remoteItem in remote.ABList)
            {
                if (remoteItem.IsEmpty() || result.Contains(remoteItem) || Contains(local.ABList, remoteItem))
                {
                    continue;
                }
                result.Add(remoteItem);
            }
            return result;
        }
        private static bool Contains(List<ABInfo> list, ABInfo item)
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