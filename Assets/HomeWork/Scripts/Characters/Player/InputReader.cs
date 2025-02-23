using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [Header("Settings")]
    private Vector2 _moveInput;
    private bool _isInteract;
    private bool _isAttack;

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
    public Vector3 GetMoveInput()
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
        if (EventSystem.current.IsPointerOverGameObject()) return;
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
