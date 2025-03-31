using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private int _maxHealth;

    private EnemySound _sound;
    private Health _health;

    public event Action<Vector2> DeathEffectsCreated;
    public event Action<Vector2, Quaternion> HitEffectsCreated;

    private void Awake()
    {
        _health = new Health(_maxHealth);
        _healthBar.Initialize(_health);
        _sound = GetComponent<EnemySound>();
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);

        if (_health.Value <= 0)
        {
            _sound.PlayDeathSound();
            DeathEffectsCreated?.Invoke(transform.position);
            Destroy(gameObject);
        }
        else
        {
            _sound.PlayHitSound();
            HitEffectsCreated?.Invoke(transform.position, transform.rotation);
        }
    }
}
