using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private string _initState;
    [SerializeField] private StateMachineState[] _states;
    public StateMachineState CurrentState { get; set; }

    private void Start()
    {
        ChangeState(_initState);
    }
    private void Update()
    {
        CurrentState?.UpadateState(this);
    }

    public void ChangeState(string newStateID)
    {
        StateMachineState newState = GetState(newStateID);
        if (newState == null) return;
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
