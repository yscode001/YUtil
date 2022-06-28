using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YUnity
{
    #region 常用缓存属性
    public partial class MonoBehaviourBaseY : MonoBehaviour
    {
        private Transform _transform;
        private RectTransform _rectTransform;
        private GameObject _gameObject;
        private Animator _animator;
        private AudioSource _audioSource;
        private AudioListener _audioListener;
        private CharacterController _characterController;
        private Rigidbody _rigidbody;
        private CanvasGroup _canvasGroup;
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

    #region 播放动画
    public partial class MonoBehaviourBaseY
    {
        public int AniStateError => -9999;
        public string AniStateName { get; private set; } = "";
        public int AniStateValue { get; private set; } = -9999;

        private Coroutine aniC;
        public void PlayAni(string stateName, int stateValue, Action complete)
        {
            if (string.IsNullOrWhiteSpace(stateName)) { return; }
            AnimatorY.Update(0);
            AnimatorY.SetInteger(stateName, stateValue);
            AniStateName = stateName;
            AniStateValue = stateValue;
            if (aniC != null)
            {
                StopCoroutine(aniC);
                aniC = null;
            }
            if (complete == null)
            {
                return;
            }
            aniC = StartCoroutine(DelayPlayAni(AnimatorY, stateName, complete));
        }
        private IEnumerator DelayPlayAni(Animator animator, string stateName, Action complete)
        {
            // 状态机的切换发生在帧的结尾
            yield return new WaitForEndOfFrame();
            var info = animator.GetCurrentAnimatorStateInfo(0);
            if (!info.IsName(stateName))
            {
                yield return null;
            }
            yield return new WaitForSeconds(info.length);
            complete?.Invoke();
        }
    }
    #endregion
}