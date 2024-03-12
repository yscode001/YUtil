using System;
using System.Collections;
using UnityEngine;

namespace YUnity
{
    /// <summary>
    /// 音频管理器
    /// </summary>
    public partial class AudioMag : MonoBehaviourBaseY
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
            bgAudio = bg.AddComponent<AudioSource>();
            bgAudio.volume = 0.1f;

            GameObject normal = new GameObject
            {
                name = "NormalAudioGO"
            };
            normal.transform.parent = transform;
            normalAudio = normal.AddComponent<AudioSource>();
            normalAudio.volume = 0.2f;
        }

        private void OnDestroy()
        {
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
        /// <param name="bgMusic">背景音效</param>
        /// <param name="isLoop">是否循环</param>
        public void PlayBGMusic(AudioClip bgMusic, bool isLoop)
        {
            if (bgMusic == null)
            {
                return;
            }
            if (bgAudio.clip == null || bgAudio.clip.name != bgMusic.name || !bgAudio.isPlaying)
            {
                bgAudio.clip = bgMusic;
                bgAudio.loop = isLoop;
                bgAudio.Play();
                if (preBGClip == null)
                {
                    preBGClip = bgMusic;
                }
                else if (preBGClip != bgMusic)
                {
                    Resources.UnloadAsset(preBGClip);
                    preBGClip = bgMusic;
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
        /// <param name="audioClip">音效</param>
        public void PlayAudioClip(AudioClip audioClip)
        {
            if (audioClip == null)
            {
                return;
            }
            if (normalAudio.clip != null && normalAudio.clip.name == audioClip.name && normalAudio.isPlaying)
            {
                return;
            }
            normalAudio.clip = audioClip;
            normalAudio.loop = false;
            normalAudio.Play();
        }

        public void PlayAudioClipOneShoot(AudioClip audioClip)
        {
            if (audioClip == null)
            {
                return;
            }
            normalAudio.PlayOneShot(audioClip);
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
}