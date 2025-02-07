using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void SetMoveAnimation(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            _animator.SetBool(ConstantData.AnimatorParametr.IsMoving, false);
            return;
        }
        _animator.SetBool(ConstantData.AnimatorParametr.IsMoving, true);
        _animator.SetFloat(ConstantData.AnimatorParametr.MoveX, direction.x);
        _animator.SetFloat(ConstantData.AnimatorParametr.MoveY, direction.y);
    }
}
