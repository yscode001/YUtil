// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-16
// ------------------------------

/*
规则：
1、根目录下如果有文件，打成一个bundle包
2、根目录下的一级目录，每个一级目录打成一个bundle包
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace YUtilEditor
{
    #region Init
    public static partial class AssetBundleUtil
    {
        // 平台名称
        private static string PlatformName;

        // 资源所在的根目录
        private static string ResSourceDirectory;

        // 资源输出目录
        private static string ResOutputDirectory;

        // 忽略文件的扩展名集合
        private static List<string> IgnoreExts;

        // ab包后缀
        private const string BundleExt = ".unity3d";

        // 根目录下如果有文件，打成一个root bundle
        private static string RootBundleName => "root" + BundleExt;

        // 清单文件名
        private static string BundleListFileName => "manifest" + BundleExt;

        // 打包选项
        private static BuildAssetBundleOptions BundleOptions = BuildAssetBundleOptions.None;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="platformName">平台名称(示例：iOS、android...)</param>
        /// <param name="resSourceDirectory">资源所在的根目录(示例：Assets/Res)</param>
        /// <param name="resOutputDirectory">可选参数：资源输出路径(默认：./AssetBundleFiles/PlatformName/)</param>
        /// <param name="ignoreExts">可选参数：需要忽略的文件扩展名集合(带不带点都可以)</param>
        /// <param name="bundleOptions">可选参数：打包选项(默认：None)</param>
        public static void Init(string platformName, string resSourceDirectory, string resOutputDirectory = null, string[] ignoreExts = null, BuildAssetBundleOptions bundleOptions = BuildAssetBundleOptions.None)
        {
            if (string.IsNullOrWhiteSpace(platformName) || string.IsNullOrWhiteSpace(resSourceDirectory))
            {
                throw new System.Exception("AssetBundleUtil-Init：platformName或resSourceDirectory不能为空");
            }
            if (!Directory.Exists(resSourceDirectory))
            {
                throw new System.Exception("AssetBundleUtil-Init：resSourceDirectory目录不存在");
            }
            PlatformName = platformName;
            ResSourceDirectory = resSourceDirectory.EndsWith("/") ? resSourceDirectory : resSourceDirectory + "/";
            HandleExts(ignoreExts);
            BundleOptions = bundleOptions;

            ResOutputDirectory = string.IsNullOrWhiteSpace(resOutputDirectory) ? $"./AssetBundleFiles/{PlatformName}/" : resOutputDirectory;
            ResOutputDirectory = ResOutputDirectory.EndsWith("/") ? ResOutputDirectory : ResOutputDirectory + "/";
            if (!ResOutputDirectory.EndsWith($"/{PlatformName}/"))
            {
                ResOutputDirectory += (PlatformName + "/");
            }
            if (Directory.Exists(ResOutputDirectory))
            {
                Directory.Delete(ResOutputDirectory, true);
            }
            Directory.CreateDirectory(ResOutputDirectory);
            AssetDatabase.Refresh();
        }

        private static void HandleExts(string[] ignoreExts)
        {
            if (ignoreExts == null || ignoreExts.Length <= 0)
            {
                IgnoreExts = new List<string>();
            }
            else
            {
                IgnoreExts = ignoreExts.ToList();
            }
            for (int i = IgnoreExts.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrWhiteSpace(IgnoreExts[i]))
                {
                    IgnoreExts.RemoveAt(i);
                    continue;
                }
                string ext = IgnoreExts[i].ToLower();
                if (ext.StartsWith("."))
                {
                    IgnoreExts[i] = ext;
                }
                else
                {
                    IgnoreExts[i] = "." + ext;
                }
            }
            if (IgnoreExts != null)
            {
                if (!IgnoreExts.Contains(".meta"))
                {
                    IgnoreExts.Add(".meta");
                }
                if (!IgnoreExts.Contains(".tpsheet"))
                {
                    IgnoreExts.Add(".tpsheet");
                }
                if (!IgnoreExts.Contains(".cs"))
                {
                    IgnoreExts.Add(".cs");
                }
            }
        }
    }
    #endregion

    #region Build
    public static partial class AssetBundleUtil
    {
        public static void BuildAssetBundles()
        {
            if (string.IsNullOrWhiteSpace(PlatformName) || string.IsNullOrWhiteSpace(ResSourceDirectory) || string.IsNullOrWhiteSpace(ResOutputDirectory))
            {
                throw new System.Exception("AssetBundleUtil-BuildAssetBundles：PlatformName或ResSourceDirectory或ResOutputDirectory为空，请先调用YUtilEditor.AssetBundleUtil.Init方法进行初始化");
            }
            if (!Directory.Exists(ResSourceDirectory))
            {
                throw new System.Exception("AssetBundleUtil-BuildAssetBundles：ResSourceDirectory目录不存在，请先调用YUtilEditor.AssetBundleUtil.Init方法进行初始化");
            }
            if (!HasFilesWillBeBuiled(new DirectoryInfo(ResSourceDirectory)))
            {
                throw new System.Exception("AssetBundleUtil-BuildAssetBundles：ResSourceDirectory不存在任何文件，无法build");
            }
            List<AssetBundleBuild> list = new List<AssetBundleBuild>();

            DirectoryInfo sourceDir = new DirectoryInfo(ResSourceDirectory);
            AssetBundleBuild rootBuildInfo = GetBuildInfo(sourceDir, true);
            if (!string.IsNullOrWhiteSpace(rootBuildInfo.assetBundleName) && rootBuildInfo.assetNames != null && rootBuildInfo.assetNames.Length > 0)
            {
                list.Add(rootBuildInfo);
            }

            DirectoryInfo[] childrenDirs = sourceDir.GetDirectories();
            foreach (var childDir in childrenDirs)
            {
                AssetBundleBuild childBuildInfo = GetBuildInfo(childDir, false);
                if (!string.IsNullOrWhiteSpace(childBuildInfo.assetBundleName) && childBuildInfo.assetNames != null && childBuildInfo.assetNames.Length > 0)
                {
                    list.Add(childBuildInfo);
                }
            }

            if (list.Count <= 0)
            {
                throw new System.Exception("AssetBundleUtil-BuildAssetBundles：ResSourceDirectory不存在任何符合条件可以打包的文件，无法build");
            }

            // 被打包的文件清单
            StringBuilder sbFileList = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                AssetBundleBuild buildInfo = list[i];

                sbFileList.Append("BundleName:  ");
                sbFileList.Append(buildInfo.assetBundleName);
                sbFileList.Append("\n");
                for (int j = 0; j < buildInfo.assetNames.Length; j++)
                {
                    string filePath = buildInfo.assetNames[j];
                    sbFileList.Append("----  ");
                    sbFileList.Append("AssetName:  ");
                    sbFileList.Append(filePath);
                    if (j < buildInfo.assetNames.Length - 1 || i < list.Count - 1)
                    {
                        sbFileList.Append("\n");
                    }
                }
                if (i < list.Count - 1)
                {
                    sbFileList.Append("\n");
                }
            }

            DirectoryInfo outputDirInfo = new DirectoryInfo(ResOutputDirectory);
            BuildPipeline.BuildAssetBundles(outputDirInfo.FullName, list.ToArray(), BundleOptions, EditorUserBuildSettings.activeBuildTarget);
            AfterBuild(sbFileList);
        }

        // 判断文件夹里面是否有需要build的文件
        private static bool HasFilesWillBeBuiled(DirectoryInfo directoryInfo)
        {
            if (directoryInfo == null) { return false; }

            // 先判断是否有文件
            FileInfo[] childrenFiles = directoryInfo.GetFiles();
            if (childrenFiles != null && childrenFiles.Length > 0)
            {
                if (IgnoreExts == null || IgnoreExts.Count <= 0)
                {
                    return true;
                }

                foreach (var fileinfo in childrenFiles)
                {
                    string ext = Path.GetExtension(fileinfo.Name).ToLower();
                    if (!IgnoreExts.Contains(ext))
                    {
                        return true;
                    }
                }
            }

            // 没有文件，那么判断子文件夹中是否有文件
            DirectoryInfo[] childrenDirs = directoryInfo.GetDirectories();
            if (childrenDirs == null || childrenDirs.Length <= 0)
            {
                return false;
            }
            foreach (var childDir in childrenDirs)
            {
                if (HasFilesWillBeBuiled(childDir))
                {
                    return true;
                }
            }

            // 没有文件
            return false;
        }

        // 获取文件夹中可以build的文件集合
        private static List<FileInfo> GetFilesWillBeBuiled(DirectoryInfo directoryInfo, bool root)
        {
            if (directoryInfo == null) { return null; }
            List<FileInfo> list = new List<FileInfo>();
            if (root)
            {
                // 不需要递归
                var fileinfos = directoryInfo.GetFiles();
                if (fileinfos != null && fileinfos.Length > 0)
                {
                    foreach (var fileinfo in fileinfos)
                    {
                        if (IgnoreExts != null && IgnoreExts.Count > 0 && IgnoreExts.Contains(Path.GetExtension(fileinfo.Name).ToLower()))
                        {
                            // 被忽略的文件忽略掉
                            continue;
                        }
                        list.Add(fileinfo);
                    }
                }
            }
            else
            {
                // 需要递归
                GetAllFiles(directoryInfo, list);
            }
            return list;
        }

        // 获取文件夹中的所有文件(包括子文件夹)
        private static void GetAllFiles(DirectoryInfo directoryInfo, List<FileInfo> list)
        {
            if (directoryInfo == null || list == null) { return; }
            // 获取文件
            var fileinfos = directoryInfo.GetFiles();
            if (fileinfos != null && fileinfos.Length > 0)
            {
                foreach (var fileinfo in fileinfos)
                {
                    if (IgnoreExts != null && IgnoreExts.Count > 0 && IgnoreExts.Contains(Path.GetExtension(fileinfo.Name).ToLower()))
                    {
                        // 被忽略的文件忽略掉
                        continue;
                    }
                    list.Add(fileinfo);
                }
            }
            // 递归获取
            DirectoryInfo[] childrenDirs = directoryInfo.GetDirectories();
            if (childrenDirs != null && childrenDirs.Length > 0)
            {
                foreach (var childDir in childrenDirs)
                {
                    if (childDir == null) { continue; }
                    GetAllFiles(childDir, list);
                }
            }
        }

        private static AssetBundleBuild GetBuildInfo(DirectoryInfo directoryInfo, bool root)
        {
            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = root ? RootBundleName : directoryInfo.Name + BundleExt;

            List<FileInfo> fileInfolist = GetFilesWillBeBuiled(directoryInfo, root);
            List<string> fileNames = new List<string>();
            foreach (var fileinfo in fileInfolist)
            {
                // 获取文件的相对路径
                string filepath = "Assets" + fileinfo.FullName.Replace(Application.dataPath, "");
                fileNames.Add(filepath);
            }
            build.assetNames = fileNames.ToArray();

            return build;
        }

        public static string GetMD5HashFromFile(string filepath)
        {
            try
            {
                FileStream file = new FileStream(filepath, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail, error:" + ex.Message);
            }
        }
        private static void AfterBuild(StringBuilder sbFileList)
        {
            DirectoryInfo outputDir = new DirectoryInfo(ResOutputDirectory);
            FileInfo[] fileInfos = outputDir.GetFiles();
            if (fileInfos == null || fileInfos.Length <= 0)
            {
                BuildEnd(false);
                return;
            }
            List<FileInfo> filelist = new List<FileInfo>();
            for (int i = fileInfos.Length - 1; i >= 0; i--)
            {
                FileInfo fileInfo = fileInfos[i];
                if (Path.GetExtension(fileInfo.Name) == ".manifest")
                {
                    // 删除manifest
                    File.Delete(fileInfo.FullName);
                }
                else if (fileInfo.Name == PlatformName)
                {
                    // 先改名字
                    string newName = fileInfo.Directory + "/" + BundleListFileName;
                    fileInfo.MoveTo(newName);
                    // 再添加至清单列表
                    filelist.Add(fileInfo);
                }
                else
                {
                    // 添加至清单列表
                    filelist.Add(fileInfo);
                }
            }
            if (filelist.Count <= 0)
            {
                BuildEnd(false);
                return;
            }
            // 生成bundle包的清单
            // 文件名|大小|MD5
            StringBuilder sbBundleList = new StringBuilder();
            for (int i = 0; i < filelist.Count; i++)
            {
                FileInfo fileInfo = filelist[i];
                sbBundleList.Append(fileInfo.Name);
                sbBundleList.Append("|");
                sbBundleList.Append(fileInfo.Length);
                sbBundleList.Append("|");
                sbBundleList.Append(GetMD5HashFromFile(fileInfo.FullName));
                if (i < filelist.Count - 1)
                {
                    sbBundleList.Append("\n");
                }
            }

            byte[] bytes = System.Text.Encoding.Default.GetBytes(sbBundleList.ToString());
            if (bytes == null || bytes.Length <= 0)
            {
                BuildEnd(false);
                return;
            }
            FileStream fs = new FileStream(ResOutputDirectory + "ABBundleFiles.txt", FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            // 文件清单
            bytes = System.Text.Encoding.Default.GetBytes(sbFileList.ToString());
            if (bytes == null || bytes.Length <= 0)
            {
                BuildEnd(false);
                return;
            }
            fs = new FileStream(ResOutputDirectory + "ABBuiledFiles.txt", FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            BuildEnd(true);
        }

        private static void BuildEnd(bool success)
        {
            AssetDatabase.Refresh();
            string result = success ? "成功" : "失败";
            Debug.Log($"AssetBundle打包结束：{result}");
        }
    }
    #endregion
}