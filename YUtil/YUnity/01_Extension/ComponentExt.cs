using UnityEngine;
using UnityEngine.SceneManagement;

namespace YUnity
{
    public static class ComponentExt
    {
        /// <summary>
        /// 获取组件(如果没有，先添加后再获取)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            return component.gameObject.GetOrAddComponent<T>();
        }

        /// <summary>
        /// 启用或禁用组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <param name="enable"></param>
        public static void EnableOrDisableComponent<T>(this Component component, bool enable) where T : Behaviour
        {
            component.gameObject.EnableOrDisableComponent<T>(enable);
        }

        /// <summary>
        /// 销毁组件(如果存在)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        public static void DestroyComponent<T>(this Component component) where T : Component
        {
            component.gameObject.DestroyComponent<T>();
        }

        /// <summary>
        /// 设置激活状态
        /// </summary>
        /// <param name="component"></param>
        /// <param name="active"></param>
        public static void SetAct(this Component component, bool active)
        {
            component.gameObject.SetAct(active);
        }

        /// <summary>
        /// 所在场景
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static Scene? CurrentScene(this Component component)
        {
            return component.gameObject.CurrentScene();
        }

        /// <summary>
        /// 销毁自己
        /// </summary>
        /// <param name="component"></param>
        /// <param name="immediate"></param>
        public static void DestroySelf(this Component component, bool immediate = false)
        {
            component.gameObject.DestroySelf(immediate);
        }

        /// <summary>
        /// 销毁所有子物体
        /// </summary>
        /// <param name="component"></param>
        /// <param name="immediate"></param>
        public static void DestroyAllChild(this Component component, bool immediate = false)
        {
            component.gameObject.DestroyAllChild(immediate);
        }
    }
}