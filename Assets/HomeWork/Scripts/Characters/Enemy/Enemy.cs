using UnityEngine;

[RequireComponent(typeof(EnemyVision), typeof(Mover), typeof(EnemyAnimation))]
[RequireComponent(typeof(PlayerAttacker))]
public class Enemy : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private WayPoint[] _wayPoints;
    private Health _health;
    private EnemyVision _enemyVision;
    private Mover _mover;
    private Transform _target;
    private EnemyAnimation _animation;

    [Header("Settings")]
    [SerializeField] private float _waitTime;
    [SerializeField] private int _maxHealth;
    private int _wayPointIndex;
    private float _maxSqrDistance = 0.01f;
    private float _endWaitTime;
    private bool _isWaiting;

    private void Awake()
    {
        _health = new Health(_maxHealth);
    }
    private void Start()
    {
        _mover = GetComponent<Mover>();
        _enemyVision = GetComponent<EnemyVision>();
        _animation = GetComponent<EnemyAnimation>();
        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        if (TimeManager.IsPaused) return;

        if (_enemyVision.TrySeeTarget(out Transform target))
        {           
                _animation.SetMoveAnimation(target.position, transform.position);
                _mover.Move(target);
        }
        else
        {
            if (_isWaiting == false)
            {
                _animation.SetMoveAnimation(_target.position, transform.position);
                _mover.Move(_target);
            }

            if (IsTargetReached() && _isWaiting == false)
            {
                _isWaiting = true;
                _endWaitTime = Time.time + _waitTime;
                _animation.SetIdleAnimation(false);
            }

            if (_isWaiting && _endWaitTime <= Time.time)
            {
                ChangeTarget();
                _isWaiting = false;
                _animation.SetIdleAnimation(true);
            }
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
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);

        if (_health.Value <= 0)
            Destroy(gameObject);
    }
}
