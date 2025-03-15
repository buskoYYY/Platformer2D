using System;

[Serializable]

public class StateMachineState
{
    public string ID;
    public StateMachineAction[] Actions;
    public StateMachineTransition[] Transitions;

    public void UpadateState(EnemyBrain enemyBrain)
    {
        ExecuteActions();
        ExecuteTransitions(enemyBrain);
    }

    private void ExecuteActions()
    {
        for(int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act();
        }
    }

    private void ExecuteTransitions(EnemyBrain enemyBrain)
    {
        if (Transitions == null || Transitions.Length <= 0) return;

        for (int i = 0;i < Transitions.Length;i++)
        {
            bool value = Transitions[i].Decision.Decide();
            if(value)
            {
                enemyBrain.ChangeState(Transitions[i].TrueState);
            }
            else
            {
                enemyBrain.ChangeState(Transitions[i].FalseState);
            }
        }
    }
}
