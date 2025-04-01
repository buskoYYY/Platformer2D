using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DecisionAttackPlayer), typeof(DecisionDetectPlayer))]
public class ActionAttack : StateMachineAction
{
    [SerializeField] EnemyAnimationEvent _animationEvent;
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;
    [SerializeField] float _eventCooldown = 0.5f;

    private DecisionDetectPlayer _decisionDetectPlayer;
    private DecisionAttackPlayer _decisionAttackPlayer;

    private float _endWaitTime;
    private bool _canTriggerEvent = true;

    private void Awake()
    {
        _decisionAttackPlayer = GetComponent<DecisionAttackPlayer>();
        _decisionDetectPlayer = GetComponent<DecisionDetectPlayer>();
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
            Animation.SetIdleAnimation(false);

            if (Time.time >= _endWaitTime)
            {
                Mover.ToggleMovementState();
                Sound.PlayAttackSound();
                _endWaitTime = Time.time + _delay;
                Animation.SetAttackAnimation();
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
