// Author：yaoshuai
// Email：yscode@126.com
// Date：2024-3-13
// ------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace YUnity
{
    internal partial class ABLoadUtil : MonoBehaviourBaseY
    {
        internal static ABLoadUtil Instance { get; private set; } = null;

        internal void Init()
        {
            Instance = this;
        }
    }

    #region 加载AssetBundle
    internal partial class ABLoadUtil
    {
        internal List<string> GetLoadedAssetBundleNameList()
        {
            List<string> list = new List<string>();
            var assetBundles = GetLoadedAssetBundleList();
            foreach (var ab in assetBundles)
            {
                if (ab != null && !list.Contains(ab.name))
                {
                    list.Add(ab.name);
                }
            }
            return list;
        }

        internal IEnumerable<AssetBundle> GetLoadedAssetBundleList()
        {
            var assetBundles = AssetBundle.GetAllLoadedAssetBundles();
            return assetBundles ?? (new AssetBundle[] { });
        }

        /*
        internal void LoadAssetBundle(string abFullPath, Action<byte[]> complete)
        {
            StartCoroutine(LoadAssetBundleAction(abFullPath, complete));
        }
        private IEnumerator LoadAssetBundleAction(string abFullPath, Action<byte[]> complete)
        {
            if (string.IsNullOrWhiteSpace(abFullPath))
            {
                complete?.Invoke(null);
            }
            else
            {
                var request = UnityWebRequest.Get(new System.Uri(abFullPath));
                yield return request.SendWebRequest();
                complete?.Invoke(request.downloadHandler.data);
            }
        }
        */

        internal void LoadAssetBundle2(string abFullPath, Action<AssetBundle> complete)
        {
            StartCoroutine(LoadAssetBundleAction2(abFullPath, complete));
        }
        private IEnumerator LoadAssetBundleAction2(string abFullPath, Action<AssetBundle> complete)
        {
            if (string.IsNullOrWhiteSpace(abFullPath))
            {
                complete?.Invoke(null);
            }
            else
            {
                UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(abFullPath);
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.Success)
                {
                    complete?.Invoke(DownloadHandlerAssetBundle.GetContent(request));
                }
                else
                {
                    complete?.Invoke(null);
                }
            }
        }
    }
    #endregion
    #region 加载Asset
    internal partial class ABLoadUtil
    {
        internal void LoadAsset<T>(AssetBundle assetBundle, string assetName, Action<T> complete) where T : UnityEngine.Object
        {
            StartCoroutine(LoadAssetAction(assetBundle, assetName, complete));
        }
        private IEnumerator LoadAssetAction<T>(AssetBundle assetBundle, string assetName, Action<T> complete) where T : UnityEngine.Object
        {
            if (assetBundle == null || string.IsNullOrWhiteSpace(assetName))
            {
                complete?.Invoke(null);
            }
            else
            {
                AssetBundleRequest request = assetBundle.LoadAssetAsync<T>(assetName);
                yield return request;
                if (request.asset != null && request.asset is T)
                {
                    complete?.Invoke(request.asset as T);
                }
                else
                {
                    complete?.Invoke(null);
                }
            }
        }
    }
    #endregion
}