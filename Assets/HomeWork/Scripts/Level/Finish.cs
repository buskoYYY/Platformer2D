using System;
using System.Linq;
using UnityEngine;

public class Finish : MonoBehaviour, IInteractable
{
    public event Action Activated;

    [SerializeField] public Lever[] _doors;
    public void Interact()
    {
        if (_doors.All(i => i.IsActive))
        {
            Activated?.Invoke();
        }
    }
}
