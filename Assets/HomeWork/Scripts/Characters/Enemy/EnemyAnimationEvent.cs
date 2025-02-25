using System;
using System.Collections;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public static event Action DealDamage;

    [SerializeField] float _eventCooldown = 0.5f;
    private bool _canTriggerEvent = true;

    public void TriggerAttackEvent()
    {
        if (_canTriggerEvent)
        {
            TriggerAnimationEvent();
            _canTriggerEvent = false;
            StartCoroutine(ResetEventCooldown());
        }
    }

    private void TriggerAnimationEvent()
    {
        DealDamage?.Invoke();
    }

    private IEnumerator ResetEventCooldown()
    {
        yield return new WaitForSeconds(_eventCooldown);
        _canTriggerEvent = true;
    }
}
