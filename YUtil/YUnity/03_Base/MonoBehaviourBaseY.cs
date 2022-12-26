using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

namespace YUnity
{
    #region 常用缓存属性
    public partial class MonoBehaviourBaseY : MonoBehaviour
    {
        private Transform _transform;
        private RectTransform _rectTransform;
        private GameObject _gameObject;
        private PlayableDirector _playableDirector;
        private Animator _animator;
        private VideoPlayer _videoPlayer;
        private AudioSource _audioSource;
        private AudioListener _audioListener;
        private CharacterController _characterController;
        private Rigidbody _rigidbody;
        private CanvasGroup _canvasGroup;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private HumanBodyBoneUtil _humanBodyBoneUtil;
    }
    public partial class MonoBehaviourBaseY
    {
        /// <summary>
        /// 缓存属性
        /// </summary>
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
        /// 缓存属性，UI专用
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

        /// <summary>
        /// 缓存属性
        /// </summary>
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
        /// 获取PlayableDirector，如果没有则添加
        /// </summary>
        public PlayableDirector PlayableDirectorY
        {
            get
            {
                if (_playableDirector != null) { return _playableDirector; }
                _playableDirector = gameObject.GetComponent<PlayableDirector>();
                if (_playableDirector != null) { return _playableDirector; }
                _playableDirector = gameObject.AddComponent<PlayableDirector>();
                return _playableDirector;
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
        /// 获取VideoPlayer，如果没有则添加
        /// </summary>
        public VideoPlayer VideoPlayerY
        {
            get
            {
                if (_videoPlayer != null) { return _videoPlayer; }
                _videoPlayer = gameObject.GetComponent<VideoPlayer>();
                if (_videoPlayer != null) { return _videoPlayer; }
                _videoPlayer = gameObject.AddComponent<VideoPlayer>();
                return _videoPlayer;
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

        /// <summary>
        /// 获取CanvasGroup(UI专用)，如果没有则添加
        /// </summary>
        public CanvasGroup CanvasGroupY
        {
            get
            {
                if (_canvasGroup != null) { return _canvasGroup; }
                _canvasGroup = gameObject.GetComponent<CanvasGroup>();
                if (_canvasGroup != null) { return _canvasGroup; }
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
                return _canvasGroup;
            }
        }

        /// <summary>
        /// 获取MeshFilter，如果没有则添加
        /// </summary>
        public MeshFilter MeshFilterY
        {
            get
            {
                if (_meshFilter != null) { return _meshFilter; }
                _meshFilter = gameObject.GetComponent<MeshFilter>();
                if (_meshFilter != null) { return _meshFilter; }
                _meshFilter = gameObject.AddComponent<MeshFilter>();
                return _meshFilter;
            }
        }

        /// <summary>
        /// 获取MeshRenderer，如果没有则添加
        /// </summary>
        public MeshRenderer MeshRendererY
        {
            get
            {
                if (_meshRenderer != null) { return _meshRenderer; }
                _meshRenderer = gameObject.GetComponent<MeshRenderer>();
                if (_meshRenderer != null) { return _meshRenderer; }
                _meshRenderer = gameObject.AddComponent<MeshRenderer>();
                return _meshRenderer;
            }
        }

        /// <summary>
        /// 获取SkinnedMeshRenderer，如果没有则添加
        /// </summary>
        public SkinnedMeshRenderer SkinnedMeshRendererY
        {
            get
            {
                if (_skinnedMeshRenderer != null) { return _skinnedMeshRenderer; }
                _skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
                if (_skinnedMeshRenderer != null) { return _skinnedMeshRenderer; }
                _skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
                return _skinnedMeshRenderer;
            }
        }
    }
    #endregion

    #region 其他缓存属性
    public partial class MonoBehaviourBaseY
    {
        private Dictionary<string, Component> _comDict;
        private Dictionary<string, Component> comDict
        {
            get
            {
                if (_comDict == null) { _comDict = new Dictionary<string, Component>(); }
                return _comDict;
            }
        }

        /// <summary>
        /// 获取缓存的组件，如果没有此组件将添加该组件并缓存起来
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ComponentOfCache<T>() where T : Component
        {
            if (comDict.TryGetValue(typeof(T).Name, out Component com))
            {
                return (T)com;
            }
            else
            {
                com = gameObject.AddComponent<T>();
                comDict.Add(typeof(T).Name, com);
                return (T)com;
            }
        }
    }
    #endregion

    #region 人体骨骼
    public partial class MonoBehaviourBaseY
    {
        /// <summary>
        /// 人体骨骼工具缓存
        /// </summary>
        public HumanBodyBoneUtil HumanBodyBoneUtil
        {
            get
            {
                if (_humanBodyBoneUtil != null) { return _humanBodyBoneUtil; }
                if (AnimatorY != null)
                {
                    _humanBodyBoneUtil = new HumanBodyBoneUtil(AnimatorY);
                }
                else
                {
                    _humanBodyBoneUtil = new HumanBodyBoneUtil();
                }
                return _humanBodyBoneUtil;
            }
        }
    }
    #endregion

    #region 监听动画事件
    public partial class MonoBehaviourBaseY
    {
        /// <summary>
        /// 监听动画事件
        /// </summary>
        public virtual void AniEvent()
        {
            Debug.Log("动画事件：没有参数");
        }

        /// <summary>
        /// 监听动画事件
        /// </summary>
        /// <param name="args"></param>
        public virtual void AniEvent(float args)
        {
            Debug.Log($"动画事件：float：{args}");
        }

        /// <summary>
        /// 监听动画事件
        /// </summary>
        /// <param name="args"></param>
        public virtual void AniEvent(int args)
        {
            Debug.Log($"动画事件：int：{args}");
        }

        /// <summary>
        /// 监听动画事件
        /// </summary>
        /// <param name="args"></param>
        public virtual void AniEvent(string args)
        {
            string arg = string.IsNullOrWhiteSpace(args) ? "" : args;
            Debug.Log($"动画事件：string：{arg}");
        }

        /// <summary>
        /// 监听动画事件
        /// </summary>
        /// <param name="args"></param>
        public virtual void AniEvent(object args)
        {
            string arg = args == null ? "" : args.ToString();
            Debug.Log($"动画事件：object：{arg}");
        }
    }
    #endregion
}