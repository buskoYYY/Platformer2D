using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotion : MonoBehaviour
{
    [Header("Elements")]
    private Rigidbody2D _rigidbody;


    [Header("Settings")]
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _extraSpeed;
    private float _currentSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentSpeed = _normalSpeed;
    }
    private void Update()
    {
        SpeedBoost();
    }
     public void Move(Vector2 move)
    {
        _rigidbody.velocity = move * _currentSpeed;
    }
    private void SpeedBoost()
    {
        if (Input.GetKey(KeyCode.B))
        {
            _currentSpeed = _extraSpeed;
        }
        else
        {
            _currentSpeed = _normalSpeed;
        }
    }
}
