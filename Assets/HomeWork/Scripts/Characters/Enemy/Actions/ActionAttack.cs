using UnityEngine;

public class ActionAttack : StateMachineAction
{
    private EnemyBrain _enemy;
    private EnemyAnimation _animation;
    private Mover _mover;

    [SerializeField] private int _damage;
    [SerializeField] private float _timeBtwAttacks;
    private float _lastAttackTime;


    private void Start()
    {
        _enemy = GetComponent<EnemyBrain>();
        _animation = GetComponent<EnemyAnimation>();
        _mover = GetComponent<Mover>();
    }
    public override void Act()
    {
        Attack();
    }
    private void Attack()
    {
        if (_enemy.Player != null && _enemy.Player.TryGetComponent(out Player player))
        {
            _animation.SetIdleAnimation(false);
            if (Time.time - _lastAttackTime > _timeBtwAttacks)
            {
                _mover.CanMove();
                _lastAttackTime = Time.time;
                _animation.SetAttackAnimation();
                player.ApplyDamage(_damage);
            }
        }
    }
}
