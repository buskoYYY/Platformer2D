using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHitEffectsPool : HitEffectPool
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.HitEffectsTriggered += CreateHitEffect;
    }

    private void OnDisable()
    {
        _enemy.HitEffectsTriggered -= CreateHitEffect;
    }
}
