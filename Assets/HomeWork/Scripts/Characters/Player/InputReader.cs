using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, IInputReader
{
    [Header("Settings")]
    private Vector2 _moveInput;
    private bool _isInteract;
    private bool _isAttack;

    public Vector2 Direction => _moveInput;

    private void Update()
    {
        if(TimeManager.IsPaused) return;

        if(Input.GetKeyDown(KeyCode.F))
        {
            _isInteract = true;
        }
    }
    public bool GetIsInteract()
    {
        bool isInteract = _isInteract;
        _isInteract = false;
        return isInteract;
    }
    public Vector2 GetMoveInput()
    {
        return _moveInput;
    }
    public bool GetAttack() => GetBoolAsTrigger(ref _isAttack);

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>().normalized;
    }
    
    void OnFire(InputValue value)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        if (value.isPressed)
        {
            _isAttack = true;
        }
    }
    private bool GetBoolAsTrigger(ref bool value)
    {
        bool lockalValue = value;
        value = false;
        return lockalValue;
    }
}

