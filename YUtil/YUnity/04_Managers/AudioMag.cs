using System;
using System.Collections;
using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 音频管理器
    /// </summary>
    public partial class AudioMag : BaseMonoBehaviour
    {
        private AudioMag() { }
        public static AudioMag Instance { get; private set; } = null;

        /// <summary>
        /// 背景音效
        /// </summary>
        private AudioSource bgAudio;

        /// <summary>
        /// 普通音效
        /// </summary>
        private AudioSource normalAudio;

        internal void Init()
        {
            this.Log("初始化(YFramework)：音频管理器(AudioMag)");
            Instance = this;
            EnableNormalAudioClip = true;
            CreateObjectAfterInit();
        }

        private void CreateObjectAfterInit()
        {
            GameObject bg = new GameObject
            {
                name = "BGAudioGO"
            };
            bg.transform.parent = transform;
            bgAudio = bg.GetOrAddComponent<AudioSource>();
            bgAudio.volume = 0.1f;

            GameObject normal = new GameObject
            {
                name = "NormalAudioGO"
            };
            normal.transform.parent = transform;
            normalAudio = normal.GetOrAddComponent<AudioSource>();
            normalAudio.volume = 0.2f;
        }

        private void OnDestroy()
        {
            this.Log("销毁(YFramework)：音频管理器(AudioMag)");
            Instance = null;
        }
    }

    #region 背景音效
    public partial class AudioMag
    {
        private AudioClip preBGClip;

        /// <summary>
        /// 播放背景音效
        /// </summary>
        /// <param name="fullFilePath">音效完整路径</param>
        /// <param name="isLoop">是否循环</param>
        public void PlayBGMusic(string fullFilePath, bool isLoop)
        {
            if (bgAudio == null)
            {
                return;
            }
            AudioClip ac = ResourceMag.Instance.LoadAudio(fullFilePath, false);
            if (ac == null)
            {
                return;
            }
            if (bgAudio.clip == null || bgAudio.clip.name != ac.name || !bgAudio.isPlaying)
            {
                bgAudio.clip = ac;
                bgAudio.loop = isLoop;
                bgAudio.Play();
                if (preBGClip == null)
                {
                    preBGClip = ac;
                }
                else if (preBGClip != ac)
                {
                    Resources.UnloadAsset(preBGClip);
                    preBGClip = ac;
                }
            }
        }

        public void PauseBGMusic()
        {
            bgAudio.Pause();
        }

        public void StopBGMusic()
        {
            bgAudio.Stop();
        }

        public bool IsPlayingBGMusic => bgAudio.isPlaying;
    }
    #endregion

    #region 普通音效
    public partial class AudioMag
    {
        /// <summary>
        /// 普通音效是否可用(方便与业务系统进行解耦)
        /// </summary>
        public bool EnableNormalAudioClip { get; private set; }

        public void SetupEnableNormalAudioClip(bool enable)
        {
            EnableNormalAudioClip = enable;
            if (!enable)
            {
                StopAudioClip();
            }
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="fullFilePath">音效完整路径</param>
        public void PlayAudioClip(string fullFilePath)
        {
            if (!EnableNormalAudioClip || normalAudio == null)
            {
                return;
            }
            AudioClip ac = ResourceMag.Instance.LoadAudio(fullFilePath, true);
            if (ac == null)
            {
                return;
            }
            if (normalAudio.clip != null && normalAudio.clip.name == ac.name && normalAudio.isPlaying)
            {
                return;
            }
            normalAudio.clip = ac;
            normalAudio.loop = false;
            normalAudio.Play();
        }

        public void PauseAudioClip()
        {
            normalAudio.Pause();
        }

        public void StopAudioClip()
        {
            normalAudio.Stop();
        }
    }
    #endregion

    #region 设置音量
    public partial class AudioMag
    {
        /// <summary>
        /// 设置背景音效音量
        /// </summary>
        /// <param name="volume">音量范围：0-1</param>
        public void SetBGAudioSourceVolume(float volume)
        {
            float vle = Mathf.Clamp(volume, 0, 1);
            bgAudio.volume = vle;
        }

        /// <summary>
        /// 设置普通音效音量
        /// </summary>
        /// <param name="volume">音量范围：0-1</param>
        public void SetNormalAudioSourceVolume(float volume)
        {
            float vle = Mathf.Clamp(volume, 0, 1);
            normalAudio.volume = vle;
        }
    }
    #endregion

    #region 播放传入的AudioSource
    public partial class AudioMag
    {
        private IEnumerator DelayPlayAudio(float seconds, Action playAction)
        {
            yield return new WaitForSeconds(seconds);
            playAction?.Invoke();
        }

        public void PlayEntityAudio(AudioSource audioSrc, string fullFilePath, bool loop = false, int delayMilliseconds = 0)
        {
            if (string.IsNullOrWhiteSpace(fullFilePath) || audioSrc == null || !EnableNormalAudioClip) { return; }
            AudioClip ac = ResourceMag.Instance.LoadAudio(fullFilePath, true);
            if (ac == null) { return; }
            void PlayAudio()
            {
                if (audioSrc.clip != null && audioSrc.clip.name == ac.name && audioSrc.isPlaying) { return; }
                audioSrc.clip = ac;
                audioSrc.loop = loop;
                audioSrc.Play();
            }
            if (delayMilliseconds <= 0)
            {
                PlayAudio();
            }
            else
            {
                StartCoroutine(DelayPlayAudio(delayMilliseconds * 1.0f / 1000, PlayAudio));
            }
        }
    }
    #endregion
}