using UnityEngine;
public static class ConstantData
{
    public static class AnimatorParametr
    {
        public static readonly int MoveX = Animator.StringToHash(nameof(MoveX));
        public static readonly int MoveY = Animator.StringToHash(nameof(MoveY));
        public static readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));
        public static readonly int IsOpen = Animator.StringToHash(nameof(IsOpen));
        public static readonly int IsClose = Animator.StringToHash(nameof(IsClose));
        public static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
    }
}
