using UnityEngine;

namespace YUnity
{
    public partial class YSRoot : MonoBehaviourBaseY
    {
        private YSRoot() { }
        public static YSRoot Instance { get; private set; } = null;
    }
    public partial class YSRoot
    {
        public static void Init()
        {
            if (Instance != null)
            {
                return;
            }
            // 1.初始化YSRoot
            GameObject rootGO = GOUtil.CreateEmptyGO(null, "YSRoot");
            DontDestroyOnLoad(rootGO);
            Instance = rootGO.AddComponent<YSRoot>();
            // 2.初始化其他管理者(主要是为了使用单例)
            rootGO.AddComponent<UIStackMag>().Init();
            rootGO.AddComponent<SceneMag>().Init();
            rootGO.AddComponent<HttpMag>().Init();
            rootGO.AddComponent<AsyncImage>().Init();
        }
    }
}