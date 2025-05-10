using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerHitEffectsPool : EffectsPool
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.HitEffectsTriggered += CreateHitEffect;
        _player.PuffEffectsTriggered += CreatePuffEffect;
    }

    private void OnDisable()
    {
        _player.HitEffectsTriggered -= CreateHitEffect;
        _player.PuffEffectsTriggered -= CreatePuffEffect;
    }
}
