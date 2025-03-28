using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using UnityEngine;
using YCSharp;

namespace YUnity
{
    #region Init
    public static partial class ABLoader
    {
        /// <summary>
        /// 真正存放bundle包的路径
        /// </summary>
        public static string BundlePath { get; private set; }

        public static AssetBundleManifest Manifest { get; private set; }
        public static string[] AllAssetBundle { get; private set; }
        public static string[] AllAssetBundleWithVariant { get; private set; }

        /// <summary>
        /// 热更前初始化存放Bundle包的路径
        /// </summary>
        /// <param name="bundlePath">bundle包所在路径</param>
        /// <param name="createBundleDirectory">是否需要强制创建bundle包所在的目录</param>
        /// <exception cref="System.Exception"></exception>
        public static void InitBundlePathBeforeHotUpdate(string bundlePath, bool createBundleDirectory = true)
        {
            if (string.IsNullOrWhiteSpace(bundlePath))
            {
                throw new System.Exception("bundlePath不能为空");
            }
            BundlePath = bundlePath;
            if (createBundleDirectory && !Directory.Exists(BundlePath))
            {
                Directory.CreateDirectory(BundlePath);
            }
        }

        /// <summary>
        /// 热更后初始化manifest依赖文件
        /// </summary>
        /// <param name="manifestBundleName">manifestBundleName</param>
        /// <param name="complete">完成回调</param>
        /// <exception cref="System.Exception"></exception>
        public static void InitManifestAfterHotUpdate(string manifestBundleName, Action complete)
        {
            manifestBundleName = ABHelper.GetAssetBundleName(manifestBundleName);
            ABLoadUtil.Instance.LoadAssetBundle(Path.Combine(BundlePath, manifestBundleName), (manifestAB) =>
            {
                if (manifestAB == null)
                {
                    throw new System.Exception($"{manifestBundleName}，这个bundle包不存在");
                }
                else
                {
                    Manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                    if (Manifest == null)
                    {
                        throw new System.Exception($"{manifestBundleName}，这个bundle包错误，取不到里面的AssetBundleManifest");
                    }
                    manifestAB.Unload(false);
                    AllAssetBundle = Manifest.GetAllAssetBundles();
                    AllAssetBundleWithVariant = Manifest.GetAllAssetBundlesWithVariant();
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
                throw new System.Exception($"Manifest未初始化，请先调用InitManifestAfterHotUpdate进行初始化");
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
                ABLoadUtil.Instance.LoadAssetBundle(Path.Combine(BundlePath, dep), (assetbundle) =>
                {
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

        private static AssetBundle GetAssetBundleFromCache(string bundleName)
        {
            return ABLoadUtil.Instance.GetLoadedAssetBundleList().FirstOrDefault(m => m.name == bundleName);
        }

        private static readonly object lock_loadAssetBundle = new object();
        public static void LoadAssetBundle(string bundleName, Action<AssetBundle> complete)
        {
            if (string.IsNullOrWhiteSpace(bundleName) || complete == null)
            {
                complete?.Invoke(null);
                return;
            }
            // 拼接带hashcode的AB名字：ab_hashcode.unity3d
            string abName = ABHelper.GetAssetBundleName(bundleName);
            int lastIndex = abName.LastIndexOf(ABHelper.BundleExt);
            if (lastIndex != -1)
            {
                abName = abName.Remove(lastIndex, ABHelper.BundleExt.Length);
            }
            if (!abName.Contains("_"))
            {
                abName = $"{abName}_";
            }
            // 此时，abName == ab_hashcode 或 abName = ab_
            foreach (var ab in AllAssetBundle)
            {
                if (abName.EndsWith("_") && ab.StartsWith(abName))
                {
                    // ab_
                    abName = ab;
                    break;
                }
                else if (!abName.EndsWith("_") && ab == $"{abName}{ABHelper.BundleExt}")
                {
                    // ab_hashcode
                    abName = ab;
                    break;
                }
            }
            // 先从缓存中查找
            AssetBundle cacheBundle = GetAssetBundleFromCache(abName);
            if (cacheBundle != null)
            {
                complete?.Invoke(cacheBundle);
                return;
            }
            // 先锁起来
            Monitor.Enter(lock_loadAssetBundle);
            // 解锁后再从缓存中查找
            cacheBundle = GetAssetBundleFromCache(abName);
            if (cacheBundle != null)
            {
                Monitor.Exit(lock_loadAssetBundle);
                complete?.Invoke(cacheBundle);
                return;
            }
            LoadDependencies(Manifest.GetAllDependencies(abName), () =>
            {
                // 加载完依赖，再次判断缓存
                cacheBundle = GetAssetBundleFromCache(abName);
                if (cacheBundle == null)
                {
                    ABLoadUtil.Instance.LoadAssetBundle(Path.Combine(BundlePath, abName), (assetbundle) =>
                    {
                        Monitor.Exit(lock_loadAssetBundle);
                        complete?.Invoke(assetbundle);
                    });
                }
                else
                {
                    Monitor.Exit(lock_loadAssetBundle);
                    complete?.Invoke(cacheBundle);
                }
            });
        }
    }
    #endregion

    #region LoadAsset
    public static partial class ABLoader
    {
        public static void LoadAsset<T>(string bundleName, string assetName, Action<AssetBundle, T> loaded) where T : UnityEngine.Object
        {
            if (string.IsNullOrWhiteSpace(bundleName) || string.IsNullOrWhiteSpace(assetName) || loaded == null)
            {
                loaded?.Invoke(null, null);
                return;
            }
            LoadAssetBundle(bundleName, (ab) =>
            {
                ABLoadUtil.Instance.LoadAsset<T>(ab, assetName, (asset) =>
                {
                    loaded?.Invoke(ab, asset);
                });
            });
        }
        public static void LoadPrefab(string bundleName, string assetName, Action<AssetBundle, GameObject> loaded)
        {
            LoadAsset<GameObject>(bundleName, assetName, loaded);
        }
        public static void LoadAudioClip(string bundleName, string assetName, Action<AssetBundle, AudioClip> loaded)
        {
            LoadAsset<AudioClip>(bundleName, assetName, loaded);
        }
        public static void LoadMaterial(string bundleName, string assetName, Action<AssetBundle, Material> loaded)
        {
            LoadAsset<Material>(bundleName, assetName, loaded);
        }
        public static void LoadSprite(string bundleName, string assetName, Action<AssetBundle, Sprite> loaded)
        {
            LoadAsset<Sprite>(bundleName, assetName, loaded);
        }
        public static void LoadTexture(string bundleName, string assetName, Action<AssetBundle, Texture> loaded)
        {
            LoadAsset<Texture>(bundleName, assetName, loaded);
        }
        public static void LoadTextAsset(string bundleName, string assetName, Action<AssetBundle, TextAsset> loaded)
        {
            LoadAsset<TextAsset>(bundleName, assetName, loaded);
        }
        public static void LoadTextAssetContent(string bundleName, string assetName, Action<AssetBundle, string> loaded)
        {
            LoadAsset<TextAsset>(bundleName, assetName, (ab, textAsset) =>
            {
                if (textAsset != null && !string.IsNullOrWhiteSpace(textAsset.text))
                {
                    loaded?.Invoke(ab, textAsset.text);
                }
                else
                {
                    loaded?.Invoke(ab, null);
                }
            });
        }
    }
    #endregion

    #region LoadInstantiate
    public static partial class ABLoader
    {
        public static void LoadGameObjectInstantiate(string bundleName, string assetName, Transform parent, Action<AssetBundle, GameObject> loaded)
        {
            LoadPrefab(bundleName, assetName, (ab, prefab) =>
            {
                if (prefab == null)
                {
                    loaded?.Invoke(ab, null);
                }
                else
                {
                    GameObject go = GameObject.Instantiate(prefab, parent, false);
                    go.name = assetName;
                    go.transform.ResetLocal();
                    loaded?.Invoke(ab, go);
                }
            });
        }
        public static void LoadGameObjectInstantiate(string bundleName, string assetName, Transform parent, Vector3 localPosition, Vector3 localEulerAngles, Vector3 localScale, Action<AssetBundle, GameObject> loaded)
        {
            LoadPrefab(bundleName, assetName, (ab, prefab) =>
            {
                if (prefab == null)
                {
                    loaded?.Invoke(ab, null);
                }
                else
                {
                    GameObject go = GameObject.Instantiate(prefab, parent, false);
                    go.name = assetName;
                    go.transform.localPosition = localPosition;
                    go.transform.localEulerAngles = localEulerAngles;
                    go.transform.localScale = localScale;
                    loaded?.Invoke(ab, go);
                }
            });
        }
    }
    #endregion

    #region Clear
    public static partial class ABLoader
    {
        /// <summary>
        /// 清理(删除)所有bundle包资源
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void ClearBundle()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(BundlePath);
            if (directoryInfo == null)
            {
                Debug.Log($"{BundlePath}对应的目录不存在，无需清理");
                return;
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
                Debug.Log($"{BundlePath}对应的目录不存在，无需清理");
                return;
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
        public static string ManifestFileFullPath => Path.Combine(BundlePath, ABHelper.ManifestFileName);

        /// <summary>
        /// 保存资源包清单文件
        /// </summary>
        /// <param name="bytes"></param>
        public static void SaveManifestFile(byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0)
            {
                return;
            }
            if (File.Exists(ManifestFileFullPath))
            {
                File.Delete(ManifestFileFullPath);
            }
            File.WriteAllBytes(ManifestFileFullPath, bytes);
        }

        /// <summary>
        /// 保存资源包清单文件
        /// </summary>
        /// <param name="manifestFile"></param>
        public static void SaveManifestFile(ABLoadManifestFile manifestFile)
        {
            SaveManifestFile(manifestFile.Serialize());
        }

        /// <summary>
        /// 加载bundle清单文件
        /// </summary>
        /// <returns></returns>
        public static ABLoadManifestFile LoadManifestFile()
        {
            ABLoadManifestFile manifestFile = new ABLoadManifestFile();
            if (File.Exists(ManifestFileFullPath))
            {
                try
                {
                    manifestFile = JsonConvert.DeserializeObject<ABLoadManifestFile>(File.ReadAllText(ManifestFileFullPath, Encoding.UTF8));
                }
                catch (Exception e)
                {
                    Debug.Log($"加载ManifestFile时失败：{e}");
                }
            }
            return manifestFile;
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
            AssetBundle ab = ABLoadUtil.Instance.GetLoadedAssetBundleList().FirstOrDefault(m => m.name == ABHelper.GetAssetBundleName(loadedBundleName));
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
            AssetBundle ab = ABLoadUtil.Instance.GetLoadedAssetBundleList().FirstOrDefault(m => m.name == ABHelper.GetAssetBundleName(loadedBundleName));
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