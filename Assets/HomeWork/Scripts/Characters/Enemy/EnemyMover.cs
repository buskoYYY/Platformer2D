using System.Collections;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _waitTimeToMove;

    private float _currentEnemySpeed;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentEnemySpeed = _enemySpeed;
    }

    public void Move(Transform target)
    {
        Vector2 newPOsition = Vector2.MoveTowards(transform.position, target.position, _currentEnemySpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPOsition);
    }

    public void ToggleMovementState()
    {
        StartCoroutine(ToggleMovementStateRoutine());
    }

    private IEnumerator ToggleMovementStateRoutine()
    {
        _currentEnemySpeed = 0;
        yield return new WaitForSeconds(_waitTimeToMove);
        _currentEnemySpeed = _enemySpeed;
    }
}
