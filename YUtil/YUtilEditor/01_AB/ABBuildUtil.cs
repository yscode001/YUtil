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
    public static partial class ABBuildUtil
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
        private static string BundleExt = ".unity3d";

        // 清单文件名
        private static string BundleListFileName = "manifest" + BundleExt;

        // 打包选项
        private static BuildAssetBundleOptions BundleOptions = BuildAssetBundleOptions.None;

        // 资源版本号(更换版本情况：资源发生重大改变，资源的目录结构都变了。一般情况下无需更换版本号)
        private static UInt32 Version = 1;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="platformName"平台名称(示例：iOS、android...)</param>
        /// <param name="resSourceDirectory">资源所在的根目录(示例：Assets/Res)</param>
        /// <param name="resOutputDirectory">可选参数：资源输出路径(默认：./AssetBundleFiles/PlatformName/)</param>
        /// <param name="ignoreExts">可选参数：需要忽略的文件扩展名集合(带不带点都可以)</param>
        /// <param name="bundleExt">可选参数：bundle包的扩展名(默认：.unity3d)</param>
        /// <param name="manifestBundleName">可选参数：manifest包的名字，默认：manifest.unity3d</param>
        /// <param name="bundleOptions">可选参数：打包选项(默认：None)</param>
        /// <param name="version">可选参数：资源版本号(更换版本情况：资源发生重大改变，资源的目录结构都变了。一般情况下无需更换版本号，默认：1)</param>
        public static void Init(string platformName, string resSourceDirectory, string resOutputDirectory = null, string[] ignoreExts = null, string bundleExt = ".unity3d", string manifestBundleName = "manifest", BuildAssetBundleOptions bundleOptions = BuildAssetBundleOptions.None, UInt32 version = 1)
        {
            if (string.IsNullOrWhiteSpace(platformName) || string.IsNullOrWhiteSpace(resSourceDirectory))
            {
                throw new System.Exception("AssetBundleBuildUtil-Init：platformName或resSourceDirectory不能为空");
            }
            if (!Directory.Exists(resSourceDirectory))
            {
                throw new System.Exception("AssetBundleBuildUtil-Init：resSourceDirectory目录不存在");
            }
            Version = (UInt32)Mathf.Max(1, version);
            PlatformName = platformName;
            ResSourceDirectory = resSourceDirectory.EndsWith("/") ? resSourceDirectory : resSourceDirectory + "/";
            SetManifestBundleName(manifestBundleName);
            SetBundleExt(bundleExt);
            SetIgnoreExts(ignoreExts);
            BundleOptions = bundleOptions;

            ResOutputDirectory = string.IsNullOrWhiteSpace(resOutputDirectory) ? $"./AssetBundleFiles/{PlatformName}/" : resOutputDirectory;
            ResOutputDirectory = ResOutputDirectory.EndsWith("/") ? ResOutputDirectory : ResOutputDirectory + "/";
            if (!ResOutputDirectory.EndsWith($"/{PlatformName}/"))
            {
                ResOutputDirectory += (PlatformName + "/");
            }
            if (!ResOutputDirectory.EndsWith($"/Version{Version}/"))
            {
                ResOutputDirectory += $"Version{Version}/";
            }
        }

        private static void SetManifestBundleName(string manifestBundleName)
        {
            if (string.IsNullOrWhiteSpace(manifestBundleName))
            {
                BundleListFileName = "manifest" + BundleExt;
            }
            else if (manifestBundleName.Contains("."))
            {
                BundleListFileName = manifestBundleName.ToLower();
            }
            else
            {
                BundleListFileName = manifestBundleName.ToLower() + BundleExt;
            }
        }
        private static void SetBundleExt(string bundleExt)
        {
            BundleExt = string.IsNullOrWhiteSpace(bundleExt) ? ".unity3d" : bundleExt;
            BundleExt = BundleExt.StartsWith(".") ? BundleExt : "." + BundleExt;
        }
        private static void SetIgnoreExts(string[] ignoreExts)
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
    public static partial class ABBuildUtil
    {
        public static void BuildAssetBundles()
        {
            if (string.IsNullOrWhiteSpace(PlatformName) || string.IsNullOrWhiteSpace(ResSourceDirectory) || string.IsNullOrWhiteSpace(ResOutputDirectory))
            {
                throw new System.Exception("AssetBundleBuildUtil-BuildAssetBundles：PlatformName或ResSourceDirectory或ResOutputDirectory为空，请先调用YUtilEditor.AssetBundleUtil.Init方法进行初始化");
            }
            if (!Directory.Exists(ResSourceDirectory))
            {
                throw new System.Exception("AssetBundleBuildUtil-BuildAssetBundles：ResSourceDirectory目录不存在，请先调用YUtilEditor.AssetBundleUtil.Init方法进行初始化");
            }
            if (!HasFilesWillBeBuiled(new DirectoryInfo(ResSourceDirectory)))
            {
                throw new System.Exception("AssetBundleBuildUtil-BuildAssetBundles：ResSourceDirectory不存在任何文件，无法build");
            }
            if (Directory.Exists(ResOutputDirectory))
            {
                Directory.Delete(ResOutputDirectory, true);
            }
            Directory.CreateDirectory(ResOutputDirectory);

            List<AssetBundleBuild> list = new List<AssetBundleBuild>();

            DirectoryInfo sourceDir = new DirectoryInfo(ResSourceDirectory);
            DirectoryInfo[] childrenDirs = sourceDir.GetDirectories();
            foreach (var childDir in childrenDirs)
            {
                AssetBundleBuild childBuildInfo = GetBuildInfo(childDir);
                if (!string.IsNullOrWhiteSpace(childBuildInfo.assetBundleName) && childBuildInfo.assetNames != null && childBuildInfo.assetNames.Length > 0)
                {
                    list.Add(childBuildInfo);
                }
            }

            if (list.Count <= 0)
            {
                throw new System.Exception("AssetBundleBuildUtil-BuildAssetBundles：ResSourceDirectory不存在任何符合条件可以打包的文件，无法build");
            }

            // 被打包的文件清单
            ABBuiledFileList fileList = new ABBuiledFileList();
            for (int i = 0; i < list.Count; i++)
            {
                AssetBundleBuild buildInfo = list[i];
                fileList.Add(buildInfo.assetBundleName, buildInfo.assetNames);
            }

            DirectoryInfo outputDirInfo = new DirectoryInfo(ResOutputDirectory);
            BuildPipeline.BuildAssetBundles(outputDirInfo.FullName, list.ToArray(), BundleOptions, EditorUserBuildSettings.activeBuildTarget);
            AfterBuild(fileList.Serialize());
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
        private static List<FileInfo> GetFilesWillBeBuiled(DirectoryInfo directoryInfo)
        {
            if (directoryInfo == null) { return null; }
            List<FileInfo> list = new List<FileInfo>();
            // 递归遍历文件
            GetAllFiles(directoryInfo, list);
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

        private static AssetBundleBuild GetBuildInfo(DirectoryInfo directoryInfo)
        {
            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = GetBundleName(directoryInfo.Name);

            List<FileInfo> fileInfolist = GetFilesWillBeBuiled(directoryInfo);
            List<string> fileNames = new List<string>();
            foreach (var fileinfo in fileInfolist)
            {
                // 获取文件的相对路径
                string filepath = "Assets" + fileinfo.FullName.Replace("\\", "/").Replace(Application.dataPath.Replace("\\", "/"), "");
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
                throw new Exception("AssetBundleBuildUtil-GetMD5HashFromFile() fail, error:" + ex.Message);
            }
        }
        private static void AfterBuild(string sbFileList)
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
                else if (fileInfo.Name == $"Version{Version}")
                {
                    // 先改名字
                    string newName = fileInfo.Directory + "/" + GetBundleName(BundleListFileName);
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
            ABBuiledBundleList bundleList = new ABBuiledBundleList();
            for (int i = 0; i < filelist.Count; i++)
            {
                FileInfo fileInfo = filelist[i];
                bundleList.Add(new ABBuiledBundle(fileInfo.Name, fileInfo.Length, GetMD5HashFromFile(fileInfo.FullName)));
            }

            byte[] bytes = System.Text.Encoding.Default.GetBytes(bundleList.Serialize());
            if (bytes == null || bytes.Length <= 0)
            {
                BuildEnd(false);
                return;
            }
            FileStream fs = new FileStream(ResOutputDirectory + "ABBundleFiles.txt", FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            // 文件清单
            bytes = System.Text.Encoding.Default.GetBytes(sbFileList);
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

    #region Clear
    public static partial class ABBuildUtil
    {
        /// <summary>
        /// 清理某一版本或所有版本的bundle资源
        /// </summary>
        /// <param name="version">相应的版本号(< 1表示清理所有版本的bundle资源)</param>
        public static void ClearVersion(UInt32 version = 0)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(ResOutputDirectory);
            if (directoryInfo == null)
            {
                throw new Exception("AssetBundleBuildUtil-ClearVersion：ResOutputDirectory对应的目录不存在");
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
            AssetDatabase.Refresh();
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
                throw new Exception("AssetBundleBuildUtil-ClearBundle：abBundleName不能为空，version必须大于0");
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(ResOutputDirectory);
            if (directoryInfo == null)
            {
                throw new Exception("AssetBundleBuildUtil-ClearBundle：ResOutputDirectory对应的目录不存在");
            }
            string path = directoryInfo.Parent.FullName + $"/Version{version}/" + GetBundleName(abBundleName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            AssetDatabase.Refresh();
        }

        private static string GetBundleName(string abBundleName)
        {
            if (string.IsNullOrWhiteSpace(abBundleName))
            {
                throw new Exception("AssetBundleBuildUtil-GetBundleName：abBundleName不能为空");
            }
            if (abBundleName.EndsWith(BundleExt))
            {
                return abBundleName.ToLower();
            }
            return abBundleName.ToLower() + BundleExt;
        }
    }
    #endregion
}