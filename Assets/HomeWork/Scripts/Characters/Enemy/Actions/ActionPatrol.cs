using UnityEngine;

public class ActionPatrol : StateMachineAction
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _waitTime;

    private Transform _target;
    private int _wayPointIndex;
    private float _maxSqrDistance = 0.1f;
    private float _endWaitTime;
    private bool _isWaiting;

    private void Start()
    {
        _target = _wayPoints[_wayPointIndex].transform;
    }

    public override void Act()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (_isWaiting == false)
        {
            Animation.SetMoveAnimation(_target.position, transform.position);
            Mover.Move(_target);
            Sound.PlayStepSound();
        }

        if (IsTargetReached() && _isWaiting == false)
        {
            _isWaiting = true;
            _endWaitTime = Time.time + _waitTime;
           Animation.SetIdleAnimation(false);
        }

        if (_isWaiting && _endWaitTime <= Time.time)
        {
            ChangeTarget();
            _isWaiting = false;
           Animation.SetIdleAnimation(true);
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
}
