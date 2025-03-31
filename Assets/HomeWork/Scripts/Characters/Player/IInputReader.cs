using UnityEngine;

public interface IInputReader 
{
    public Vector2 Direction { get; }
    public bool GetAttack();
    public bool GetSpeedUp();
    public bool GetIsInteract();
}
