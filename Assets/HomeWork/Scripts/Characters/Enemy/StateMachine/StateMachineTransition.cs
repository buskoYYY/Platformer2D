using System;

[Serializable]
public class StateMachineTransition 
{
    public StateMachineDecision Decision;
    public string TrueState;
    public string FalseState;
}
