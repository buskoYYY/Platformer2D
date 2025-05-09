using UnityEngine;

[RequireComponent (typeof(DecisionDetectPlayer))]
public class ActionChase : StateMachineAction
{
    private DecisionDetectPlayer _decisionDetectPlayer;

    private void Awake()
    {
        _decisionDetectPlayer = GetComponent<DecisionDetectPlayer>();
    }

    public override void Act()
    {
        Chase();
    }

    private void Chase()
    {
        if (_decisionDetectPlayer.Player == null)
        {
            return;
        }
        else
        {
            Sound.PlayStepSound();
            Animation.SetMoveAnimation(_decisionDetectPlayer.Player.position, transform.position);
            Mover.Move(_decisionDetectPlayer.Player);
        }
    }
}
