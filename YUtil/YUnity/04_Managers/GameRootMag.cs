using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YUnity
{
    /// <summary>
    /// 游戏入口管理器
    /// </summary>
    public partial class GameRootMag : MonoBehaviourBaseY
    {
        private GameRootMag() { }
        public static GameRootMag Instance { get; private set; } = null;

        [HideInInspector]
        public ResourceMag ResourceMag { get; private set; } = null;

        [HideInInspector]
        public QueueMag QueueMag { get; private set; } = null;

        [HideInInspector]
        public SceneMag SceneMag { get; private set; } = null;

        [HideInInspector]
        public PoolMag PoolMag { get; private set; } = null;

        [HideInInspector]
        public UIStackMag UIStackMag { get; private set; } = null;

        [HideInInspector]
        public AudioMag AudioMag { get; private set; } = null;

        [HideInInspector]
        public TimeTaskMag TimeTaskMag { get; private set; } = null;
    }

    public partial class GameRootMag
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init(Scene scene, LogConfig logConfig = null, uint standardScreenWidth = 2160, uint standardScreenHeight = 1080)
        {
            if (Instance != null) { return; }
            if (logConfig == null)
            {
                logConfig = new LogConfig()
                {
                    IsEnableSave = false,
                };
            }
            LogTool.InitSettings(logConfig);
            LogTool.Log("初始化(YFramework)：游戏入口管理器(GameRootMag)");
            ScreenCfg.SetupData(standardScreenWidth, standardScreenHeight);
            GameObject grGO;
            if (scene != null && scene.GetRootGameObjects().Length > 0 && scene.GetRootGameObjects().FirstOrDefault(obj => obj.name == "YFrameworkGameRoot") != null)
            {
                grGO = scene.GetRootGameObjects().First(obj => obj.name == "YFrameworkGameRoot");
            }
            else
            {
                grGO = new GameObject
                {
                    name = "YFrameworkGameRoot"
                };
            }
            Instance = grGO.GetOrAddComponent<GameRootMag>();
            DontDestroyOnLoad(grGO);
            DontDestroyOnLoad(Instance);
            Instance.InitOthersAfterInit(logConfig);
        }

        private void InitOthersAfterInit(LogConfig logConfig = null)
        {
            ResourceMag = this.GetOrAddComponent<ResourceMag>();
            ResourceMag.Init();

            QueueMag = this.GetOrAddComponent<QueueMag>();
            QueueMag.Init();

            SceneMag = this.GetOrAddComponent<SceneMag>();
            SceneMag.Init();

            PoolMag = this.GetOrAddComponent<PoolMag>();
            PoolMag.Init();

            UIStackMag = this.GetOrAddComponent<UIStackMag>();
            UIStackMag.Init();

            AudioMag = this.GetOrAddComponent<AudioMag>();
            AudioMag.Init();

            TimeTaskMag = this.GetOrAddComponent<TimeTaskMag>();
            TimeTaskMag.Init();
        }

        private void OnDestroy()
        {
            this.Log("销毁(YFramework)：游戏入口管理器(GameRootMag)");
            Instance = null;
        }
    }
}