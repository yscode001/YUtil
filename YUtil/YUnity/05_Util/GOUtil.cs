using UnityEngine;

namespace YUnity
{
    public class GOUtil
    {
        public static GameObject CreateEmptyGO(Transform parentT, string name)
        {
            GameObject go = new GameObject
            {
                name = name
            };
            go.transform.parent = parentT;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            return go;
        }
    }
}