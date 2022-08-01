using UnityEngine;
using UnityEngine.SceneManagement;

namespace YUnity
{
    /// <summary>
    /// YS框架入口管理
    /// </summary>
    public partial class YSRoot : MonoBehaviourBaseY
    {
        private YSRoot() { }
        public static YSRoot Instance { get; private set; } = null;

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
    public partial class YSRoot
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init(Scene scene, LogConfig logConfig = null)
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
            GameObject rootGO = GOUtil.CreateEmptyGO(null, "YSRoot");
            Instance = rootGO.AddComponent<YSRoot>();
            DontDestroyOnLoad(rootGO);
            Instance.InitOthersAfterInit();
        }

        private void InitOthersAfterInit()
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
            Instance = null;
        }
    }
}