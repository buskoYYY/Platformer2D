using UnityEngine;

[RequireComponent (typeof(DecisionDetectPlayer), typeof(EnemyAnimation), typeof(EnemyMover))]
[RequireComponent(typeof(EnemySound))]

public class ActionChase : StateMachineAction
{
    private EnemyAnimation _animation;
    private EnemySound _sound;
    private EnemyMover _mover;
    private DecisionDetectPlayer _decisionDetectPlayer;

    private void Awake()
    {
        _animation = GetComponent<EnemyAnimation>();
        _sound = GetComponent<EnemySound>();
        _mover = GetComponent<EnemyMover>();
        _decisionDetectPlayer = GetComponent<DecisionDetectPlayer>();
    }

    public override void Act()
    {
        Chase();
    }

    private void Chase()
    {
        if (_decisionDetectPlayer.Player == null)
        {
            return;
        }
        else
        {
            _sound.PlayStepSound();
            _animation.SetMoveAnimation(_decisionDetectPlayer.Player.position, transform.position);
            _mover.Move(_decisionDetectPlayer.Player);
        }
    }
}
