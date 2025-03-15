using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [Header("Elements")]
    private Rigidbody2D _rigidbody;

    [Header("Settings")]
    [SerializeField] private float _waitTimeToMove;
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _extraSpeed;
    [SerializeField] private float _enemySpeed;
    private float _currentSpeed;
    private float _currentEnemySpeed;

    public bool IsAccelerate {  get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentSpeed = _normalSpeed;
        _currentEnemySpeed = _enemySpeed;
    }
    private void Update()
    {
        SpeedBoost();
    }
    public void Move(Vector2 move,PlayerSounds audio)
    {
        if (move.magnitude > 0 && _currentSpeed == _extraSpeed)
        {
            audio.PlayAccelerationSound();
        }
        else if (move.magnitude > 0)
        {
            audio.PlayStepSound();
        }

        _rigidbody.velocity = move * _currentSpeed;

    }
    public void Move(Transform target)
    {
        Vector2 newPOsition = Vector2.MoveTowards(transform.position, target.position, _currentEnemySpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPOsition);
    }

    public void CanMove()
    {
        StartCoroutine(StopMoveRoutine());
    }
    private void SpeedBoost()
    {
        if (Input.GetKey(KeyCode.B))
        {
            _currentSpeed = _extraSpeed;
            IsAccelerate = true;
        }
        else
        {
            _currentSpeed = _normalSpeed;
            IsAccelerate = false;
        }
    }
    IEnumerator StopMoveRoutine()
    {
        _currentEnemySpeed = 0;
        yield return new WaitForSeconds(_waitTimeToMove);
        _currentEnemySpeed = _enemySpeed;
    }
}
