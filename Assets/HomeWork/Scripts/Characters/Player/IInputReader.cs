using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputReader 
{
    public Vector2 Direction { get; }
    public bool GetAttack();
    public bool GetIsInteract();
}
