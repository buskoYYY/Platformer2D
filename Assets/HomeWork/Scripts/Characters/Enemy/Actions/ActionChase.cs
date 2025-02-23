using UnityEngine;

public class ActionChase : StateMachineAction
{
    private EnemyBrain _enemy;
    private EnemyAnimation _animation;
    private Mover _mover;
    private void Awake()
    {
        _enemy = GetComponent<EnemyBrain>();
        _animation = GetComponent<EnemyAnimation>();
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
            _animation.SetMoveAnimation(_enemy.Player.position, transform.position);
            _mover.Move(_enemy.Player);
        }
    }
}
