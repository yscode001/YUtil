// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-17
// ------------------------------

using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace YUnity
{
    #region Init
    public static partial class ABLoadUtil
    {
        /// <summary>
        /// 真正存放bundle包的路径，由平台和版本号组成
        /// </summary>
        public static string BundlePath { get; private set; }

        /// <summary>
        /// bundle包的扩展名
        /// </summary>
        public const string BundleExt = ".unity3d";

        private const string ManifestBundleName = "manifest.unity3d";
        private static AssetBundleManifest Manifest;

        /// <summary>
        /// 热更前初始化，主要是初始化路径
        /// </summary>
        /// <param name="platformName">平台名称</param>
        /// <param name="bundlePath">bundle包所在路径</param>
        /// <param name="version">可选参数：资源版本号(更换版本情况：资源发生重大改变，资源的目录结构都变了。一般情况下无需更换版本号，默认：1)</param>
        public static void InitBeforeHotUpdate(string platformName, string bundlePath, UInt32 version = 1)
        {
            if (string.IsNullOrWhiteSpace(platformName) || string.IsNullOrWhiteSpace(bundlePath))
            {
                throw new System.Exception("ABLoadUtil-InitBeforeHotUpdate：platformName和bundlePath不能为空");
            }
            BundlePath = (bundlePath.EndsWith("/") ? bundlePath : bundlePath + "/") + platformName + "/" + $"Version{version}/";
            if (!Directory.Exists(BundlePath))
            {
                Directory.CreateDirectory(BundlePath);
            }
        }

        /// <summary>
        /// 热更后初始化，主要是初始化依赖文件
        /// </summary>
        public static void InitAfterHotUpdate()
        {
            AssetBundle manifestBundle = AssetBundle.LoadFromFile(BundlePath + GetBundleName(ManifestBundleName));
            if (manifestBundle == null)
            {
                throw new System.Exception($"ABLoadUtil-Init：{ManifestBundleName}，这个bundle包不存在");
            }
            Manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            if (Manifest == null)
            {
                throw new System.Exception($"ABLoadUtil-Init：{ManifestBundleName}，这个bundle包错误，取不到里面的AssetBundleManifest");
            }
            manifestBundle.Unload(false);
        }

        public static string GetBundleName(string bundleName)
        {
            if (string.IsNullOrWhiteSpace(bundleName))
            {
                throw new Exception("ABLoadUtil-GetBundleName：bundleName不能为空");
            }
            if (bundleName.EndsWith(BundleExt))
            {
                return bundleName.ToLower();
            }
            return bundleName.ToLower() + BundleExt;
        }

        /// <summary>
        /// 从新加载依赖文件
        /// </summary>
        public static void ReloadManifest()
        {
            if (Manifest == null)
            {
                InitAfterHotUpdate();
            }
        }
    }
    #endregion

    #region LoadAsset
    public static partial class ABLoadUtil
    {
        /// <summary>
        /// 获取所有的依赖包(包含依赖的依赖)
        /// </summary>
        /// <param name="bundleName">指定的bundle包的名字</param>
        /// <returns></returns>
        public static string[] GetAllDependencies(string bundleName)
        {
            if (string.IsNullOrWhiteSpace(bundleName))
            {
                throw new System.Exception("ABLoadUtil-GetAllDependencies：bundleName不能为空");
            }
            return Manifest.GetAllDependencies(GetBundleName(bundleName));
        }

        /// <summary>
        /// 加载bundle包
        /// </summary>
        /// <param name="bundleName">指定的bundle包的名字</param>
        /// <param name="handleDependencieBeforeLoad">在加载之前先处理依赖包(这里包含所有的依赖包，包含依赖的依赖)</param>
        /// <returns></returns>
        public static AssetBundle LoadAssetBundle(string bundleName, Action<string[]> handleDependencieBeforeLoad)
        {
            if (string.IsNullOrWhiteSpace(bundleName))
            {
                throw new System.Exception("ABLoadUtil-LoadAssetBundle：bundleName不能为空");
            }
            handleDependencieBeforeLoad?.Invoke(GetAllDependencies(bundleName));
            return AssetBundle.LoadFromFile(BundlePath + GetBundleName(bundleName));
        }
    }
    #endregion

    #region Clear
    public static partial class ABLoadUtil
    {
        /// <summary>
        /// 清理某一版本或所有版本的bundle资源
        /// </summary>
        /// <param name="version">相应的版本号(< 1表示清理所有版本的bundle资源)</param>
        public static void ClearVersion(UInt32 version = 0)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(BundlePath);
            if (directoryInfo == null)
            {
                Debug.Log("ABLoadUtil-ClearVersion：BundlePath对应的目录不存在");
            }
            if (version < 1)
            {
                // 清理所有版本的bundle资源(直接删除platformName对应的文件夹)
                if (Directory.Exists(directoryInfo.Parent.FullName))
                {
                    Directory.Delete(directoryInfo.Parent.FullName, true);
                }
            }
            else
            {
                // 清理指定版本的bundle资源(只删除Version对应的文件夹)
                string path = directoryInfo.Parent.FullName + $"/Version{version}/";
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        /// <summary>
        /// 清理指定版本指定名称的bundle资源
        /// </summary>
        /// <param name="bundleName">指定的bundle的名称</param>
        /// <param name="version">指定的版本号</param>
        public static void ClearBundle(string bundleName, UInt32 version)
        {
            if (string.IsNullOrWhiteSpace(bundleName) || version < 1)
            {
                Debug.Log("ABLoadUtil-ClearBundle：bundleName不能为空，version必须大于0");
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(BundlePath);
            if (directoryInfo == null)
            {
                Debug.Log("ABLoadUtil-ClearBundle：BundlePath对应的目录不存在");
            }
            string path = directoryInfo.Parent.FullName + $"/Version{version}/" + GetBundleName(bundleName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
    #endregion

    #region 清单文件
    public static partial class ABLoadUtil
    {
        /// <summary>
        /// 生成的资源包清单
        /// </summary>
        public static string ABBundleFileListFullPath => BundlePath + "ABBundleFiles.txt";

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
                    Debug.Log($"ABLoadUtil-LoadBundleFileList：加载ABLoadBundleFileList时失败：{e}");
                }
            }
            return bundleList;
        }
    }
    #endregion
}