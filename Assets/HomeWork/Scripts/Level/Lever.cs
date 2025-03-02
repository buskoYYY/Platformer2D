using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Lever : MonoBehaviour, IInteractable
{
    private Animator _animator;

    public bool IsActive { get; private set; }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        IsActive =!IsActive;

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
