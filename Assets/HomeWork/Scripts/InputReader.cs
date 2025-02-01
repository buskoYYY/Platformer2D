using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private Vector2 _moveInput;
    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }
    public Vector3 GetMoveInput()
    {
        return _moveInput;
    }
}
