using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerDecision : StateMachineDecision
{
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] private float _radius;
    private EnemyAnimation _animation;

    private void Start()
    {
        _animation = GetComponent<EnemyAnimation>();
    }
    public override bool Decide()
    {
        return AttackState();
    }

    private bool AttackState()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _radius, _playerLayer);

        if (hit != null && hit.GetComponent<Player>())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
