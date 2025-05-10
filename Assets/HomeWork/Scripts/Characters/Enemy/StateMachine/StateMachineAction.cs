using UnityEngine;

public abstract class StateMachineAction : MonoBehaviour
{
    protected EnemyAnimation Animation => _animation;
    private EnemyAnimation _animation;

    protected EnemySound Sound => _sound;
    private EnemySound _sound;

    protected EnemyMover Mover => _mover;
    private EnemyMover _mover;

    public virtual void Init(EnemyAnimation animation, EnemySound sound, EnemyMover mover)
    {
        _animation = animation;
        _sound = sound;
        _mover = mover;
    }

    public abstract void Act();
}
