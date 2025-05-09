using UnityEngine;

public class HitEffectPool : MonoBehaviour
{
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpand;
    [SerializeField] private Effect _hitEffect;

    private ObjectPool<Effect> _hitEffectsPool;

    private void Start()
    {
        _hitEffectsPool = new ObjectPool<Effect>(_hitEffect, _poolCount,transform);
        _hitEffectsPool.autoExpand = _autoExpand; 
    }

    public void CreateHitEffect(Vector2 spawnPosition, Quaternion spawnRotation)
    {
        var effect = _hitEffectsPool.GetFreeElement();
        effect.transform.position = spawnPosition;
        effect.transform.rotation = spawnRotation;
    }
}
