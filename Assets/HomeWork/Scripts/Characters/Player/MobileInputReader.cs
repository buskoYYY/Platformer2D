using UnityEngine;

public class MobileInputReader : MonoBehaviour, IInputReader
{
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private TouchHandler _attackButton;
    [SerializeField] private TouchHandler _interactButton;

    private bool _isInteract;
    private bool _isAttack;
    public Vector2 Direction => _joystick.Direction;

    private void OnEnable()
    {
        _attackButton.Down += SetAttack;
        _interactButton.Down += SetInteract;
    }
    private void OnDisable()
    {
        _attackButton.Down -= SetAttack;
        _interactButton.Down -= SetInteract;
    }

    public bool GetAttack()
    {
        return GetBoolAsTrigger(ref _isAttack);     
    }

    public bool GetIsInteract()
    {
        bool isInteract = _isInteract;
        _isInteract = false;
        return isInteract;
    }
    public void SetInteract() => _isInteract = true;
    public void SetAttack() => _isAttack = true;
    private bool GetBoolAsTrigger(ref bool value)
    {
        bool lockalValue = value;
        value = false;
        return lockalValue;
    }
}
