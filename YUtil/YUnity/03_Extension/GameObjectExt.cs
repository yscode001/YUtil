using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YUnity
{
    public static class GameObjectExt
    {
        /// <summary>
        /// 获取GameObject的组件，如果没有此组件，先添加组件后再获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            if (go == null) { return null; }
            T com = go.GetComponent<T>();
            if (com != null) { return com; }
            return go.AddComponent<T>();
        }

        /// <summary>
        /// GameObject身上是否有某类型的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static bool HasComponent<T>(this GameObject go) where T : Component
        {
            if (go == null) { return false; }
            return go.GetComponent<T>() != null;
        }

        /// <summary>
        /// 设置GameObject是否激活或禁用
        /// </summary>
        /// <param name="go"></param>
        /// <param name="active"></param>
        public static void SetAct(this GameObject go, bool active)
        {
            if (go == null) { return; }
            if (go.activeSelf != active) { go.SetActive(active); }
        }

        /// <summary>
        /// 设置父物体
        /// </summary>
        /// <param name="go"></param>
        /// <param name="parentGO"></param>
        /// <returns>返回自己</returns>
        public static GameObject SetToParent(this GameObject go, GameObject parentGO)
        {
            if (go == null || parentGO == null) { return go; }
            go.transform.SetParent(parentGO.transform, false);
            return go;
        }

        /// <summary>
        /// 添加子物体
        /// </summary>
        /// <param name="go"></param>
        /// <param name="children"></param>
        public static void AddChildren(this GameObject go, List<GameObject> children)
        {
            if (go == null) { return; }
            foreach (GameObject child in children)
            {
                if (child == null) { continue; }
                child.transform.SetParent(go.transform, false);
            }
        }

        /// <summary>
        /// 设置名称和标签，空值不设置
        /// </summary>
        /// <param name="go"></param>
        /// <param name="name"></param>
        /// <param name="tag"></param>
        public static void SetupNameAndTag(this GameObject go, string name, string tag)
        {
            if (go == null) { return; }
            if (!string.IsNullOrWhiteSpace(name)) { go.name = name; }
            if (!string.IsNullOrWhiteSpace(tag)) { go.tag = tag; }
        }

        /// <summary>
        /// 移除游戏物体的某个组件(如果存在)
        /// </summary>
        /// <typeparam name="T">即将移除的组件类型</typeparam>
        /// <param name="go"></param>
        public static void RemoveComponent<T>(this GameObject go) where T : Component
        {
            if (go == null) { return; }
            T com = go.GetComponent<T>();
            if (com != null)
            {
                UnityEngine.MonoBehaviour.Destroy(com);
            }
        }

        /// <summary>
        /// 启用或禁用某个Behaviour组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <param name="enable"></param>
        public static void EnableOrDisableComponent<T>(this GameObject go, bool enable) where T : Behaviour
        {
            if (go == null) { return; }
            T com = go.GetComponent<T>();
            if (enable)
            {
                if (com == null)
                {
                    com = go.AddComponent<T>();
                }
                com.enabled = true;
            }
            else
            {
                if (com != null)
                {
                    com.enabled = false;
                }
            }
        }

        /// <summary>
        /// 销毁自己的游戏物体
        /// </summary>
        /// <param name="component"></param>
        public static void DestroyGO(this GameObject go)
        {
            if (go == null) { return; }
            GameObject.Destroy(go);
        }

        /// <summary>
        /// 销毁所有的子物体(先子物体后自己)
        /// </summary>
        /// <param name="go"></param>
        public static void DestroyAllChild(this GameObject go)
        {
            if (go == null) { return; }
            for (int i = go.transform.childCount - 1; i >= 0; i--)
            {
                GameObject.Destroy(go.transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// 销毁所有的子物体
        /// </summary>
        /// <param name="go"></param>
        /// <param name="expect"></param>
        public static void DestroyAllChild(this GameObject go, List<GameObject> expect)
        {
            if (go == null) { return; }
            for (int i = go.transform.childCount - 1; i >= 0; i--)
            {
                GameObject childGO = go.transform.GetChild(i).gameObject;
                if (expect != null && expect.Contains(childGO))
                {
                    continue;
                }
                GameObject.Destroy(childGO);
            }
        }

        /// <summary>
        /// 获取子物体的组件(childPath为空，获取自身组件)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <param name="childPath">子物体路径</param>
        /// <returns></returns>
        public static T GetChildOrSelfComponent<T>(this GameObject go, string childPath = null) where T : Component
        {
            if (go == null) { return null; }
            if (string.IsNullOrWhiteSpace(childPath)) { return go.GetComponent<T>(); }
            Transform childT = go.transform.Find(childPath);
            if (childT != null) { return childT.GetComponent<T>(); }
            return null;
        }
    }
}