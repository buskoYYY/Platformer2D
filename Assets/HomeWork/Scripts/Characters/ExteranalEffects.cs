using UnityEngine;

public class ExteranalEffects : MonoBehaviour
{
    [SerializeField] private Effect _hitEffect;
    [SerializeField] private Effect _deathEffect;
    [SerializeField] private float _waitToDestroyEffect;

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
            GameObject deathEffect = Instantiate(_deathEffect.gameObject, spawnPosition, Quaternion.identity);
            Destroy(deathEffect, _waitToDestroyEffect);
        }
    }
}
