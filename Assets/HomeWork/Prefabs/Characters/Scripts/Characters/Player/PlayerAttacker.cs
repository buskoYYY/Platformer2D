using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private float _timeBtwAttacks;
    private float _lastAttackTime;

    public bool Attack(InputReader input, PlayerAnimator playerAnimator)
    {
        if (Time.time - _lastAttackTime > _timeBtwAttacks && input.GetAttack())
        {
            _lastAttackTime = Time.time;
            playerAnimator.SetAttackAnimation();
            return true;
        }
        return false;
    }
}
