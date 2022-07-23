using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YUnity
{
    public static class SMRUtil
    {
        /// <summary>
        /// 将fromSMR拷贝至toSMR
        /// </summary>
        /// <param name="basicBones">基础骨骼(可能会对齐进行添加操作)</param>
        /// <param name="basicBoneNames">基础骨骼名称(可能会对齐进行添加操作)</param>
        /// <param name="fromSMR"></param>
        /// <param name="toSMR"></param>
        public static void CopyClothesToAnother(List<Transform> basicBones, List<string> basicBoneNames, SkinnedMeshRenderer fromSMR, SkinnedMeshRenderer toSMR)
        {
            if (basicBones == null || basicBones.Count <= 0 ||
                basicBoneNames == null || basicBoneNames.Count <= 0 ||
                fromSMR == null || toSMR == null) { return; }

            toSMR.transform.localPosition = fromSMR.transform.localPosition;
            toSMR.transform.localRotation = fromSMR.transform.localRotation;
            toSMR.transform.localScale = fromSMR.transform.localScale;
            toSMR.sharedMesh = fromSMR.sharedMesh;
            toSMR.sharedMaterial = fromSMR.sharedMaterial;

            foreach (Transform bone in basicBones)
            {
                if (bone.name == fromSMR.rootBone.name)
                {
                    toSMR.rootBone = bone;
                    break;
                }
            }

            // 把from独有的骨骼加入到basic里面
            if (fromSMR.bones != null && fromSMR.bones.Length > 0)
            {
                for (int i = 0; i < fromSMR.bones.Length; i++)
                {
                    Transform fromBone = fromSMR.bones[i];
                    if (fromBone == null || basicBoneNames.Contains(fromBone.name)) { continue; }
                    Transform newBone = fromBone.CopySelfEmptyGoToInArrayWithSameParentName(basicBones, false);
                    if (newBone == null) { continue; }
                    basicBones.Add(newBone);
                    basicBoneNames.Add(newBone.name);
                }
            }

            // 真正的换骨
            List<Transform> boneList = new List<Transform>();
            for (int i = 0; i < fromSMR.bones.Length; i++)
            {
                for (int j = 0; j < basicBones.Count; j++)
                {
                    if (fromSMR.bones[i].name == basicBones[j].name)
                    {
                        boneList.Add(basicBones[j]);
                        break;
                    }
                }
            }
            toSMR.bones = boneList.ToArray();
        }

        /// <summary>
        /// 清理，包括(sharedMesh,sharedMaterial,rootBone,bones)
        /// </summary>
        /// <param name="smr"></param>
        public static void Clear(this SkinnedMeshRenderer smr)
        {
            if (smr == null) { return; }
            smr.sharedMesh = null;
            smr.sharedMaterial = null;
            smr.rootBone = null;
            smr.bones = null;
        }

        /// <summary>
        /// 销毁所有的骨骼
        /// </summary>
        /// <param name="smr"></param>
        public static void DestroyAllBones(this SkinnedMeshRenderer smr)
        {
            if (smr == null) { return; }
            Transform[] array = smr.bones;
            if (array == null || array.Length <= 0) { return; }
            for (int i = array.Length - 1; i >= 0; i--)
            {
                GameObject.Destroy(array[i].gameObject);
            }
        }

        /// <summary>
        /// 销毁所有的骨骼(骨骼必须不在nameNotIn里面才会被销毁)
        /// </summary>
        /// <param name="smr"></param>
        /// <param name="nameNotIn">骨骼必须不在nameNotIn里面才会被销毁</param>
        public static void DestroyAllBones(this SkinnedMeshRenderer smr, string[] nameNotIn)
        {
            if (nameNotIn == null || nameNotIn.Length <= 0)
            {
                smr.DestroyAllBones();
                return;
            }

            if (smr == null) { return; }
            Transform[] array = smr.bones;
            if (array == null || array.Length <= 0) { return; }
            for (int i = array.Length - 1; i >= 0; i--)
            {
                if (!nameNotIn.Contains(array[i].name))
                {
                    GameObject.Destroy(array[i].gameObject);
                }
            }
        }
    }
}