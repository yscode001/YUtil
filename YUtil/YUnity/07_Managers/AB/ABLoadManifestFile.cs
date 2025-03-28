using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YCSharp;

namespace YUnity
{
    [Serializable]
    public partial class ABLoadManifestFile
    {
        public List<ABInfo> AssetBundles = new List<ABInfo>();
        public ABLoadManifestFile() { }

        public byte[] Serialize()
        {
            if (AssetBundles == null || AssetBundles.Count == 0)
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
            ABLoader.SaveManifestFile(this);
        }

        public void AddOrUpdateABInfo(ABInfo abInfo)
        {
            foreach (var item in AssetBundles)
            {
                if (ABHelper.GetAssetBundleName(item.AssetBundleName) == ABHelper.GetAssetBundleName(abInfo.AssetBundleName))
                {
                    item.EditFileInfo(abInfo.FileSize);
                    return;
                }
            }
            AssetBundles.Add(abInfo);
        }
        public void DeleteABInfo(string assetBundleName)
        {
            for (int i = AssetBundles.Count - 1; i >= 0; i--)
            {
                if (ABHelper.GetAssetBundleName(AssetBundles[i].AssetBundleName) == ABHelper.GetAssetBundleName(assetBundleName))
                {
                    AssetBundles.RemoveAt(i);
                }
            }
        }
    }

    public partial class ABLoadManifestFile
    {
        /// <summary>
        /// 比较本地和远端的bundle清单文件，获取需要下载的bundle清单
        /// </summary>
        /// <param name="local">本地的bundle清单文件</param>
        /// <param name="remote">远端的bundle清单文件</param>
        /// <returns></returns>
        public static List<ABInfo> CompareAndGetCanDownloadFiles(ABLoadManifestFile local, ABLoadManifestFile remote)
        {
            if (remote == null || remote.AssetBundles == null || remote.AssetBundles.Count <= 0)
            {
                // 远端没有资源，直接返回null
                return null;
            }
            if (local == null || local.AssetBundles == null || local.AssetBundles.Count <= 0)
            {
                // 本地没有资源，直接返回远端的所有资源
                return remote.AssetBundles;
            }
            List<ABInfo> result = new List<ABInfo>();
            foreach (var remoteItem in remote.AssetBundles)
            {
                if (remoteItem.IsEmpty() || result.Contains(remoteItem) || Contains(local.AssetBundles, remoteItem))
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