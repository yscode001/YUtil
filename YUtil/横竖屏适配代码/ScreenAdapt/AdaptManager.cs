using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    #region 单例
    public partial class AdaptManager : MonoBehaviour
    {
        public static AdaptManager Instance { get; private set; } = null;
        protected void Awake()
        {
            Instance = this;
            currentIsLandscape = IsLandscape();
        }
        private void OnDestroy()
        {
            Instance = null;
        }
    }
    #endregion
    #region 监控横竖屏切换
    public partial class AdaptManager
    {
        private bool currentIsLandscape = false;
        private readonly List<IAdaptBase> adaptBases = new List<IAdaptBase>();
        private uint frameCount = 0;
        private void Update()
        {
            if (frameCount % 2 == 0)
            {
                bool newState = IsLandscape();
                if (newState != currentIsLandscape)
                {
                    currentIsLandscape = newState;
                    CallFunction();
                }
            }
            frameCount += 1;
        }
        private void CallFunction(IAdaptBase adaptBase = null)
        {
            OnChange(adaptBase);
            if (currentIsLandscape)
            {
                OnLandscape(adaptBase);
            }
            else
            {
                OnPortrait(adaptBase);
            }
        }
    }
    #endregion
    #region 监听
    public partial class AdaptManager
    {
        public void Register(IAdaptBase adaptBase)
        {
            if (adaptBases.Contains(adaptBase))
            {
                return;
            }
            adaptBases.Add(adaptBase);
            CallFunction(adaptBase);
        }
        public void Remove(IAdaptBase adaptBase)
        {
            if (adaptBases.Contains(adaptBase))
            {
                adaptBases.Remove(adaptBase);
            }
        }
    }
    #endregion
    #region 响应横竖屏切换结果
    public partial class AdaptManager
    {
        public static bool IsLandscape()
        {
            return Screen.height < Screen.width;
        }
        private void OnChange(IAdaptBase adaptBase = null)
        {
            if (null != adaptBase)
            {
                adaptBase.OnChange();
                return;
            }
            for (int i = 0; i < adaptBases.Count; i++)
            {
                adaptBases[i].OnChange();
            }
        }
        private void OnLandscape(IAdaptBase adaptBase = null)
        {
            if (null != adaptBase)
            {
                adaptBase.OnLandscape();
                return;
            }
            for (int i = 0; i < adaptBases.Count; i++)
            {
                adaptBases[i].OnLandscape();
            }
        }
        private void OnPortrait(IAdaptBase adaptBase = null)
        {
            if (null != adaptBase)
            {
                adaptBase.OnPortrait();
                return;
            }
            for (int i = 0; i < adaptBases.Count; i++)
            {
                adaptBases[i].OnPortrait();
            }
        }
    }
    #endregion
}