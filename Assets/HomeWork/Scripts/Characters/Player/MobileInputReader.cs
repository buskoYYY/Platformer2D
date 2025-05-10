using UnityEngine;

public class MobileInputReader : MonoBehaviour, IInputReader
{
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private TouchHandler _attackButton;
    [SerializeField] private TouchHandler _speedUpButton;
    [SerializeField] private TouchHandler _interactButton;
    private bool _isInteract;
    private bool _isAttack;
    private bool _isSpeedUp;
    public Vector2 Direction => _joystick.Direction;

    private void OnEnable()
    {
        _attackButton.Down += SetAttack;
        _speedUpButton.Down += SetSpeedUp;
        _interactButton.Down += SetInteract;
    }

    private void OnDisable()
    {
        _attackButton.Down -= SetAttack;
        _speedUpButton.Down -= SetSpeedUp;
        _interactButton.Down -= SetInteract;
    }

    public bool GetAttack()
    {
        return GetBoolAsTrigger(ref _isAttack);     
    }

    public bool GetSpeedUp()
    {
        return GetBoolAsTrigger(ref _isSpeedUp);
    }

    public bool GetIsInteract()
    {
        return GetBoolAsTrigger(ref _isInteract);
    }

    public void SetInteract() => _isInteract = true;

    public void SetAttack() => _isAttack = true;

    public void SetSpeedUp() => _isSpeedUp = true;

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool lockalValue = value;
        value = false;
        return lockalValue;
    }
}
