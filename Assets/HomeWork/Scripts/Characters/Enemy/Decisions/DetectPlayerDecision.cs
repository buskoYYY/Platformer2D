using UnityEngine;

public class DetectPlayerDecision : StateMachineDecision
{
    [Header("Elements")]
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] LayerMask _playerSwordLayer;
    [SerializeField] LayerMask _backGroundLayer;
    private EnemyBrain _enemy;

    [Header("Settings")]
    [SerializeField] private float _seeAreaSize;

    private void Start()
    {
        _enemy = GetComponent<EnemyBrain>();
    }
    public override bool Decide()
    {
        return TrySeeTarget();
    }

    private bool TrySeeTarget()
    {
        _enemy.Player = null;
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _seeAreaSize, _playerLayer);

        if (hit != null)
        {
            Vector2 direction = (hit.transform.position - transform.position).normalized;
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, _seeAreaSize, ~(1 << gameObject.layer | _playerSwordLayer | _backGroundLayer));
            if (hit2D.collider != null)
            {

                if (hit2D.collider == hit)
                {
                    _enemy.Player = hit2D.transform;
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _seeAreaSize);
    }
}
