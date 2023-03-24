using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    public static class TransformExt
    {
        #region 查找子物体
        public static Transform FindChildRecursively(this Transform parent, string name)
        {
            if (parent == null || string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            Transform child = parent.Find(name);
            if (child == null)
            {
                foreach (Transform tran in parent)
                {
                    child = FindChildRecursively(tran, name);
                    if (child != null)
                    {
                        return child;
                    }
                }
            }
            return child;
        }

        public static T FindChildRecursively<T>(this Transform parent, string name) where T : Component
        {
            Transform child = parent.FindChildRecursively(name);
            if (child == null) { return null; }
            return child.GetComponent<T>();
        }

        public static Transform FindChildByPath(this Transform parent, string path)
        {
            if (parent == null || string.IsNullOrWhiteSpace(path))
            {
                return null;
            }
            return parent.Find(path);
        }

        public static T FindChildByPath<T>(this Transform parent, string path) where T : Component
        {
            Transform child = parent.FindChildByPath(path);
            if (child == null) { return null; }
            return child.GetComponent<T>();
        }
        #endregion

        #region 查找子物体索引
        /// <summary>
        /// 查找子物体索引，错误返回-1
        /// </summary>
        /// <param name="childTransform"></param>
        public static int GetChildIndex(this Transform tf, Transform childTransform)
        {
            if (tf != null && childTransform != null)
            {
                for (int i = 0; i < tf.childCount; i++)
                {
                    if (tf.GetChild(i) == childTransform)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// 查找自己在父物体中的索引，错误返回-1
        /// </summary>
        /// <param name="tf"></param>
        /// <returns></returns>
        public static int GetSelfIndexInParent(this Transform tf)
        {
            if (tf.parent == null) { return -1; }
            return tf.parent.GetChildIndex(tf);
        }
        #endregion

        #region SetPosition

        public static void SetPositionX(this Transform tf, float x, bool isLocal)
        {
            Vector3 v3 = isLocal ? tf.localPosition : tf.position;
            v3.x = x;
            if (isLocal) { tf.localPosition = v3; }
            else { tf.position = v3; }
        }

        public static void SetPositionY(this Transform tf, float y, bool isLocal)
        {
            Vector3 v3 = isLocal ? tf.localPosition : tf.position;
            v3.y = y;
            if (isLocal) { tf.localPosition = v3; }
            else { tf.position = v3; }
        }

        public static void SetPositionZ(this Transform tf, float z, bool isLocal)
        {
            Vector3 v3 = isLocal ? tf.localPosition : tf.position;
            v3.z = z;
            if (isLocal) { tf.localPosition = v3; }
            else { tf.position = v3; }
        }

        public static void SetPositionXY(this Transform tf, float x, float y, bool isLocal)
        {
            Vector3 v3 = isLocal ? tf.localPosition : tf.position;
            v3.x = x;
            v3.y = y;
            if (isLocal) { tf.localPosition = v3; }
            else { tf.position = v3; }
        }

        public static void SetPositionXZ(this Transform tf, float x, float z, bool isLocal)
        {
            Vector3 v3 = isLocal ? tf.localPosition : tf.position;
            v3.x = x;
            v3.z = z;
            if (isLocal) { tf.localPosition = v3; }
            else { tf.position = v3; }
        }

        public static void SetPositionYZ(this Transform tf, float y, float z, bool isLocal)
        {
            Vector3 v3 = isLocal ? tf.localPosition : tf.position;
            v3.y = y;
            v3.z = z;
            if (isLocal) { tf.localPosition = v3; }
            else { tf.position = v3; }
        }

        public static void SetPositionXYZ(this Transform tf, float x, float y, float z, bool isLocal)
        {
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
            Vector3 v3 = tf.localScale;
            v3.x = x;
            tf.localScale = v3;
        }

        public static void SetLocalScaleY(this Transform tf, float y)
        {
            Vector3 v3 = tf.localScale;
            v3.y = y;
            tf.localScale = v3;
        }

        public static void SetLocalScaleZ(this Transform tf, float z)
        {
            Vector3 v3 = tf.localScale;
            v3.z = z;
            tf.localScale = v3;
        }

        public static void SetLocalScaleXY(this Transform tf, float x, float y)
        {
            Vector3 v3 = tf.localScale;
            v3.x = x;
            v3.y = y;
            tf.localScale = v3;
        }

        public static void SetLocalScaleXZ(this Transform tf, float x, float z)
        {
            Vector3 v3 = tf.localScale;
            v3.x = x;
            v3.z = z;
            tf.localScale = v3;
        }

        public static void SetLocalScaleYZ(this Transform tf, float y, float z)
        {
            Vector3 v3 = tf.localScale;
            v3.y = y;
            v3.z = z;
            tf.localScale = v3;
        }

        public static void SetLocalScaleXYZ(this Transform tf, float x, float y, float z)
        {
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
            Vector3 v3 = isLocal ? tf.localEulerAngles : tf.eulerAngles;
            v3.x = x;
            if (isLocal) { tf.localEulerAngles = v3; }
            else { tf.eulerAngles = v3; }
        }

        public static void SetEulerAnglesY(this Transform tf, float y, bool isLocal)
        {
            Vector3 v3 = isLocal ? tf.localEulerAngles : tf.eulerAngles;
            v3.y = y;
            if (isLocal) { tf.localEulerAngles = v3; }
            else { tf.eulerAngles = v3; }
        }

        public static void SetEulerAnglesZ(this Transform tf, float z, bool isLocal)
        {
            Vector3 v3 = isLocal ? tf.localEulerAngles : tf.eulerAngles;
            v3.z = z;
            if (isLocal) { tf.localEulerAngles = v3; }
            else { tf.eulerAngles = v3; }
        }

        public static void SetEulerAnglesXY(this Transform tf, float x, float y, bool isLocal)
        {
            Vector3 v3 = isLocal ? tf.localEulerAngles : tf.eulerAngles;
            v3.x = x;
            v3.y = y;
            if (isLocal) { tf.localEulerAngles = v3; }
            else { tf.eulerAngles = v3; }
        }

        public static void SetEulerAnglesXZ(this Transform tf, float x, float z, bool isLocal)
        {
            Vector3 v3 = isLocal ? tf.localEulerAngles : tf.eulerAngles;
            v3.x = x;
            v3.z = z;
            if (isLocal) { tf.localEulerAngles = v3; }
            else { tf.eulerAngles = v3; }
        }

        public static void SetEulerAnglesYZ(this Transform tf, float y, float z, bool isLocal)
        {
            Vector3 v3 = isLocal ? tf.localEulerAngles : tf.eulerAngles;
            v3.y = y;
            v3.z = z;
            if (isLocal) { tf.localEulerAngles = v3; }
            else { tf.eulerAngles = v3; }
        }

        public static void SetEulerAnglesXYZ(this Transform tf, float x, float y, float z, bool isLocal)
        {
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
        /// Copy基础属性至另一个(位置、角度、旋转)
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
        /// 把自己移动至另一个Parent下面
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
        /// 创建一个和自己Transform一样的空Go，然后放到Parent下面
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
        /// 把自己移动至Array中的某一个具有相同ParentName的下面
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
        /// 把自己移动至Array中的某一个具有相同ParentName的下面
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
        /// 创建一个和自己Transform一样的空Go，然后放到Array中的某一个具有相同ParentName的下面
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
        /// 创建一个和自己Transform一样的空Go，然后放到Array中的某一个具有相同ParentName的下面
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