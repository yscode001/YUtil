using System;

namespace YCSharp
{
    public partial class ABHelper
    {
        /// <summary>
        /// 清单文件名称(ABBundleFiles.txt)
        /// </summary>
        public const string ABBundleFilesName = "ABBundleFiles.txt";

        /// <summary>
        /// ab包后缀名(.unity3d)
        /// </summary>
        public const string BundleExt = ".unity3d";

        /// <summary>
        /// ManifestBundleName(manifest.unity3d)
        /// </summary>
        public const string ManifestBundleName = "manifest.unity3d";

        /// <summary>
        /// 获取AB包名称(小写带扩展名，如：audio.unity3d)
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetAssetBundleName(string assetBundleName)
        {
            if (string.IsNullOrWhiteSpace(assetBundleName))
            {
                throw new Exception("assetBundleName不能为空");
            }
            if (assetBundleName.EndsWith(BundleExt))
            {
                return assetBundleName.ToLower();
            }
            else
            {
                return assetBundleName.ToLower() + BundleExt;
            }
        }
    }
}