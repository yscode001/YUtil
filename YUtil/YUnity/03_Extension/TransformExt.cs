using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    public static class TransformExt
    {
        #region SetPosition

        public static void SetPositionX(this Transform tf, float x, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localPosition : tf.position;
            v3.x = x;
            if (isLocal) { tf.localPosition = v3; }
            else { tf.position = v3; }
        }

        public static void SetPositionY(this Transform tf, float y, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localPosition : tf.position;
            v3.y = y;
            if (isLocal) { tf.localPosition = v3; }
            else { tf.position = v3; }
        }

        public static void SetPositionZ(this Transform tf, float z, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localPosition : tf.position;
            v3.z = z;
            if (isLocal) { tf.localPosition = v3; }
            else { tf.position = v3; }
        }

        public static void SetPositionXY(this Transform tf, float x, float y, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localPosition : tf.position;
            v3.x = x;
            v3.y = y;
            if (isLocal) { tf.localPosition = v3; }
            else { tf.position = v3; }
        }

        public static void SetPositionXZ(this Transform tf, float x, float z, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localPosition : tf.position;
            v3.x = x;
            v3.z = z;
            if (isLocal) { tf.localPosition = v3; }
            else { tf.position = v3; }
        }

        public static void SetPositionYZ(this Transform tf, float y, float z, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localPosition : tf.position;
            v3.y = y;
            v3.z = z;
            if (isLocal) { tf.localPosition = v3; }
            else { tf.position = v3; }
        }

        public static void SetPositionXYZ(this Transform tf, float x, float y, float z, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localPosition : tf.position;
            v3.x = x;
            v3.y = y;
            v3.z = z;
            if (isLocal) { tf.localPosition = v3; }
            else { tf.position = v3; }
        }

        #endregion

        #region SetLocalScale

        public static void SetLocalScaleX(this Transform tf, float x)
        {
            if (tf == null) { return; }
            Vector3 v3 = tf.localScale;
            v3.x = x;
            tf.localScale = v3;
        }

        public static void SetLocalScaleY(this Transform tf, float y)
        {
            if (tf == null) { return; }
            Vector3 v3 = tf.localScale;
            v3.y = y;
            tf.localScale = v3;
        }

        public static void SetLocalScaleZ(this Transform tf, float z)
        {
            if (tf == null) { return; }
            Vector3 v3 = tf.localScale;
            v3.z = z;
            tf.localScale = v3;
        }

        public static void SetLocalScaleXY(this Transform tf, float x, float y)
        {
            if (tf == null) { return; }
            Vector3 v3 = tf.localScale;
            v3.x = x;
            v3.y = y;
            tf.localScale = v3;
        }

        public static void SetLocalScaleXZ(this Transform tf, float x, float z)
        {
            if (tf == null) { return; }
            Vector3 v3 = tf.localScale;
            v3.x = x;
            v3.z = z;
            tf.localScale = v3;
        }

        public static void SetLocalScaleYZ(this Transform tf, float y, float z)
        {
            if (tf == null) { return; }
            Vector3 v3 = tf.localScale;
            v3.y = y;
            v3.z = z;
            tf.localScale = v3;
        }

        public static void SetLocalScaleXYZ(this Transform tf, float x, float y, float z)
        {
            if (tf == null) { return; }
            Vector3 v3 = tf.localScale;
            v3.x = x;
            v3.y = y;
            v3.z = z;
            tf.localScale = v3;
        }

        #endregion

        #region SetEulerAngles

        public static void SetEulerAnglesX(this Transform tf, float x, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localEulerAngles : tf.eulerAngles;
            v3.x = x;
            if (isLocal) { tf.localEulerAngles = v3; }
            else { tf.eulerAngles = v3; }
        }

        public static void SetEulerAnglesY(this Transform tf, float y, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localEulerAngles : tf.eulerAngles;
            v3.y = y;
            if (isLocal) { tf.localEulerAngles = v3; }
            else { tf.eulerAngles = v3; }
        }

        public static void SetEulerAnglesZ(this Transform tf, float z, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localEulerAngles : tf.eulerAngles;
            v3.z = z;
            if (isLocal) { tf.localEulerAngles = v3; }
            else { tf.eulerAngles = v3; }
        }

        public static void SetEulerAnglesXY(this Transform tf, float x, float y, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localEulerAngles : tf.eulerAngles;
            v3.x = x;
            v3.y = y;
            if (isLocal) { tf.localEulerAngles = v3; }
            else { tf.eulerAngles = v3; }
        }

        public static void SetEulerAnglesXZ(this Transform tf, float x, float z, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localEulerAngles : tf.eulerAngles;
            v3.x = x;
            v3.z = z;
            if (isLocal) { tf.localEulerAngles = v3; }
            else { tf.eulerAngles = v3; }
        }

        public static void SetEulerAnglesYZ(this Transform tf, float y, float z, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localEulerAngles : tf.eulerAngles;
            v3.y = y;
            v3.z = z;
            if (isLocal) { tf.localEulerAngles = v3; }
            else { tf.eulerAngles = v3; }
        }

        public static void SetEulerAnglesXYZ(this Transform tf, float x, float y, float z, bool isLocal)
        {
            if (tf == null) { return; }
            Vector3 v3 = isLocal ? tf.localEulerAngles : tf.eulerAngles;
            v3.x = x;
            v3.y = y;
            v3.z = z;
            if (isLocal) { tf.localEulerAngles = v3; }
            else { tf.eulerAngles = v3; }
        }

        #endregion

        public static void SetToIdentity(this Transform tf, bool isLocal)
        {
            if (tf == null) { return; }
            if (isLocal)
            {
                tf.localPosition = Vector3.zero;
                tf.localScale = Vector3.one;
                tf.localRotation = Quaternion.identity;
            }
            else
            {
                tf.position = Vector3.zero;
                tf.localScale = Vector3.one;
                tf.rotation = Quaternion.identity;
            }
        }

        /// <summary>
        /// Copy????????????????????????(????????????????????????)
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="isLocal"></param>
        public static void CopyBasicPropertyToAnthor(this Transform from, Transform to, bool isLocal)
        {
            if (from == null || to == null) { return; }
            if (isLocal)
            {
                to.localPosition = from.localPosition;
                to.localRotation = from.localRotation;
            }
            else
            {
                to.position = from.position;
                to.rotation = from.rotation;
            }
            to.localScale = from.localScale;
        }

        /// <summary>
        /// ???????????????????????????Parent??????
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        /// <param name="worldPositionStays"></param>
        public static void MoveSelfGOToAnotherParent(this Transform self, Transform parent, bool worldPositionStays)
        {
            if (self == null || parent == null || self.parent == parent) { return; }
            self.SetParent(parent, worldPositionStays);
        }

        /// <summary>
        /// ?????????????????????Transform????????????Go???????????????Parent??????
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        /// <param name="worldPositionStays"></param>
        /// <returns></returns>
        public static Transform CopySelfEmptyGoToAnotherParent(this Transform self, Transform parent, bool worldPositionStays)
        {
            if (self == null || parent == null || self.parent == parent) { return null; }
            GameObject go = new GameObject();
            Transform goT = go.transform;
            goT.name = self.name;
            goT.SetParent(self.parent);
            goT.localScale = self.localScale;
            goT.localPosition = self.localPosition;
            goT.localEulerAngles = self.localEulerAngles;
            goT.MoveSelfGOToAnotherParent(parent, worldPositionStays);
            return goT;
        }

        /// <summary>
        /// ??????????????????Array???????????????????????????ParentName?????????
        /// </summary>
        /// <param name="self"></param>
        /// <param name="array"></param>
        /// <param name="worldPositionStays"></param>
        public static void MoveSelfGOToInArrayWithSameParentName(this Transform self, Transform[] array, bool worldPositionStays)
        {
            if (self == null || self.parent == null || array == null || array.Length <= 0) { return; }
            string selfParentName = self.transform.parent.name;
            for (int i = 0; i < array.Length; i++)
            {
                Transform p = array[i];
                if (p.name == selfParentName)
                {
                    self.MoveSelfGOToAnotherParent(p, worldPositionStays);
                    break;
                }
            }
        }

        /// <summary>
        /// ??????????????????Array???????????????????????????ParentName?????????
        /// </summary>
        /// <param name="self"></param>
        /// <param name="array"></param>
        /// <param name="worldPositionStays"></param>
        public static void MoveSelfGOToInArrayWithSameParentName(this Transform self, List<Transform> array, bool worldPositionStays)
        {
            if (self == null || self.parent == null || array == null || array.Count <= 0) { return; }
            string selfParentName = self.transform.parent.name;
            for (int i = 0; i < array.Count; i++)
            {
                Transform p = array[i];
                if (p.name == selfParentName)
                {
                    self.MoveSelfGOToAnotherParent(p, worldPositionStays);
                    break;
                }
            }
        }

        /// <summary>
        /// ?????????????????????Transform????????????Go???????????????Array???????????????????????????ParentName?????????
        /// </summary>
        /// <param name="self"></param>
        /// <param name="array"></param>
        /// <param name="worldPositionStays"></param>
        /// <returns></returns>
        public static Transform CopySelfEmptyGoToInArrayWithSameParentName(this Transform self, Transform[] array, bool worldPositionStays)
        {
            if (self == null || self.parent == null || array == null || array.Length <= 0) { return null; }
            string selfParentName = self.transform.parent.name;
            for (int i = 0; i < array.Length; i++)
            {
                Transform p = array[i];
                if (p.name == selfParentName)
                {
                    return self.CopySelfEmptyGoToAnotherParent(p, worldPositionStays);
                }
            }
            return null;
        }

        /// <summary>
        /// ?????????????????????Transform????????????Go???????????????Array???????????????????????????ParentName?????????
        /// </summary>
        /// <param name="self"></param>
        /// <param name="array"></param>
        /// <param name="worldPositionStays"></param>
        /// <returns></returns>
        public static Transform CopySelfEmptyGoToInArrayWithSameParentName(this Transform self, List<Transform> array, bool worldPositionStays)
        {
            if (self == null || self.parent == null || array == null || array.Count <= 0) { return null; }
            string selfParentName = self.transform.parent.name;
            for (int i = 0; i < array.Count; i++)
            {
                Transform p = array[i];
                if (p.name == selfParentName)
                {
                    return self.CopySelfEmptyGoToAnotherParent(p, worldPositionStays);
                }
            }
            return null;
        }
    }
}