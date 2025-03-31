using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerEffects : ExteranalEffects
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.HitEffectsCreated += OnHitEffect;
        _player.DeathEffectsCreated += OnDeathEffect;
    }
    private void OnDisable()
    {
        _player.HitEffectsCreated += OnHitEffect;
        _player.DeathEffectsCreated += OnDeathEffect;
    }
}
