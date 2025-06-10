using System;

namespace YCSharp
{
    public partial class ABHelper
    {
        /// <summary>
        /// 清单文件名称(ManifestFile.txt)
        /// </summary>
        public const string ManifestFileName = "ManifestFile.txt";

        /// <summary>
        /// ab包后缀名(.unity3d)
        /// </summary>
        public const string BundleExt = ".unity3d";

        /// <summary>
        /// Manifest(manifest)
        /// </summary>
        public const string Manifest = "manifest";

        /// <summary>
        /// 获取Manifest所在的AB包的名字(Manifest_hashcode.unity3d)
        /// </summary>
        /// <param name="manifestABHashCode">hashCode</param>
        /// <returns>{Manifest}_{manifestABHashCode}{BundleExt}</returns>
        /// <exception cref="Exception"></exception>
        public static string GetManifestBundleName(string manifestABHashCode)
        {
            if (string.IsNullOrWhiteSpace(manifestABHashCode))
            {
                throw new Exception("manifestABHashCode不能为空");
            }
            return $"{Manifest}_{manifestABHashCode}{BundleExt}";
        }

        /// <summary>
        /// 获取AB包名称(小写带扩展名，如：audio.unity3d)
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <returns>小写带扩展名，如：audio.unity3d</returns>
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