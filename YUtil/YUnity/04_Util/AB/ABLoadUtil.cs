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
        private static string BundlePath;

        private static string BundleExt;

        private static string ManifestBundleName;
        private static AssetBundleManifest Manifest;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="platformName">平台名称</param>
        /// <param name="bundlePath">bundle包所在路径</param>
        /// <param name="manifestBundleName">manifest的bundle包名，用于获取依赖关系</param>
        /// <param name="bundleExt">可选参数：bundle包的扩展名(默认：unity3d)</param>
        /// <param name="version">可选参数：资源版本号(更换版本情况：资源发生重大改变，资源的目录结构都变了。一般情况下无需更换版本号，默认：1)</param>
        public static void Init(string platformName, string bundlePath, string manifestBundleName, string bundleExt = ".unity3d", UInt32 version = 1)
        {
            if (string.IsNullOrWhiteSpace(platformName) || string.IsNullOrWhiteSpace(bundlePath) || string.IsNullOrWhiteSpace(manifestBundleName))
            {
                throw new System.Exception("AssetBundleUtil-Init：platformName和bundlePath和manifestBundleName不能为空");
            }
            string path = (bundlePath.EndsWith("/") ? bundlePath : bundlePath + "/") + platformName + "/" + $"Version{version}/";
            if (!Directory.Exists(path))
            {
                throw new System.Exception("AssetBundleUtil-Init：bundlePath和platformName和version组成的路径不存在对应的目录");
            }
            SetBundleExt(bundleExt);
            AssetBundle manifestBundle = AssetBundle.LoadFromFile(path + GetBundleName(manifestBundleName));
            if (manifestBundle == null)
            {
                throw new System.Exception("AssetBundleUtil-Init：manifestBundleName对应的bundle包不存在");
            }
            ManifestBundleName = manifestBundleName.ToLower();
            Manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            if (Manifest == null)
            {
                throw new System.Exception("AssetBundleUtil-Init：manifestBundleName错误，取不到里面的AssetBundleManifest");
            }
            manifestBundle.Unload(false);
            BundlePath = path;
        }

        private static void SetBundleExt(string bundleExt)
        {
            BundleExt = string.IsNullOrWhiteSpace(bundleExt) ? ".unity3d" : bundleExt;
            BundleExt = BundleExt.StartsWith(".") ? BundleExt : "." + BundleExt;
        }
        private static string GetBundleName(string abBundleName)
        {
            if (string.IsNullOrWhiteSpace(abBundleName))
            {
                throw new Exception("AssetBundleUtil-GetBundleName：abBundleName不能为空");
            }
            if (abBundleName.EndsWith(BundleExt))
            {
                return abBundleName.ToLower();
            }
            return abBundleName.ToLower() + BundleExt;
        }

        public static void ReloadManifest()
        {
            if (Manifest == null && !string.IsNullOrWhiteSpace(BundlePath) && !string.IsNullOrWhiteSpace(ManifestBundleName))
            {
                AssetBundle manifestBundle = AssetBundle.LoadFromFile(BundlePath + GetBundleName(ManifestBundleName));
                if (manifestBundle == null)
                {
                    throw new System.Exception("AssetBundleUtil-ReloadManifest：manifestBundleName对应的bundle包不存在");
                }
                Manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                if (Manifest == null)
                {
                    throw new System.Exception("AssetBundleUtil-ReloadManifest：manifestBundleName错误，取不到里面的AssetBundleManifest");
                }
                manifestBundle.Unload(false);
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
        /// <param name="abBundleName">指定的bundle包的名字</param>
        /// <returns></returns>
        public static string[] GetAllDependencies(string abBundleName)
        {
            if (string.IsNullOrWhiteSpace(abBundleName))
            {
                throw new System.Exception("AssetBundleUtil-GetAllDependencies：abBundleName不能为空");
            }
            return Manifest.GetAllDependencies(GetBundleName(abBundleName));
        }

        /// <summary>
        /// 加载bundle包
        /// </summary>
        /// <param name="abBundleName">指定的bundle包的名字</param>
        /// <param name="handleDependencieBeforeLoad">在加载之前先处理依赖包(这里包含所有的依赖包，包含依赖的依赖)</param>
        /// <returns></returns>
        public static AssetBundle LoadAssetBundle(string abBundleName, Action<string[]> handleDependencieBeforeLoad)
        {
            if (string.IsNullOrWhiteSpace(abBundleName))
            {
                throw new System.Exception("AssetBundleUtil-LoadAssetBundle：abBundleName不能为空");
            }
            handleDependencieBeforeLoad?.Invoke(GetAllDependencies(abBundleName));
            return AssetBundle.LoadFromFile(BundlePath + GetBundleName(abBundleName));
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="assetBundle">指定的assetBundle包</param>
        /// <param name="assetName">资源名称</param>
        /// <returns></returns>
        public static T LoadAsset<T>(AssetBundle assetBundle, string assetName) where T : UnityEngine.Object
        {
            if (assetBundle == null)
            {
                throw new System.Exception("AssetBundleUtil-LoadAsset：assetBundle不能为空");
            }
            if (string.IsNullOrWhiteSpace(assetName))
            {
                throw new System.Exception("AssetBundleUtil-LoadAsset：assetName不能为空");
            }
            return assetBundle.LoadAsset<T>(assetName);
        }
    }
    #endregion

    #region 保存清单文件
    public static partial class ABLoadUtil
    {
        /// <summary>
        /// 保存资源包清单文件
        /// </summary>
        /// <param name="bundleList"></param>
        /// <param name="bundleListFilePath"></param>
        public static void Save(ABLoadBundleList bundleList, string bundleListFilePath = null)
        {
            if (bundleList == null) { return; }
            byte[] bytes = System.Text.Encoding.Default.GetBytes(bundleList.Serialize());
            if (bytes == null || bytes.Length <= 0)
            {
                return;
            }

            string bundlePath = string.IsNullOrWhiteSpace(bundleListFilePath) ? BundlePath + "ABBundleFiles.txt" : bundleListFilePath;
            if (File.Exists(bundlePath))
            {
                File.Delete(bundlePath);
            }
            FileStream fs = new FileStream(bundlePath, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }

        /// <summary>
        /// 保存打进资源包的资源清单文件
        /// </summary>
        /// <param name="fileList"></param>
        /// <param name="fileListFilePath"></param>
        public static void Save(ABLoadFileList fileList, string fileListFilePath = null)
        {
            if (fileList == null) { return; }
            byte[] bytes = System.Text.Encoding.Default.GetBytes(fileList.Serialize());
            if (bytes == null || bytes.Length <= 0)
            {
                return;
            }

            string filePath = string.IsNullOrWhiteSpace(fileListFilePath) ? BundlePath + "ABBuiledFiles.txt" : fileListFilePath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            FileStream fs = new FileStream(filePath, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
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
                Debug.Log("AssetBundleUtil-ClearVersion：BundlePath对应的目录不存在");
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
        /// <param name="abBundleName">指定的bundle的名称</param>
        /// <param name="version">指定的版本号</param>
        public static void ClearBundle(string abBundleName, UInt32 version)
        {
            if (string.IsNullOrWhiteSpace(abBundleName) || version < 1)
            {
                Debug.Log("AssetBundleUtil-ClearBundle：abBundleName不能为空，version必须大于0");
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(BundlePath);
            if (directoryInfo == null)
            {
                Debug.Log("AssetBundleUtil-ClearBundle：BundlePath对应的目录不存在");
            }
            string path = directoryInfo.Parent.FullName + $"/Version{version}/" + GetBundleName(abBundleName);
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
        public static string ABBundleFilesFullPath => BundlePath + "ABBundleFiles.txt";

        /// <summary>
        /// 打进资源包的文件清单
        /// </summary>
        public static string ABBuiledFilesFullPath => BundlePath + "ABBuiledFiles.txt";

        /// <summary>
        /// 加载bundle和file清单文件
        /// </summary>
        /// <param name="bundleListFilePath">bundle清单文件路径，默认为初始化时的路径</param>
        /// <param name="fileListFilePath">file清单文件路径，默认为初始化时的路径</param>
        /// <returns></returns>
        public static Tuple<ABLoadBundleList, ABLoadFileList> LoadListFiles(string bundleListFilePath = null, string fileListFilePath = null)
        {
            string bundlePath = string.IsNullOrWhiteSpace(bundleListFilePath) ? BundlePath + "ABBundleFiles.txt" : bundleListFilePath;
            string filePath = string.IsNullOrWhiteSpace(fileListFilePath) ? BundlePath + "ABBuiledFiles.txt" : fileListFilePath;

            ABLoadBundleList bundleList = new ABLoadBundleList();
            ABLoadFileList fileList = new ABLoadFileList();

            if (File.Exists(bundlePath))
            {
                try
                {
                    bundleList = JsonConvert.DeserializeObject<ABLoadBundleList>(File.ReadAllText(bundlePath, Encoding.UTF8));
                }
                catch (Exception e)
                {
                    Debug.Log($"ABLoadUtil-LoadListFiles：加载ABLoadBundleList时失败：{e}");
                }
            }

            if (File.Exists(filePath))
            {
                try
                {
                    fileList = JsonConvert.DeserializeObject<ABLoadFileList>(File.ReadAllText(filePath, Encoding.UTF8));
                }
                catch (Exception e)
                {
                    Debug.Log($"ABLoadUtil-LoadListFiles：加载ABLoadFileList时失败：{e}");
                }
            }

            return new Tuple<ABLoadBundleList, ABLoadFileList>(bundleList, fileList);
        }
    }
    #endregion
}