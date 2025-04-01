using System;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public event Action DealDamage;

    public void TriggerAttackEvent()
    {
        DealDamage?.Invoke();
    }
}
