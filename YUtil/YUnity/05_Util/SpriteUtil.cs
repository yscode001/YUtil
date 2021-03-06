// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-7-28
// ------------------------------

using UnityEngine;

namespace YUnity
{
    public class SpriteUtil
    {
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
    }
}