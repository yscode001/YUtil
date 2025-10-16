using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace YUnity
{
    public partial class AsyncImage : MonoBehaviourBaseY
    {
        private AsyncImage() { }
        public static AsyncImage Instance { get; private set; } = null;

        private readonly bool IsWebGL = Application.platform == RuntimePlatform.WebGLPlayer;
        private readonly string WebImgDirectory = Application.persistentDataPath + "/WebImgDirectory/";

        internal void Init()
        {
            Instance = this;
            if (IsWebGL == false && !Directory.Exists(WebImgDirectory))
            {
                Directory.CreateDirectory(WebImgDirectory);
            }
        }
    }
    #region 工具方法，网络请求
    public partial class AsyncImage
    {
        private void DownloadSprite(string url, Action<Sprite> complete)
        {
            StartCoroutine(DownloadAction(url, (tex) =>
            {
                complete?.Invoke(SpriteUtil.Generate(tex));
            }));
        }
        private void DownloadTexture2D(string url, Action<Texture2D> complete)
        {
            StartCoroutine(DownloadAction(url, complete));
        }
        private IEnumerator DownloadAction(string url, Action<Texture2D> complete)
        {
            using UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();
            Texture2D tex = request.result == UnityWebRequest.Result.Success ? DownloadHandlerTexture.GetContent(request) : null;
            complete?.Invoke(tex);
            yield break;
        }
    }
    #endregion
    public partial class AsyncImage
    {
        public void LoadSprite(string url, Action<Sprite> complete)
        {

        }
    }
}