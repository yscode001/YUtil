using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YUnity
{
    public static class ComponentExt
    {
        /// <summary>
        /// 获取此脚本对应的GameObject的组件，如果没有此组件，先添加组件后再获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            if (component == null || component.gameObject == null) { return null; }
            T com = component.GetComponent<T>();
            if (com != null) { return com; }
            return component.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// 获取此脚本对应的GameObject的组件，如果没有此组件，返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public static T GetComponent<T>(this Component component) where T : Component
        {
            if (component == null || component.gameObject == null) { return null; }
            return component.GetComponent<T>();
        }

        /// <summary>
        /// 此脚本对应的GameObject身上如果没有组件，则添加，否则直接返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        public static void AddComponentIfHasNot<T>(this Component component) where T : Component
        {
            if (component == null || component.gameObject == null) { return; }
            if (component.GetComponent<T>() != null) { return; }
            component.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// 此脚本对应的GameObject身上是否有某类型的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public static bool HasComponent<T>(this Component component) where T : Component
        {
            if (component == null || component.gameObject == null) { return false; }
            return component.GetComponent<T>() != null;
        }

        /// <summary>
        /// 设置此脚本对应的GameObject是否激活或禁用
        /// </summary>
        /// <param name="component"></param>
        /// <param name="active"></param>
        public static void SetAct(this Component component, bool active)
        {
            if (component == null || component.gameObject == null) { return; }
            if (component.gameObject.activeSelf != active) { component.gameObject.SetActive(active); }
        }

        /// <summary>
        /// 组件所在游戏物体的场景
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static Scene? CurrentScene(this Component component)
        {
            if (component == null || component.gameObject == null) { return null; }
            return component.gameObject.scene;
        }

        /// <summary>
        /// 给当前游戏物体设置父物体
        /// </summary>
        /// <param name="component"></param>
        /// <param name="parentCont"></param>
        /// <returns>返回自己</returns>
        public static Component SetToParent(this Component component, Component parentCont)
        {
            if (component == null || component.transform == null || parentCont == null || parentCont.transform == null) { return component; }
            component.transform.SetParent(parentCont.transform, false);
            return component;
        }

        /// <summary>
        /// 给当前游戏物体添加子物体
        /// </summary>
        /// <param name="component"></param>
        /// <param name="children"></param>
        public static void AddChildren(this Component component, List<Component> children)
        {
            if (component == null || component.transform == null) { return; }
            foreach (Component child in children)
            {
                if (child == null || child.transform == null) { continue; }
                child.transform.SetParent(component.transform, false);
            }
        }

        /// <summary>
        /// 给当前游戏物体设置名称和标签，空值不设置
        /// </summary>
        /// <param name="component"></param>
        /// <param name="name"></param>
        /// <param name="tag"></param>
        public static void SetupNameAndTag(this Component component, string name, string tag)
        {
            if (component == null || component.gameObject == null) { return; }
            if (!string.IsNullOrWhiteSpace(name)) { component.gameObject.name = name; }
            if (!string.IsNullOrWhiteSpace(tag)) { component.tag = tag; }
        }

        /// <summary>
        /// 销毁组件所在的游戏物体
        /// </summary>
        /// <param name="component"></param>
        public static void DestroyGO(this Component component)
        {
            if (component == null || component.gameObject == null) { return; }
            GameObject.Destroy(component.gameObject);
        }

        /// <summary>
        /// 销毁所有的子物体(先子物体后自己)
        /// </summary>
        /// <param name="component"></param>
        /// <param name="includeSelf">是否包含自己</param>
        public static void DestroyAllChild(this Component component)
        {
            if (component == null || component.gameObject == null) { return; }
            for (int i = component.transform.childCount - 1; i >= 0; i--)
            {
                GameObject.Destroy(component.transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// 销毁所有的子物体
        /// </summary>
        /// <param name="component"></param>
        /// <param name="expect"></param>
        public static void DestroyAllChild(this Component component, List<GameObject> except)
        {
            if (component == null || component.gameObject == null) { return; }
            for (int i = component.transform.childCount - 1; i >= 0; i--)
            {
                GameObject childGO = component.transform.GetChild(i).gameObject;
                if (except != null && except.Contains(childGO))
                {
                    continue;
                }
                GameObject.Destroy(childGO);
            }
        }

        /// <summary>
        /// 移除此组件所在游戏物体上的某个组件(如果存在)
        /// </summary>
        /// <typeparam name="T">即将移除的组件类型</typeparam>
        /// <param name="component"></param>
        public static void RemoveComponent<T>(this Component component) where T : Component
        {
            if (component == null || component.gameObject == null) { return; }
            T com = component.GetComponent<T>();
            if (com != null)
            {
                UnityEngine.MonoBehaviour.Destroy(com);
            }
        }

        /// <summary>
        /// 启用或禁用此组件所在游戏物体上的某个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <param name="enable"></param>
        public static void EnableOrDisableComponent<T>(this Component component, bool enable) where T : Behaviour
        {
            if (component == null || component.gameObject == null) { return; }
            T com = component.GetComponent<T>();
            if (enable)
            {
                if (com == null)
                {
                    com = component.gameObject.AddComponent<T>();
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
        /// 设置组件所在游戏物体及子物体的Layer
        /// </summary>
        /// <param name="component"></param>
        /// <param name="layer"></param>
        /// <param name="onlyFirstLevelChildren">True仅第一级子孩子，False为所有的孩子和孙子</param>
        /// <param name="except"></param>
        public static void SetupItAndChildrenLayer(this Component component, int layer, bool onlyFirstLevelChildren, List<GameObject> except)
        {
            if (component == null || component.gameObject == null) { return; }
            component.gameObject.layer = layer;
            for (int i = component.transform.childCount - 1; i >= 0; i--)
            {
                Transform childT = component.transform.GetChild(i);
                if (except != null && except.Contains(childT.gameObject))
                {
                    continue;
                }
                childT.gameObject.layer = layer;
                if (onlyFirstLevelChildren)
                {
                    continue;
                }
                ComponentExt.SetupItAndChildrenLayer(childT, layer, onlyFirstLevelChildren, except);
            }
        }

        /// <summary>
        /// 设置组件所在游戏物体及子物体的Tag
        /// </summary>
        /// <param name="component"></param>
        /// <param name="tag"></param>
        /// <param name="onlyFirstLevelChildren">True仅第一级子孩子，False为所有的孩子和孙子</param>
        /// <param name="except"></param>
        public static void SetupItAndChildrenTag(this Component component, string tag, bool onlyFirstLevelChildren, List<GameObject> except)
        {
            if (component == null || component.gameObject == null || string.IsNullOrWhiteSpace(tag)) { return; }
            component.gameObject.tag = tag;
            for (int i = component.transform.childCount - 1; i >= 0; i--)
            {
                Transform childT = component.transform.GetChild(i);
                if (except != null && except.Contains(childT.gameObject))
                {
                    continue;
                }
                childT.gameObject.tag = tag;
                if (onlyFirstLevelChildren)
                {
                    continue;
                }
                ComponentExt.SetupItAndChildrenTag(childT, tag, onlyFirstLevelChildren, except);
            }
        }

        /// <summary>
        /// 获取子物体的组件(childPath为空，获取自身组件)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <param name="childPath">子物体路径</param>
        /// <returns></returns>
        public static T GetChildOrSelfComponent<T>(this Component component, string childPath = null) where T : Component
        {
            if (component == null || component.gameObject == null) { return null; }
            if (string.IsNullOrWhiteSpace(childPath)) { return component.gameObject.GetComponent<T>(); }
            Transform childT = component.gameObject.transform.Find(childPath);
            if (childT != null) { return childT.GetComponent<T>(); }
            return null;
        }

        /// <summary>
        /// 是否激活
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static bool ActiveInHierarchy(this Component component)
        {
            if (component == null || component.gameObject == null) { return false; }
            return component.gameObject.activeInHierarchy;
        }

        /// <summary>
        /// 是否激活
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static bool ActiveSelf(this Component component)
        {
            if (component == null || component.gameObject == null) { return false; }
            return component.gameObject.activeSelf;
        }
    }
}