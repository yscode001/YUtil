using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    public partial class ResourceMag : MonoBehaviourBaseY
    {
        private ResourceMag() { }
        public static ResourceMag Instance { get; private set; } = null;

        internal void Init()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }

    #region 加载音效
    public partial class ResourceMag
    {
        private readonly Dictionary<string, AudioClip> acDic = new Dictionary<string, AudioClip>();

        /// <summary>
        /// 加载音效
        /// </summary>
        /// <param name="resourcesPath">完整路径</param>
        /// <param name="cache">是否缓存</param>
        /// <returns></returns>
        public AudioClip LoadAudio(string resourcesPath, bool cache = false)
        {
            if (string.IsNullOrWhiteSpace(resourcesPath))
            {
                return null;
            }
            if (acDic.TryGetValue(resourcesPath, out AudioClip ac))
            {
                return ac;
            }
            else
            {
                ac = Resources.Load<AudioClip>(resourcesPath);
                if (ac == null) { return null; }
                if (cache)
                {
                    if (acDic.ContainsKey(resourcesPath))
                    {
                        acDic[resourcesPath] = ac;
                    }
                    else
                    {
                        acDic.Add(resourcesPath, ac);
                    }
                }
                return ac;
            }
        }
    }
    #endregion

    #region 加载prefab，实例化go
    public partial class ResourceMag
    {
        private readonly Dictionary<string, GameObject> goPrefabDic = new Dictionary<string, GameObject>();

        /// <summary>
        /// 根据prefab路径加载游戏物体
        /// </summary>
        /// <param name="prefabResourcesPath">Prefab完整路径</param>
        /// <param name="goName">生成的游戏物体名字</param>
        /// <param name="cache">是否缓存Prefab</param>
        /// <returns></returns>
        public GameObject LoadInstanceGo(string prefabResourcesPath, string goName = null, bool cache = false)
        {
            if (string.IsNullOrWhiteSpace(prefabResourcesPath))
            {
                return null;
            }
            GameObject prefabGO = null;
            if (goPrefabDic.TryGetValue(prefabResourcesPath, out GameObject prefab))
            {
                prefabGO = prefab;
            }
            else
            {
                prefabGO = Resources.Load<GameObject>(prefabResourcesPath);
            }
            if (prefabGO == null)
            {
                return null;
            }
            else
            {
                if (cache)
                {
                    if (goPrefabDic.ContainsKey(prefabResourcesPath))
                    {
                        goPrefabDic[prefabResourcesPath] = prefabGO;
                    }
                    else
                    {
                        goPrefabDic.Add(prefabResourcesPath, prefabGO);
                    }
                }
                GameObject go = Instantiate(prefabGO);
                if (!string.IsNullOrWhiteSpace(goName))
                {
                    go.name = goName;
                }
                return go;
            }
        }
    }
    #endregion

    #region 加载Sprite
    public partial class ResourceMag
    {
        private readonly Dictionary<string, Sprite> spDic = new Dictionary<string, Sprite>();

        /// <summary>
        /// 加载Sprite
        /// </summary>
        /// <param name="resourcesPath"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        public Sprite LoadSprite(string resourcesPath, bool cache = false)
        {
            if (string.IsNullOrWhiteSpace(resourcesPath))
            {
                return null;
            }
            if (spDic.TryGetValue(resourcesPath, out Sprite sp))
            {
                return sp;
            }
            else
            {
                sp = Resources.Load<Sprite>(resourcesPath);
                if (sp == null) { return null; }
                if (cache)
                {
                    if (spDic.ContainsKey(resourcesPath))
                    {
                        spDic[resourcesPath] = sp;
                    }
                    else
                    {
                        spDic.Add(resourcesPath, sp);
                    }
                }
                return sp;
            }
        }
    }
    #endregion
}