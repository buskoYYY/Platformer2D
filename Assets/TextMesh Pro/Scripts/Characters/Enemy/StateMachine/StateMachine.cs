using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private States[] _states;
    [SerializeField] private string _initState;

    private EnemyAnimation _animation;
    private EnemySound _sound;
    private EnemyMover _mover;

    public States CurrentState { get; set; }

    private void Start()
    {
        ChangeState(_initState);
    }

    private void FixedUpdate()
    {
        CurrentState?.UpadateState(this);
    }

    public void Init(EnemyAnimation animation, EnemySound sound, EnemyMover mover)
    {
        _animation = animation;
        _sound = sound;
        _mover = mover;

        CurrentState?.Init(_animation, _sound, _mover);
    }

    public void ChangeState(string newStateID)
    {
        States newState = GetState(newStateID);

        if (newState == null)
            return;

        CurrentState = newState;
        CurrentState.Init(_animation, _sound, _mover);
    }

    private States GetState(string newStateID)
    {
        for (int i = 0; i < _states.Length; i++)
        {
            if (_states[i].ID == newStateID)
            {
                return _states[i];
            }
        }
        return null;
    }
}
