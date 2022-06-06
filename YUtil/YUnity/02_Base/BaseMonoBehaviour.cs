using UnityEngine;

namespace YUnity
{
    public partial class BaseMonoBehaviour : MonoBehaviour
    {
        private Transform _transform;
        private RectTransform _rectTransform;
        private GameObject _gameObject;
        private Animator _animator;
        private AudioSource _audioSource;
        private AudioListener _audioListener;
        private CharacterController _characterController;
        private Rigidbody _rigidbody;
    }
    public partial class BaseMonoBehaviour
    {
        public Transform TransformY
        {
            get
            {
                if (_transform != null) { return _transform; }
                _transform = transform;
                return _transform;
            }
        }

        /// <summary>
        /// UI专用
        /// </summary>
        public RectTransform RectTransformY
        {
            get
            {
                if (_rectTransform != null) { return _rectTransform; }
                _rectTransform = gameObject.GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        public GameObject GameObjectY
        {
            get
            {
                if (_gameObject != null) { return _gameObject; }
                _gameObject = gameObject;
                return _gameObject;
            }
        }

        /// <summary>
        /// 获取Animator，如果没有则添加
        /// </summary>
        public Animator AnimatorY
        {
            get
            {
                if (_animator != null) { return _animator; }
                _animator = gameObject.GetComponent<Animator>();
                if (_animator != null) { return _animator; }
                _animator = gameObject.AddComponent<Animator>();
                return _animator;
            }
        }

        /// <summary>
        /// 获取AudioSource，如果没有则添加
        /// </summary>
        public AudioSource AudioSourceY
        {
            get
            {
                if (_audioSource != null) { return _audioSource; }
                _audioSource = gameObject.GetComponent<AudioSource>();
                if (_audioSource != null) { return _audioSource; }
                _audioSource = gameObject.AddComponent<AudioSource>();
                return _audioSource;
            }
        }

        /// <summary>
        /// 获取AudioListener，如果没有则添加
        /// </summary>
        public AudioListener AudioListenerY
        {
            get
            {
                if (_audioListener != null) { return _audioListener; }
                _audioListener = gameObject.GetComponent<AudioListener>();
                if (_audioListener != null) { return _audioListener; }
                _audioListener = gameObject.AddComponent<AudioListener>();
                return _audioListener;
            }
        }

        /// <summary>
        /// 获取CharacterController，如果没有则添加
        /// </summary>
        public CharacterController CharacterControllerY
        {
            get
            {
                if (_characterController != null) { return _characterController; }
                _characterController = gameObject.GetComponent<CharacterController>();
                if (_characterController != null) { return _characterController; }
                _characterController = gameObject.AddComponent<CharacterController>();
                return _characterController;
            }
        }

        /// <summary>
        /// 获取Rigidbody，如果没有则添加
        /// </summary>
        public Rigidbody RigidbodyY
        {
            get
            {
                if (_rigidbody != null) { return _rigidbody; }
                _rigidbody = gameObject.GetComponent<Rigidbody>();
                if (_rigidbody != null) { return _rigidbody; }
                _rigidbody = gameObject.AddComponent<Rigidbody>();
                return _rigidbody;
            }
        }
    }
}