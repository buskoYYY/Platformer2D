using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Switch : Interactable
{
    private Animator _animator;

    public bool IsActive { get; private set; }


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

        IsActive = !IsActive;

        if (IsActive)
        {
            _animator.SetTrigger(ConstantData.AnimatorParametr.IsOpen);
        }
        else
        {
            _animator.SetTrigger(ConstantData.AnimatorParametr.IsClose);
        }
    }
}
