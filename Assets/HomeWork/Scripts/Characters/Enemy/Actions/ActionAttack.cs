using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DecisionAttackPlayer), typeof(DecisionDetectPlayer), typeof(EnemyAnimation))]
[RequireComponent(typeof(EnemyMover), typeof(EnemySound))]
public class ActionAttack : StateMachineAction
{
    [SerializeField] EnemyAnimationEvent _animationEvent;
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;
    [SerializeField] float _eventCooldown = 0.5f;

    private EnemyAnimation _animation;
    private EnemySound _sound;
    private DecisionDetectPlayer _decisionDetectPlayer;
    private DecisionAttackPlayer _decisionAttackPlayer;
    private EnemyMover _mover;
    private float _endWaitTime;
    private bool _canTriggerEvent = true;

    private void Awake()
    {
        _decisionAttackPlayer = GetComponent<DecisionAttackPlayer>();
        _decisionDetectPlayer = GetComponent<DecisionDetectPlayer>();
        _animation = GetComponent<EnemyAnimation>();
        _mover = GetComponent<EnemyMover>();
        _sound = GetComponent<EnemySound>();
    }

    private void OnEnable()
    {
        _animationEvent.DealDamage += OnDealDamage;
    }

    private void OnDisable()
    {
        _animationEvent.DealDamage -= OnDealDamage;
    }

    public override void Act()
    {
        Attack();
    }

    private void Attack()
    {
        if (_decisionDetectPlayer.Player != null)
        {
            _animation.SetIdleAnimation(false);

            if (Time.time >= _endWaitTime)
            {
                _mover.ToggleMovementState();
                _sound.PlayAttackSound();
                _endWaitTime = Time.time + _delay;
                _animation.SetAttackAnimation();
            }
        }
    }

    private void OnDealDamage()
    {
        if (_decisionAttackPlayer.IsAttackState)
        {
            if (_decisionDetectPlayer.Player.TryGetComponent(out Player player))
            {
                if (_canTriggerEvent)
                {
                    player.ApplyDamage(_damage);
                    _canTriggerEvent = false;
                    StartCoroutine(ResetEventCooldown());
                }
            }
        }
    }

    private IEnumerator ResetEventCooldown()
    {
        yield return new WaitForSeconds(_eventCooldown);
        _canTriggerEvent = true;
    }
}
