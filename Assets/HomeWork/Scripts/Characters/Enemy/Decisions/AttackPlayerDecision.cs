using UnityEngine;

public class AttackPlayerDecision : StateMachineDecision
{
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] private float _radius;
    public bool IsAttackState {  get; private set; }
    public override bool Decide()
    {
        return AttackState();
    }

    private bool AttackState()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _radius, _playerLayer);

        if (hit != null && hit.GetComponent<Player>())
        {
            IsAttackState = true;
            return true;
        }
        else
        {
            IsAttackState = false;
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
