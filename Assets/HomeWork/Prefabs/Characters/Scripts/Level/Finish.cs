using System;
using System.Linq;
using UnityEngine;

public class Finish : Interactable
{
    public event Action Activated;

    [SerializeField] private Switch[] _switchers;

    public override void Interact()
    {
        if (_isLock) return;

        if (_switchers.All(i => i.IsActive))
        {
            Activated?.Invoke();
        }
    }
}
