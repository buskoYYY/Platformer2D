using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private float _delay;
    private float _endWaitTime;

    public bool Attack( PlayerAnimator playerAnimator)
    {
        if (Time.time >= _endWaitTime )
        {
            _endWaitTime = Time.time + _delay;
            playerAnimator.SetAttackAnimation();
            return true;
        }
        return false;
    }
}
