using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetMoveAnimation(Vector2 targetPos, Vector2 enemyPos)
    {
        Vector2 direction = targetPos - enemyPos;

        if (direction == Vector2.zero)
        {
            SetIdleAnimation(false);
            return;
        }

        SetIdleAnimation(true);
        _animator.SetFloat(ConstantData.AnimatorParametr.MoveX, direction.x);
        _animator.SetFloat(ConstantData.AnimatorParametr.MoveY, direction.y);
    }

    public void SetIdleAnimation(bool state)
    {
        _animator.SetBool(ConstantData.AnimatorParametr.IsMoving, state);
    }

    public void SetAttackAnimation()
    {
        _animator.SetTrigger(ConstantData.AnimatorParametr.Attack);
    }
}
