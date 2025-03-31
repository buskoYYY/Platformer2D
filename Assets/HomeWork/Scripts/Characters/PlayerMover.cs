using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _extraSpeed;

    private Rigidbody2D _rigidbody;
    private float _currentSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentSpeed = _normalSpeed;
    }

    public void Move(Vector2 direction, PlayerSounds audio)
    {
        if (direction.sqrMagnitude > 0)
        {
            if (_currentSpeed != _normalSpeed)
            {
                Debug.Log("SppedUp");
                audio.PlayAccelerationSound();
            }
            else
            {
                audio.PlayStepSound();
            }
        }

        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, direction * _currentSpeed,.3f);
    }

    public void CheckAcceleration(bool isSpeedUp)
    {
        if (isSpeedUp)
        {
            _currentSpeed = _extraSpeed;
        }
        else
        {
            _currentSpeed = _normalSpeed;
        }
    }
}
