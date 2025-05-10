using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, IInputReader
{
    private Vector2 _moveInput;
    private bool _isInteract;
    private bool _isAttack;
    private bool _isSpeedUp;

    public Vector2 Direction => _moveInput;

    private void Update()
    {
        if(TimeManager.IsPaused)
        {
            return;
        }
    }

    public bool GetAttack() => GetBoolAsTrigger(ref _isAttack);
    
    public bool GetSpeedUp() => GetBoolAsTrigger(ref _isSpeedUp);

    public bool GetIsInteract() => GetBoolAsTrigger(ref _isInteract);

    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>().normalized;
    }

    private void OnFire(InputValue value)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (value.isPressed)
        {
            _isAttack = true;
        }
    }

    private void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            _isInteract = true;
        }
    }

    private void OnSpeedUp(InputValue value)
    {
        if (value.isPressed)
        {
            _isSpeedUp = true;
        }
    }

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool lockalValue = value;
        value = false;
        return lockalValue;
    }
}
