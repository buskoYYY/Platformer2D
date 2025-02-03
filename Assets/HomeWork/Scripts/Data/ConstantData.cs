using UnityEngine;

public static class ConstantData
{
    public static class AnimatorParametr
    {
        public static readonly int IsOpen = Animator.StringToHash(nameof(IsOpen));
        public static readonly int IsClose = Animator.StringToHash(nameof(IsClose));
    }
}
