using UnityEngine;

public class EffectsPool : MonoBehaviour
{
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpand;
    [SerializeField] private Effect _hitEffect;
    [SerializeField] private Effect _puffEffect;

    private ObjectPool<Effect> _hitEffectsPool;
    private ObjectPool<Effect> _puffEffectsPool;

    private void Start()
    {
        _hitEffectsPool = new ObjectPool<Effect>(_hitEffect, _poolCount, transform);

        if (_puffEffect != null)
        {
            _puffEffectsPool = new ObjectPool<Effect>(_puffEffect, _poolCount, transform);
            _puffEffectsPool.autoExpand = _autoExpand;
        }

        _hitEffectsPool.autoExpand = _autoExpand;
    }

    public void CreateHitEffect(Vector2 spawnPosition, Quaternion spawnRotation)
    {
        CreateEffect(_hitEffectsPool, spawnPosition, spawnRotation);
    }

    public void CreatePuffEffect(Vector2 spawnPosition, Quaternion spawnRotation)
    {
        CreateEffect(_puffEffectsPool, spawnPosition, spawnRotation);
    }

    private void CreateEffect(ObjectPool<Effect> effects, Vector2 spawnPosition, Quaternion spawnRotation)
    {
        var effect = effects.GetFreeElement();
        effect.transform.position = spawnPosition;
        effect.transform.rotation = spawnRotation;
    }
}
