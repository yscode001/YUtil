﻿// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-17
// ------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

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
        /// 加载bundle包及其依赖
        /// </summary>
        /// <param name="abBundleName"></param>
        /// <returns></returns>
        public static Tuple<AssetBundle, List<AssetBundle>> LoadAssetBundle(string abBundleName)
        {
            List<AssetBundle> dependencieList = null;
            string[] dependencies = Manifest.GetAllDependencies(GetBundleName(abBundleName));
            if (dependencies != null && dependencies.Length > 0)
            {
                dependencieList = new List<AssetBundle>();
                foreach (var dependencie in dependencies)
                {
                    AssetBundle dependencieBundle = AssetBundle.LoadFromFile(BundlePath + GetBundleName(dependencie));
                    if (dependencieBundle == null)
                    {
                        throw new Exception($"AssetBundleUtil-LoadAssetBundle：{abBundleName}的依赖包：{GetBundleName(dependencie)}不存在");
                    }
                    else
                    {
                        dependencieList.Add(dependencieBundle);
                    }
                }
            }
            return new Tuple<AssetBundle, List<AssetBundle>>(AssetBundle.LoadFromFile(BundlePath + GetBundleName(abBundleName)), dependencieList);
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="abBundleName">bundle包名称</param>
        /// <param name="assetName">资源名称</param>
        /// <returns>bundle、dependencieBundleArray、asset</returns>
        public static Tuple<AssetBundle, AssetBundle[], T> LoadAsset<T>(string abBundleName, string assetName) where T : UnityEngine.Object
        {
            if (string.IsNullOrWhiteSpace(abBundleName) || string.IsNullOrWhiteSpace(assetName))
            {
                throw new System.Exception("AssetBundleUtil-LoadAsset：abBundleName和assetName不能为空");
            }
            if (string.IsNullOrWhiteSpace(BundlePath) || Manifest == null)
            {
                throw new System.Exception("AssetBundleUtil-LoadAsset：请先调用YUnity.AssetBundleUtil.Init进行初始化");
            }
            Tuple<AssetBundle, List<AssetBundle>> tupe = LoadAssetBundle(abBundleName);
            if (tupe == null)
            {
                throw new Exception($"AssetBundleUtil-LoadAsset：加载资源出错，abBundleName：{abBundleName}，assetName：{assetName}");
            }
            if (tupe.Item1 == null)
            {
                throw new System.Exception($"AssetBundleUtil-LoadAsset：{abBundleName}对应的bundle包不存在，请检查");
            }
            if (tupe.Item2 == null || tupe.Item2.Count <= 0)
            {
                return new Tuple<AssetBundle, AssetBundle[], T>(tupe.Item1, null, tupe.Item1.LoadAsset<T>(assetName));
            }
            else
            {
                return new Tuple<AssetBundle, AssetBundle[], T>(tupe.Item1, tupe.Item2.ToArray(), tupe.Item1.LoadAsset<T>(assetName));
            }
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