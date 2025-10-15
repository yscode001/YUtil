using System.IO;
using UnityEngine;
using UnityEngine.UI;

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
    public partial class AsyncImage
    {
        public void Load(Image image, string url)
        {

        }
    }
}