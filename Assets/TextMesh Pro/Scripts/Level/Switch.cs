using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Switch : Interactable
{
    private Animator _animator;

    public bool IsBlocked { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (IsLock)
        {
            base.Interact();
            return;
        }

        IsBlocked = !IsBlocked;

        if (IsBlocked)
        {
            _animator.SetTrigger(ConstantData.AnimatorParametr.IsOpen);
        }
        else
        {
            _animator.SetTrigger(ConstantData.AnimatorParametr.IsClose);
        }
    }
}
