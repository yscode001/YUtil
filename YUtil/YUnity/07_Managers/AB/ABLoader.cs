// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-17
// ------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using YUnityAndEditorCommon;

namespace YUnity
{
    #region Init
    public static partial class ABLoader
    {
        /// <summary>
        /// 真正存放bundle包的路径
        /// </summary>
        public static string BundlePath { get; private set; }

        private static AssetBundleManifest Manifest;

        /// <summary>
        /// 热更前初始化，主要是初始化路径
        /// </summary>
        /// <param name="bundlePath">bundle包所在路径</param>
        /// <param name="createBundleDirectory">是否需要强制创建bundle包所在的目录</param>
        public static void InitBeforeHotUpdate(string bundlePath, bool createBundleDirectory = true)
        {
            if (string.IsNullOrWhiteSpace(bundlePath))
            {
                throw new System.Exception("ABLoader-InitBeforeHotUpdate：bundlePath不能为空");
            }
            BundlePath = bundlePath.EndsWith("/") ? bundlePath : bundlePath + "/";
            if (createBundleDirectory)
            {
                if (!Directory.Exists(BundlePath))
                {
                    Directory.CreateDirectory(BundlePath);
                }
            }
        }

        /// <summary>
        /// 热更后初始化，主要是初始化依赖文件
        /// </summary>
        /// <param name="complete"></param>
        /// <exception cref="System.Exception"></exception>
        public static void InitAfterHotUpdate(Action complete)
        {
            ABLoadUtil.Instance.LoadAssetBundle(BundlePath + ABHelper.ManifestBundleName, (bytes) =>
            {
                /*
                if (manifestAB == null)
                {
                    throw new System.Exception($"ABLoader-Init：{ABHelper.ManifestBundleName}，这个bundle包不存在");
                }
                else
                {
                    Manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                    if (Manifest == null)
                    {
                        throw new System.Exception($"ABLoader-Init：{ABHelper.ManifestBundleName}，这个bundle包错误，取不到里面的AssetBundleManifest");
                    }
                    manifestAB.Unload(false);
                }
                complete?.Invoke();
                */
                if (bytes != null && bytes.Length > 0)
                {
                    AssetBundle manifestBundle = AssetBundle.LoadFromMemory(bytes);
                    if (manifestBundle == null)
                    {
                        throw new System.Exception($"ABLoader-Init：{ABHelper.ManifestBundleName}，这个bundle包不存在");
                    }
                    Manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                    if (Manifest == null)
                    {
                        throw new System.Exception($"ABLoader-Init：{ABHelper.ManifestBundleName}，这个bundle包错误，取不到里面的AssetBundleManifest");
                    }
                    manifestBundle.Unload(false);
                }
                complete?.Invoke();
            });
        }
    }
    #endregion

    #region 获取依赖项
    public static partial class ABLoader
    {
        public static string[] GetAllDependencies(string bundleName)
        {
            if (Manifest == null)
            {
                throw new System.Exception($"ABLoader-GetAllDependencies：Manifest未初始化，请先调用InitAfterHotUpdate进行初始化");
            }
            else
            {
                return Manifest.GetAllDependencies(ABHelper.GetAssetBundleName(bundleName));
            }
        }
    }
    #endregion

    #region LoadAssetBundle
    public static partial class ABLoader
    {
        private static void LoadDependencies(string[] dependencies, Action complete)
        {
            if (dependencies == null || dependencies.Length == 0)
            {
                complete?.Invoke();
                return;
            }
            // 先排除已加载的
            List<string> depList = new List<string>();
            var loadedList = ABLoadUtil.Instance.GetLoadedAssetBundleNameList();
            foreach (var dep in dependencies)
            {
                if (!loadedList.Contains(dep) && !depList.Contains(dep))
                {
                    depList.Add(dep);
                }
            }
            // 没有需要加载的依赖项
            if (depList.Count == 0)
            {
                complete?.Invoke();
                return;
            }
            // 开始加载依赖项
            int loadCount = 0;
            foreach (var dep in depList)
            {
                /*
                ABLoadUtil.Instance.LoadAssetBundle2(BundlePath + dep, (assetbundle) =>
                {
                    // 全部加载后完成回调
                    loadCount += 1;
                    if (loadCount >= depList.Count)
                    {
                        complete?.Invoke();
                        return;
                    }
                });
                */
                ABLoadUtil.Instance.LoadAssetBundle(BundlePath + dep, (bytes) =>
                {
                    // 为避免重复加载再次判断
                    if (bytes != null && bytes.Length > 0)
                    {
                        var loadedList2 = ABLoadUtil.Instance.GetLoadedAssetBundleNameList();
                        if (!loadedList2.Contains(dep))
                        {
                            AssetBundle.LoadFromMemory(bytes);
                        }
                    }
                    // 全部加载后完成回调
                    loadCount += 1;
                    if (loadCount >= depList.Count)
                    {
                        complete?.Invoke();
                        return;
                    }
                });
            }
        }

        public static void LoadAssetBundle(string bundleName, Action<AssetBundle> complete)
        {
            if (string.IsNullOrWhiteSpace(bundleName) || complete == null)
            {
                complete?.Invoke(null);
                return;
            }
            string abName = ABHelper.GetAssetBundleName(bundleName);
            var loadedABList = ABLoadUtil.Instance.GetLoadedAssetBundleList();
            var loadedBundle = loadedABList.FirstOrDefault(m => m.name == abName);
            if (loadedBundle != null)
            {
                // 先从缓存中查找
                complete?.Invoke(loadedBundle);
                return;
            }
            // 缓存中没有再加载
            LoadDependencies(Manifest.GetAllDependencies(abName), () =>
            {
                // 加载完依赖，再次判断缓存
                loadedABList = ABLoadUtil.Instance.GetLoadedAssetBundleList();
                loadedBundle = loadedABList.FirstOrDefault(m => m.name == abName);
                if (loadedBundle != null)
                {
                    complete?.Invoke(loadedBundle);
                    return;
                }
                // 加载
                /*
                ABLoadUtil.Instance.LoadAssetBundle2(BundlePath + abName, (assetbundle) =>
                {
                    if (assetbundle == null)
                    {
                        complete?.Invoke(null);
                    }
                    else
                    {
                        complete?.Invoke(assetbundle);
                    }
                });
                */
                ABLoadUtil.Instance.LoadAssetBundle(BundlePath + abName, (bytes) =>
                {
                    if (bytes != null && bytes.Length > 0)
                    {
                        // 加载完成，再次判断缓存
                        loadedABList = ABLoadUtil.Instance.GetLoadedAssetBundleList();
                        loadedBundle = loadedABList.FirstOrDefault(m => m.name == abName);
                        complete?.Invoke(loadedBundle ?? AssetBundle.LoadFromMemory(bytes));
                    }
                    else
                    {
                        complete?.Invoke(null);
                    }
                });
            });
        }
    }
    #endregion

    #region LoadAsset
    public static partial class ABLoader
    {
        public static void LoadAsset<T>(string bundleName, string assetName, Action<T> complete) where T : UnityEngine.Object
        {
            if (string.IsNullOrWhiteSpace(bundleName) || string.IsNullOrWhiteSpace(assetName) || complete == null)
            {
                complete?.Invoke(null);
                return;
            }
            LoadAssetBundle(bundleName, (ab) =>
            {
                ABLoadUtil.Instance.LoadAsset<T>(ab, assetName, complete);
            });
        }
        public static void LoadPrefab(string bundleName, string assetName, Action<GameObject> complete)
        {
            LoadAsset<GameObject>(bundleName, assetName, complete);
        }
        public static void LoadAudioClip(string bundleName, string assetName, Action<AudioClip> complete)
        {
            LoadAsset<AudioClip>(bundleName, assetName, complete);
        }
        public static void LoadMaterial(string bundleName, string assetName, Action<Material> complete)
        {
            LoadAsset<Material>(bundleName, assetName, complete);
        }
        public static void LoadSprite(string bundleName, string assetName, Action<Sprite> complete)
        {
            LoadAsset<Sprite>(bundleName, assetName, complete);
        }
        public static void LoadTexture(string bundleName, string assetName, Action<Texture> complete)
        {
            LoadAsset<Texture>(bundleName, assetName, complete);
        }
        public static void LoadTextAsset(string bundleName, string assetName, Action<TextAsset> complete)
        {
            LoadAsset<TextAsset>(bundleName, assetName, complete);
        }
        public static void LoadTextAssetContent(string bundleName, string assetName, Action<string> complete)
        {
            LoadAsset<TextAsset>(bundleName, assetName, (textAsset) =>
            {
                if (textAsset != null && !string.IsNullOrWhiteSpace(textAsset.text))
                {
                    complete?.Invoke(textAsset.text);
                }
                else
                {
                    complete?.Invoke(null);
                }
            });
        }
    }
    #endregion

    #region LoadInstantiate
    public static partial class ABLoader
    {
        public static void LoadGameObjectInstantiate(string bundleName, string assetName, Transform parent, Action<GameObject> complete)
        {
            LoadPrefab(bundleName, assetName, (prefab) =>
            {
                if (prefab == null)
                {
                    complete?.Invoke(null);
                }
                else
                {
                    GameObject go = GameObject.Instantiate(prefab, parent, false);
                    go.name = assetName;
                    go.transform.ResetLocal();
                    complete?.Invoke(go);
                }
            });
        }
        public static void LoadGameObjectInstantiate(string bundleName, string assetName, Transform parent, Vector3 localPosition, Vector3 localEulerAngles, Vector3 localScale, Action<GameObject> complete)
        {
            LoadPrefab(bundleName, assetName, (prefab) =>
            {
                if (prefab == null)
                {
                    complete?.Invoke(null);
                }
                else
                {
                    GameObject go = GameObject.Instantiate(prefab, parent, false);
                    go.name = assetName;
                    go.transform.localPosition = localPosition;
                    go.transform.localEulerAngles = localEulerAngles;
                    go.transform.localScale = localScale;
                    complete?.Invoke(go);
                }
            });
        }
    }
    #endregion

    #region Clear
    public static partial class ABLoader
    {
        /// <summary>
        /// 清理所有bundle资源
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void ClearBundle()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(BundlePath);
            if (directoryInfo == null)
            {
                Debug.Log("ABLoader-ClearBundle：BundlePath对应的目录不存在");
            }
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            if (fileInfos == null || fileInfos.Length <= 0)
            {
                return;
            }
            for (int i = fileInfos.Length - 1; i >= 0; i--)
            {
                File.Delete(fileInfos[i].FullName);
            }
        }

        /// <summary>
        /// 清理指定名称的bundle资源
        /// </summary>
        /// <param name="bundleName">指定的bundle的名称</param>
        public static void ClearBundle(string bundleName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(BundlePath);
            if (directoryInfo == null)
            {
                Debug.Log("ABLoader-ClearBundle：BundlePath对应的目录不存在");
            }
            string path = Path.Combine(BundlePath, ABHelper.GetAssetBundleName(bundleName));
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
    #endregion

    #region 清单文件
    public static partial class ABLoader
    {
        /// <summary>
        /// 生成的资源包清单
        /// </summary>
        public static string ABBundleFileListFullPath => BundlePath + ABHelper.ABBundleFilesName;

        /// <summary>
        /// 保存资源包清单文件
        /// </summary>
        /// <param name="bytes"></param>
        public static void SaveBundleFileList(byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0)
            {
                return;
            }
            if (File.Exists(ABBundleFileListFullPath))
            {
                File.Delete(ABBundleFileListFullPath);
            }
            File.WriteAllBytes(ABBundleFileListFullPath, bytes);
        }

        /// <summary>
        /// 保存资源包清单文件
        /// </summary>
        /// <param name="bundleFileList"></param>
        public static void SaveBundleFileList(ABLoadBundleFileList bundleFileList)
        {
            SaveBundleFileList(bundleFileList.Serialize());
        }

        /// <summary>
        /// 加载bundle清单文件
        /// </summary>
        /// <returns></returns>
        public static ABLoadBundleFileList LoadBundleFileList()
        {
            ABLoadBundleFileList bundleList = new ABLoadBundleFileList();
            if (File.Exists(ABBundleFileListFullPath))
            {
                try
                {
                    bundleList = JsonConvert.DeserializeObject<ABLoadBundleFileList>(File.ReadAllText(ABBundleFileListFullPath, Encoding.UTF8));
                }
                catch (Exception e)
                {
                    Debug.Log($"ABLoader-LoadBundleFileList：加载ABLoadBundleFileList时失败：{e}");
                }
            }
            return bundleList;
        }
    }
    #endregion

    #region 获取已加载的Bundle包
    public static partial class ABLoader
    {
        public static List<string> GetLoadedAssetBundleNameList()
        {
            return ABLoadUtil.Instance.GetLoadedAssetBundleNameList();
        }

        public static IEnumerable<AssetBundle> GetLoadedAssetBundleList()
        {
            return ABLoadUtil.Instance.GetLoadedAssetBundleList();
        }

        public static AssetBundle GetLoadedAssetBundle(string loadedBundleName)
        {
            if (string.IsNullOrWhiteSpace(loadedBundleName))
            {
                return null;
            }
            return ABLoadUtil.Instance.GetLoadedAssetBundleList().FirstOrDefault(m => m.name == loadedBundleName);
        }
    }
    #endregion
    #region 卸载已加载的Bundle包
    public static partial class ABLoader
    {
        /// <summary>
        /// 同步卸载AssetBundle
        /// </summary>
        /// <param name="loadedBundleName"></param>
        /// <param name="unloadAllLoadedObjects"></param>
        public static void UnloadAssetBundle(string loadedBundleName, bool unloadAllLoadedObjects)
        {
            if (string.IsNullOrWhiteSpace(loadedBundleName))
            {
                return;
            }
            AssetBundle ab = ABLoadUtil.Instance.GetLoadedAssetBundleList().FirstOrDefault(m => m.name == loadedBundleName);
            if (ab == null)
            {
                return;
            }
            ab.Unload(unloadAllLoadedObjects);
        }

        /// <summary>
        /// 异步卸载AssetBundle，返回值可能为空
        /// </summary>
        /// <param name="loadedBundleName"></param>
        /// <param name="unloadAllLoadedObjects"></param>
        /// <returns></returns>
        public static AsyncOperation UnloadAsyncAssetBundle(string loadedBundleName, bool unloadAllLoadedObjects)
        {
            if (string.IsNullOrWhiteSpace(loadedBundleName))
            {
                return null;
            }
            AssetBundle ab = ABLoadUtil.Instance.GetLoadedAssetBundleList().FirstOrDefault(m => m.name == loadedBundleName);
            if (ab == null)
            {
                return null;
            }
            return ab.UnloadAsync(unloadAllLoadedObjects);
        }

        /// <summary>
        /// 同步卸载所有的AssetBundle
        /// </summary>
        /// <param name="unloadAllLoadedObjects"></param>
        public static void UnloadAllAssetBundle(bool unloadAllLoadedObjects)
        {
            var ablist = ABLoadUtil.Instance.GetLoadedAssetBundleList();
            foreach (var ab in ablist)
            {
                ab.Unload(unloadAllLoadedObjects);
            }
        }
    }
    #endregion
}