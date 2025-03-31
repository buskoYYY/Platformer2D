using UnityEngine;

[RequireComponent(typeof(EnemySound))]
public class StateMachine : MonoBehaviour
{
    [SerializeField] private StateMachineState[] _states;
    [SerializeField] private string _initState;

    public StateMachineState CurrentState { get; set; }

    private void Start()
    {
        ChangeState(_initState);
    }

    private void FixedUpdate()
    {
        CurrentState?.UpadateState(this);
    }

    public void ChangeState(string newStateID)
    {
        StateMachineState newState = GetState(newStateID);

        if (newState == null)
            return;

        CurrentState = newState;
    }

    private StateMachineState GetState(string newStateID)
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
