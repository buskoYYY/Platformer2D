using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoveAnimation(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            _animator.SetBool(ConstantData.AnimatorParametr.IsMoving, false);
            return;
        }
        Debug.Log("Moving");
        _animator.SetBool(ConstantData.AnimatorParametr.IsMoving, true);
        _animator.SetFloat(ConstantData.AnimatorParametr.MoveX, direction.x);
        _animator.SetFloat(ConstantData.AnimatorParametr.MoveY, direction.y);
    }
}
