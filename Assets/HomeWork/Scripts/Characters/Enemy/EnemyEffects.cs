using UnityEngine;

[RequireComponent (typeof(Enemy))]
public class EnemyEffects : ExteranalEffects
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.HitEffectsCreated += OnHitEffect;
        _enemy.DeathEffectsCreated += OnDeathEffect;
    }
    private void OnDisable()
    {
        _enemy.HitEffectsCreated += OnHitEffect;
        _enemy.DeathEffectsCreated += OnDeathEffect;
    }
}
