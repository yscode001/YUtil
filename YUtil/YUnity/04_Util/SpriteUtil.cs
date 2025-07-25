﻿// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-7-28
// ------------------------------

using UnityEngine;

namespace YUnity
{
    public class SpriteUtil
    {
        public static Sprite Generate(int width, int height, Color[] colors, bool updateMipmaps = true, bool makeNoLongerReadable = false)
        {
            if (width <= 0 || height <= 0 || colors == null || colors.Length != width * height) { return null; }
            Texture2D texture = new UnityEngine.Texture2D(width, height);
            texture.SetPixels(colors);
            texture.Apply(updateMipmaps, makeNoLongerReadable);
            return Generate(texture);
        }
        public static Sprite Generate(int width, int height, Color32[] colors, bool updateMipmaps = true, bool makeNoLongerReadable = false)
        {
            if (width <= 0 || height <= 0 || colors == null || colors.Length != width * height) { return null; }
            Texture2D texture = new UnityEngine.Texture2D(width, height);
            texture.SetPixels32(colors);
            texture.Apply(updateMipmaps, makeNoLongerReadable);
            return Generate(texture);
        }
        public static Sprite Generate(int width, int height, byte[] imgBytes)
        {
            if (width <= 0 || height <= 0 || imgBytes == null || imgBytes.Length <= 0) { return null; }
            Texture2D texture = new UnityEngine.Texture2D(width, height);
            if (texture.LoadImage(imgBytes))
            {
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            }
            else
            {
                return null;
            }
        }
        public static Sprite Generate(Texture2D texture)
        {
            if (texture != null)
            {
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 生成透明图片
        /// </summary>
        public static Sprite GenerateTransparentSprite(int width, int height)
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
                    _transparentSprite = GenerateTransparentSprite(100, 100);
                }
                return _transparentSprite;
            }
        }
    }
}