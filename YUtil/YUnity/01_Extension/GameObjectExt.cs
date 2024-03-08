using UnityEngine;
using UnityEngine.SceneManagement;

namespace YUnity
{
    public static class GameObjectExt
    {
        /// <summary>
        /// 获取组件(如果没有，先添加后再获取)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            T com = go.GetComponent<T>();
            if (com != null) { return com; }
            return go.AddComponent<T>();
        }

        /// <summary>
        /// 启用或禁用组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <param name="enable"></param>
        public static void EnableOrDisableComponent<T>(this GameObject go, bool enable) where T : Behaviour
        {
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
        /// 销毁组件(如果存在)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <param name="immediate"></param>
        public static void DestroyComponent<T>(this GameObject go, bool immediate = false) where T : Component
        {
            T com = go.GetComponent<T>();
            if (com != null)
            {
                if (immediate)
                {
                    Object.DestroyImmediate(com);
                }
                else
                {
                    Object.Destroy(com);
                }
            }
        }

        /// <summary>
        /// 设置激活状态
        /// </summary>
        /// <param name="go"></param>
        /// <param name="active"></param>
        public static void SetAct(this GameObject go, bool active)
        {
            if (go.activeSelf != active)
            {
                go.SetActive(active);
            }
        }

        /// <summary>
        /// 所在场景
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        public static Scene? CurrentScene(this GameObject go)
        {
            return go.scene;
        }

        /// <summary>
        /// 销毁自己
        /// </summary>
        /// <param name="go"></param>
        /// <param name="immediate"></param>
        public static void DestroySelf(this GameObject go, bool immediate = false)
        {
            if (immediate)
            {
                Object.DestroyImmediate(go);
            }
            else
            {
                Object.Destroy(go);
            }
        }

        /// <summary>
        /// 销毁所有子物体
        /// </summary>
        /// <param name="go"></param>
        /// <param name="immediate"></param>
        public static void DestroyAllChild(this GameObject go, bool immediate = false)
        {
            Transform goT = go.transform;
            if (goT.childCount > 0)
            {
                int totalCount = goT.childCount;
                for (int i = totalCount - 1; i >= 0; i--)
                {
                    if (immediate)
                    {
                        Object.DestroyImmediate(goT.GetChild(i).gameObject);
                    }
                    else
                    {
                        Object.Destroy(goT.GetChild(i).gameObject);
                    }
                }
            }
        }
    }
}