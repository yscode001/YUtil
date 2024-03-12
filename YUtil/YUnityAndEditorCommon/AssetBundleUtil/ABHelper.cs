using System;

namespace YUnityAndEditorCommon
{
    public class ABHelper
    {
        /// <summary>
        /// 清单文件名称
        /// </summary>
        public const string ABBundleFilesName = "ABBundleFiles.txt";

        /// <summary>
        /// ab包后缀名
        /// </summary>
        public const string BundleExt = ".unity3d";

        /// <summary>
        /// ManifestBundleName
        /// </summary>
        public const string ManifestBundleName = "manifest.unity3d";

        /// <summary>
        /// 获取AB包名称
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetAssetBundleName(string assetBundleName)
        {
            if (string.IsNullOrWhiteSpace(assetBundleName))
            {
                throw new Exception("ABHelper-GetAssetBundleName：assetBundleName不能为空");
            }
            if (assetBundleName.EndsWith(BundleExt))
            {
                return assetBundleName.ToLower();
            }
            return assetBundleName.ToLower() + BundleExt;
        }
    }
}