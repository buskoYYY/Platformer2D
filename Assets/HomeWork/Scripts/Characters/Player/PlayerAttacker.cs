using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private float _timeBtwAttacks;
    private float _lastAttackTime;

    public bool Attack( PlayerAnimator playerAnimator)
    {
        if (Time.time - _lastAttackTime > _timeBtwAttacks )
        {
            Debug.Log("Attackker");
            _lastAttackTime = Time.time;
            playerAnimator.SetAttackAnimation();
            return true;
        }
        return false;
    }
}
