using UnityEngine;

namespace YUnity
{
    public class CameraHelper
    {
        private CameraHelper() { }

        private static Camera main;

        public static Camera Main
        {
            get
            {
                if (main == null)
                {
                    main = Camera.main;
                }
                return main;
            }
        }
    }
}