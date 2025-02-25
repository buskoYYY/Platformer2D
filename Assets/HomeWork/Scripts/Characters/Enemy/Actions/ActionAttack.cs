using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent (typeof(AttackPlayerDecision), typeof(EnemyBrain),typeof(EnemyAnimation))]
[RequireComponent(typeof(Mover))]
public class ActionAttack : StateMachineAction
{
    private EnemyBrain _enemy;
    private EnemyAnimation _animation;
    private AttackPlayerDecision _playerDecision;
    private Mover _mover;

    [SerializeField] private int _damage;
    [SerializeField] private float _timeBtwAttacks;
    private float _lastAttackTime;

    private void OnEnable()
    {
       EnemyAnimationEvent.DealDamage += OnDealDamage;
    }
    private void OnDisable()
    {
       EnemyAnimationEvent.DealDamage -= OnDealDamage;
    }
    private void Start()
    {
        _playerDecision = GetComponent<AttackPlayerDecision>();
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
        if (_enemy.Player != null && GetPlayer())
        {
            _animation.SetIdleAnimation(false);
            if (Time.time - _lastAttackTime > _timeBtwAttacks)
            {
                _mover.CanMove();
                _lastAttackTime = Time.time;
                _animation.SetAttackAnimation();
            }
        }
    }
    private void OnDealDamage()
    {
        if (_playerDecision.IsAttackState)
        {
            Player player = GetPlayer();
            player.ApplyDamage(_damage);
        }
    }

    private Player GetPlayer()
    {
        _enemy.Player.TryGetComponent(out Player player);
        return player;
    }
}
