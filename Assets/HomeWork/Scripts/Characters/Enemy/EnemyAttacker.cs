
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] LayerMask _playerLayer;
    ////
    private EnemyAnimation _enemyAnimation;
    ////
    [Header("Settings")]
    [SerializeField] private float _offsetx;
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;
    [SerializeField] private float _attackCooldown;

    public float Radius { get { return _radius; } }

    
    private void Start()
    {
        _enemyAnimation = GetComponent<EnemyAnimation>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _radius, _playerLayer);
        
        if (hit != null && hit.TryGetComponent(out Player player))
        {
            _enemyAnimation.SetAttackAnimation(true);
            player.ApplyDamage(_damage);
            
        }

    }

    private void AttackProcess()
    {
        
    }
}
