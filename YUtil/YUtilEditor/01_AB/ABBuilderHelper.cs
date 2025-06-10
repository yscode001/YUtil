using System;
using YCSharp;

namespace YUtilEditor
{
    public class ABBuilderHelper
    {
        /// <summary>
        /// 获取Manifest所在的AB包的名字，返回：manifest_hashcode.unity3d)
        /// </summary>
        /// <param name="manifestABHashCode">hashCode</param>
        /// <returns>manifest_hashcode.unity3d</returns>
        /// <exception cref="Exception"></exception>
        internal static string GetManifestBundleName(string manifestABHashCode)
        {
            if (string.IsNullOrWhiteSpace(manifestABHashCode))
            {
                throw new Exception("manifestABHashCode不能为空");
            }
            return $"{ABHelper.Manifest}_{manifestABHashCode}{ABHelper.BundleExt}";
        }

        /// <summary>
        /// 获取AB包名称(小写带扩展名，如：audio.unity3d)
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <returns>小写带扩展名，如：audio.unity3d</returns>
        /// <exception cref="Exception"></exception>
        internal static string GetAssetBundleName(string assetBundleName)
        {
            if (string.IsNullOrWhiteSpace(assetBundleName))
            {
                throw new Exception("assetBundleName不能为空");
            }
            if (assetBundleName.EndsWith(ABHelper.BundleExt))
            {
                return assetBundleName.ToLower();
            }
            else
            {
                return assetBundleName.ToLower() + ABHelper.BundleExt;
            }
        }
    }
}