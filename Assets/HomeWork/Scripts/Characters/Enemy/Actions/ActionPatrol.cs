using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPatrol : StateMachineAction
{
    [SerializeField] private WayPoint[] _wayPoints;
    private Transform _target;
    private Mover _mover;

    [SerializeField] private float _speed;
    [SerializeField] private float _waitTime;
    private int _wayPointIndex;
    private float _maxSqrDistance = 0.01f;
    private float _endWaitTime;
    private bool _isWaiting;

    private void Start()
    {
        _mover = GetComponent<Mover>();
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
            //_animation.SetMoveAnimation(_target.position, transform.position);
            _mover.Move(_target);
        }

        if (IsTargetReached() && _isWaiting == false)
        {
            _isWaiting = true;
            _endWaitTime = Time.time + _waitTime;
           // _animation.SetIdleAnimation(false);
        }

        if (_isWaiting && _endWaitTime <= Time.time)
        {
            ChangeTarget();
            _isWaiting = false;
           // _animation.SetIdleAnimation(true);
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
