﻿// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-17
// ------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace YUnity
{
    #region Init
    public static partial class AssetBundleUtil
    {
        private static string BundlePath;
        private static string BundleExt;
        private static AssetBundleManifest Manifest;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="platformName">平台名称</param>
        /// <param name="bundlePath">bundle包所在路径</param>
        /// <param name="manifestBundleName">manifest的bundle包名，用于获取依赖关系</param>
        /// <param name="bundleExt">可选参数：bundle包的扩展名(默认：unity3d)</param>
        public static void Init(string platformName, string bundlePath, string manifestBundleName, string bundleExt = ".unity3d")
        {
            if (string.IsNullOrWhiteSpace(platformName) || string.IsNullOrWhiteSpace(bundlePath) || string.IsNullOrWhiteSpace(manifestBundleName))
            {
                throw new System.Exception("AssetBundleUtil-Init：platformName和bundlePath和manifestBundleName不能为空");
            }
            string path = (bundlePath.EndsWith("/") ? bundlePath : bundlePath + "/") + platformName + "/";
            if (!Directory.Exists(path))
            {
                throw new System.Exception("AssetBundleUtil-Init：bundlePath和platformName组成的路径不存在对应的目录");
            }
            SetBundleExt(bundleExt);
            AssetBundle manifestBundle = AssetBundle.LoadFromFile(path + GetBundleName(manifestBundleName));
            if (manifestBundle == null)
            {
                throw new System.Exception("AssetBundleUtil-Init：manifestBundleName对应的bundle包不存在");
            }
            Manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            if (Manifest == null)
            {
                throw new System.Exception("AssetBundleUtil-Init：manifestBundleName错误，取不到里面的AssetBundleManifest");
            }
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
                return abBundleName;
            }
            return abBundleName + BundleExt;
        }
    }
    #endregion

    #region LoadAsset
    public static partial class AssetBundleUtil
    {
        /// <summary>
        /// 加载bundle包及其依赖
        /// </summary>
        /// <param name="abBundleName"></param>
        /// <returns></returns>
        private static Tuple<AssetBundle, List<AssetBundle>> LoadAssetBundle(string abBundleName)
        {
            List<AssetBundle> dependencieList = null;
            string[] dependencies = Manifest.GetAllDependencies(GetBundleName(abBundleName));
            if (dependencies != null && dependencies.Length > 0)
            {
                dependencieList = new List<AssetBundle>();
                foreach (var dependencie in dependencies)
                {
                    AssetBundle dependencieBundle = AssetBundle.LoadFromFile(BundlePath + GetBundleName(dependencie));
                    if (dependencieBundle != null)
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
            return new Tuple<AssetBundle, AssetBundle[], T>(tupe.Item1, tupe.Item2.ToArray(), tupe.Item1.LoadAsset<T>(assetName));
        }
    }
    #endregion
}