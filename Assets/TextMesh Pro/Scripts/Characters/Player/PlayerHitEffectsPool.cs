using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerHitEffectsPool : HitEffectPool
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.HitEffectsTriggered += CreateHitEffect;
    }

    private void OnDisable()
    {
        _player.HitEffectsTriggered -= CreateHitEffect;
    }
}
