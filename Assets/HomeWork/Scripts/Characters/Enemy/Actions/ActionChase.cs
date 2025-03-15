using UnityEngine;

[RequireComponent (typeof(EnemyBrain), typeof(EnemyAnimation), typeof(Mover))]
[RequireComponent(typeof(EnemySound))]

public class ActionChase : StateMachineAction
{
    private EnemyBrain _enemy;
    private EnemyAnimation _animation;
    private EnemySound _sound;
    private Mover _mover;
    private void Awake()
    {
        _enemy = GetComponent<EnemyBrain>();
        _animation = GetComponent<EnemyAnimation>();
        _sound = GetComponent<EnemySound>();
        _mover = GetComponent<Mover>();
    }
    public override void Act()
    {
        Chase();
    }
    private void Chase()
    {
        if (_enemy.Player == null) return;
        else
        {
            _sound.PlayStepSound();
            _animation.SetMoveAnimation(_enemy.Player.position, transform.position);
            _mover.Move(_enemy.Player);
        }
    }
}
