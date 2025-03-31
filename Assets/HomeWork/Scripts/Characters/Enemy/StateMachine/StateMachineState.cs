using System;
using UnityEngine;

[Serializable]

public class StateMachineState
{
    public string ID;
    [SerializeField] private StateMachineAction[] Actions;
    [SerializeField] private StateMachineTransition[] Transitions;

    public void UpadateState(StateMachine stateMachine)
    {
        ExecuteActions();
        ExecuteTransitions(stateMachine);
    }

    private void ExecuteActions()
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act();
        }
    }

    private void ExecuteTransitions(StateMachine enemyBrain)
    {
        if (Transitions == null || Transitions.Length <= 0) return;

        foreach (var transition in Transitions)
        {
            bool isStateChange = transition.Decision.Decide();

            if (isStateChange)
            {
                enemyBrain.ChangeState(transition.TrueState);
            }
            else
            {
                enemyBrain.ChangeState(transition.FalseState);
            }
        }
    }
}
