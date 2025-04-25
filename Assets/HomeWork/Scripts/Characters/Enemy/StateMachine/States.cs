using System;
using UnityEngine;

[Serializable]

public class States
{
    [SerializeField] private StateMachineAction[] Actions;
    [SerializeField] private StateMachineTransition[] Transitions;
    [SerializeField] private string _ID;

    public string ID => _ID;

    public void UpadateState(StateMachine stateMachine)
    {
        ExecuteActions();
        ExecuteTransitions(stateMachine);
    }

    public void Init(EnemyAnimation animation, EnemySound sound, EnemyMover mover)
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Init(animation, sound, mover);
        }
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
