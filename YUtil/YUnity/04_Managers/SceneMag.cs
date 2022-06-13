using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YUnity
{
    /// <summary>
    /// 场景管理器
    /// </summary>
    public partial class SceneMag : MonoBehaviourBaseY
    {
        private SceneMag() { }
        public static SceneMag Instance { get; private set; } = null;

        internal void Init()
        {
            this.Log("初始化(YFramework)：场景管理器(SceneMag)");
            Instance = this;
        }

        private void OnDestroy()
        {
            this.Log("销毁(YFramework)：场景管理器(SceneMag)");
            Instance = null;
        }
    }
    public partial class SceneMag
    {
        private Action progressCB = null;

        // 对外的回调
        private float progressValue = 0f;
        private Action<float> progressCallback = null;
        private Action complete = null;

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="begin"></param>
        /// <param name="progress"></param>
        /// <param name="complete"></param>
        public void LoadSceneAsync(string sceneName, Action begin, Action<float> progress, Action complete)
        {
            if (progressCB != null) { return; } // 上一场景还在加载
            progressValue = 0f;
            begin?.Invoke();
            progressCallback = progress;
            this.complete = complete;
            AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName);
            progressCB = () =>
            {
                progressValue = sceneAsync.progress;
                if (progressValue < 1)
                {
                    this.progressCallback?.Invoke(progressValue);
                }
                else if (progressValue >= 1)
                {
                    progressValue = 0f;
                    progressCallback = null;
                    this.complete?.Invoke();
                    this.complete = null;
                    sceneAsync = null;
                    progressCB = null;
                }
            };
        }
        private void Update()
        {
            progressCB?.Invoke();
        }
    }
}