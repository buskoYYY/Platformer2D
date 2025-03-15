using System;
using System.Linq;
using UnityEngine;

public class Finish : Interactable
{
    public event Action Activated;

    [SerializeField] private Switch[] _switchers;
    [SerializeField] private Sprite _swintchIcon;

    public override void Interact()
    {
        if (IsLock)
        {
            base.Interact();
            return;
        }

        if (_switchers.All(i => i.IsActive))
        {
            Activated?.Invoke();
        }
        else
        {
            ShowMessage(_switchers.Count(i => i.IsActive), _switchers.Length, _swintchIcon);
        }
    }
}
