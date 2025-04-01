using UnityEngine;

public class DecisionAttackPlayer : StateMachineDecision
{
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] private float _radius;

    public bool IsAttackState {  get; private set; }

    public override bool Decide()
    {
        return SetAttackState();
    }

    private bool SetAttackState()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _radius, _playerLayer);

        IsAttackState = hit != null && hit.GetComponent<Player>();
        return IsAttackState;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
