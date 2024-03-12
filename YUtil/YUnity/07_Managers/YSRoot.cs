using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace YUnity
{
    public partial class YSRoot : MonoBehaviourBaseY
    {
        private YSRoot() { }
        public static YSRoot Instance { get; private set; } = null;
    }
    public partial class YSRoot
    {
        public static void Init(bool enableLog)
        {
            if (Instance != null)
            {
                return;
            }
            // 日志必须最先初始化
            LogTool.Init(enableLog);
            // 初始化YSRoot
            GameObject rootGO = GOUtil.CreateEmptyGO(null, "YSRoot");
            DontDestroyOnLoad(rootGO);
            Instance = rootGO.AddComponent<YSRoot>();
            // 初始化其他管理者
            rootGO.AddComponent<UIStackMag>().Init();
            rootGO.AddComponent<SceneMag>().Init();
            rootGO.AddComponent<AudioMag>().Init();
            rootGO.AddComponent<QueueMag>().Init();
            rootGO.AddComponent<TimeTaskMag>().Init();
        }
    }
    public partial class YSRoot
    {
        /// <summary>
        /// 逻辑驱动(队列任务和定时任务)
        /// </summary>
        public void LogicTick()
        {
            QueueMag.Instance?.LogicTick();
            TimeTaskMag.Instance?.LogicTick();
        }
    }
    public partial class YSRoot
    {
        public void Load(string fileFullPath, Action<byte[]> complete)
        {
            StartCoroutine(LoadAction(fileFullPath, complete));
        }
        private IEnumerator LoadAction(string fileFullPath, Action<byte[]> complete)
        {
            var request = UnityWebRequest.Get(new System.Uri(fileFullPath));
            yield return request.SendWebRequest();
            complete?.Invoke(request.downloadHandler.data);
        }
    }
}