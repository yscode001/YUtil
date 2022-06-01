using UnityEngine;

namespace YFramework
{
    public class AdaptBehaviour : MonoBehaviour
    {
        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void Update() { }
        protected virtual void LateUpdate() { }
        protected virtual void OnDestroy() { }

        public virtual void ApplyLandscapeConfig() { }
        public virtual void ApplyPortraitConfig() { }
    }
}