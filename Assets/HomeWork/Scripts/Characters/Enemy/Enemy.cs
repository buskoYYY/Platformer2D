using System;
using UnityEngine;

[RequireComponent(typeof(EnemySound), typeof(EnemyAnimation), typeof(EnemyMover))]
[RequireComponent(typeof(StateMachine))]
public class Enemy : Character
{
    private EnemySound _sound;
    private EnemyAnimation _animation;
    private EnemyMover _mover;
    private StateMachine _stateMachine;

    public event Action<Vector2> DeathEffectsCreated;
    public event Action<Vector2, Quaternion> HitEffectsCreated;

    protected override void Awake()
    {
        base.Awake();

        _sound = GetComponent<EnemySound>();
        _animation = GetComponent<EnemyAnimation>();
        _mover = GetComponent<EnemyMover>();

        _stateMachine = GetComponent<StateMachine>();

        _stateMachine.Init(_animation, _sound, _mover);
    }

    protected override void OnTakingDamage()
    {
        _sound.PlayHitSound();
        HitEffectsCreated?.Invoke(transform.position, transform.rotation);
    }

    protected override void OnDied()
    {
        _sound.PlayDeathSound();
        DeathEffectsCreated?.Invoke(transform.position);
        Destroy(gameObject);
    }
}
