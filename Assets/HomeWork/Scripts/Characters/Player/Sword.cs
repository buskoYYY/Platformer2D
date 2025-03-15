using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public static event Action< Vector2 > EnemyHitEffectDamage;

    [SerializeField] private int _damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyBrain enemy))
        {
            enemy.ApplyDamage(_damage);
        }
    }
}
