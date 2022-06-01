using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    public class SMRUtil
    {
        public static void CopyClothesToAnother(Transform[] allBones, SkinnedMeshRenderer fromSMR, SkinnedMeshRenderer toSMR)
        {
            if (allBones == null || allBones.Length <= 0 || fromSMR == null || toSMR == null) { return; }

            toSMR.transform.localPosition = fromSMR.transform.localPosition;
            toSMR.transform.localRotation = fromSMR.transform.localRotation;
            toSMR.transform.localScale = fromSMR.transform.localScale;
            toSMR.sharedMesh = fromSMR.sharedMesh;
            toSMR.sharedMaterial = fromSMR.sharedMaterial;

            foreach (Transform bone in allBones)
            {
                if (bone.name == fromSMR.rootBone.name)
                {
                    toSMR.rootBone = bone;
                    break;
                }
            }
            List<Transform> boneList = new List<Transform>();
            for (int i = 0; i < fromSMR.bones.Length; i++)
            {
                for (int j = 0; j < allBones.Length; j++)
                {
                    if (fromSMR.bones[i].name == allBones[j].name)
                    {
                        boneList.Add(allBones[j]);
                        break;
                    }
                }
            }
            toSMR.bones = boneList.ToArray();
        }

        public static void ClearSMR(SkinnedMeshRenderer smr)
        {
            if (smr == null) { return; }
            smr.sharedMesh = null;
            smr.sharedMaterial = null;
            smr.rootBone = null;
            smr.bones = null;
        }
    }
}