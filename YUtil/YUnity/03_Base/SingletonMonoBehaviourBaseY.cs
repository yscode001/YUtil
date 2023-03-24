// Author：yaoshuai
// Email：yscode@126.com
// Date：2023-3-24
// ------------------------------
using System;
using UnityEngine;

namespace YUnity
{
    public class SingletonMonoBehaviourBaseY<T> : MonoBehaviourBaseY where T : SingletonMonoBehaviourBaseY<T>
    {
        private SingletonMonoBehaviourBaseY() { }

        public virtual void Init() { }

        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = GameObject.Find("Singleton");
                    if (go == null)
                    {
                        go = GOUtil.CreateEmptyGO(null, "Singleton");
                        DontDestroyOnLoad(go);
                    }
                    _instance = go.GetComponent<T>();
                    if (_instance == null)
                    {
                        _instance = go.AddComponent<T>();
                        _instance.Init();
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = GetComponent<T>();
                _instance.Init();
            }
        }
    }
}