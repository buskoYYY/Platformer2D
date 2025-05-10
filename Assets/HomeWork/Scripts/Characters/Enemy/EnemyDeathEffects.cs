using UnityEngine;

[RequireComponent (typeof(Enemy))]
public class EnemyDeathEffects : MonoBehaviour
{
    [SerializeField] private Effect _deathEffect;
    [SerializeField] private float _waitToDestroyEffect;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.DeathEffectsTriggered += OnDeathEffect;
    }

    private void OnDisable()
    {
        _enemy.DeathEffectsTriggered -= OnDeathEffect;
    }

    public void OnDeathEffect(Vector2 spawnPosition)
    {
        if (_deathEffect != null)
        {
            GameObject deathEffect = Instantiate(_deathEffect.gameObject, spawnPosition, Quaternion.identity);
            Destroy(deathEffect, _waitToDestroyEffect);
        }
    }
}
