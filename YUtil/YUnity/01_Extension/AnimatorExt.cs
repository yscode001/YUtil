// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-7-15
// ------------------------------

using UnityEngine;

namespace YUnity
{
    public static class AnimatorExt
    {
        public static AnimationClip[] GetCurrentControllerClips(this Animator animator)
        {
            if (animator == null) { return null; }
            return animator.runtimeAnimatorController.animationClips;
        }
    }
}