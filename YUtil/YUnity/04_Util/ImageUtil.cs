using UnityEngine;

namespace YUnity
{
    public class ImageUtil
    {
        /// <summary>
        /// 创建透明图片
        /// </summary>
        public static Sprite CreateTransparentSprite(int width, int height)
        {
            // 创建一个透明的Texture2D
            Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false);

            // 设置所有像素为透明
            Color transparentColor = new Color(0, 0, 0, 0);
            Color[] pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = transparentColor;
            }
            texture.SetPixels(pixels);
            texture.Apply();

            // 转换
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        }

        private static Sprite _transparentSprite = null;

        /// <summary>
        /// 大小为100*100的透明图片
        /// </summary>
        public static Sprite TransparentSprite
        {
            get
            {
                if (_transparentSprite == null)
                {
                    _transparentSprite = CreateTransparentSprite(100, 100);
                }
                return _transparentSprite;
            }
        }
    }
}