using UnityEngine;

namespace YUnity
{
    public partial class BaseMonoBehaviour : MonoBehaviour
    {
        private Transform _transform;
        public Transform TransformY
        {
            get
            {
                if (_transform == null) { _transform = transform; }
                return _transform;
            }
        }

        private RectTransform _rectTransform;
        public RectTransform RectTransformY
        {
            get
            {
                if (_rectTransform == null) { _rectTransform = gameObject.GetComponent<RectTransform>(); }
                return _rectTransform;
            }
        }

        private GameObject _gameObject;
        public GameObject GameObjectY
        {
            get
            {
                if (_gameObject == null) { _gameObject = gameObject; }
                return _gameObject;
            }
        }
    }
}