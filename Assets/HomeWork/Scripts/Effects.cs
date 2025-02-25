using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private float _waitToDestroyEffect;

    private void OnEnable()
    {
        Player.PlayerHitEffect += OnHitEffect;
        Player.StartDeathEffects += OnDeathEffect;
        EnemyBrain.EnemyHitEffect += OnHitEffect;
        EnemyBrain.StartDeathEffects += OnDeathEffect;
    }
    private void OnDisable()
    {
        Player.PlayerHitEffect -= OnHitEffect;
        Player.StartDeathEffects -= OnDeathEffect;
        EnemyBrain.EnemyHitEffect -= OnHitEffect;
        EnemyBrain.StartDeathEffects += OnDeathEffect;
    }
    public void OnHitEffect(Vector2 spawnPosition, Quaternion rotation)
    {
        if (_hitEffect != null)
        {
            Instantiate(_hitEffect, spawnPosition, rotation);
        }
    }
    public void OnDeathEffect(Vector2 spawnPosition)
    {
        if (_deathEffect != null)
        {
            GameObject deathEffect = Instantiate(_deathEffect, spawnPosition, Quaternion.identity);
            Destroy(deathEffect, _waitToDestroyEffect);
        }
    }
}
