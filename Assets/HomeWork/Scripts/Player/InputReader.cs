using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private Vector2 _moveInput;
    private bool _isInteract;

    private void Update()
    {
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

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>().normalized;
    }
}
