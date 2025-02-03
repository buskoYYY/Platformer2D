using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] LayerMask _playerLayer;
    private Rigidbody2D _rigidbody;
    private Transform _target;


    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _seeAreaSize;
    private int _wayPointIndex;
    private float _maxSqrDistance = 0.01f;
    private float _endWaitTime;
    private bool _isTurningRight;
    private bool _isWaiting;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _seeAreaSize, _playerLayer);
        
            Debug.Log(hit);
        
        
        if (_isWaiting == false)
            Move();

        if (IsTargetReached() && _isWaiting == false)
        {
            _isWaiting = true;
            _endWaitTime = Time.time + _waitTime;
        }

        if (_isWaiting && _endWaitTime <= Time.time)

        {
            ChangeTarget();
            _isWaiting = false;
        }
    }

    private bool IsTargetReached()
    {
        float sqrDistance = (transform.position - _target.position).sqrMagnitude;
        return sqrDistance < _maxSqrDistance;
    }

    private void ChangeTarget()
    {
        _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;

        if ((transform.position.x < _target.position.x && _isTurningRight == false)
           || (transform.position.x > _target.position.x && _isTurningRight))
        {
            Flip();
        }
    }

    private void Move()
    {
        Vector2 newPOsition = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPOsition);
    }

    private void Flip()
    {
        _isTurningRight = !_isTurningRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _seeAreaSize);
    }
}
