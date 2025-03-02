using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionHandler), typeof(PlayerAttacker), typeof(PlayerSounds))]
public class Player : MonoBehaviour
{
    public event Action Died;
    public static event Action<Vector2> StartDeathEffects;
    public static event Action<Vector2, Quaternion> PlayerHitEffect;

    [Header("Elements")]
    [SerializeField] private HealthBar _healthBar;
    private InputReader _inputReader;
    private Mover _playerMotion;
    private PlayerAnimator _playerAnimator;
    private PlayerAttacker _playerAttacker;
    private CollisionHandler _collisionHandler;
    private PlayerSounds _audio;
    private Health _health;

    [Header("Interface")]
    private IInteractable _interactable;

    [Header("Settings")]
    [SerializeField] private int _maxHealth;

    private void Awake()
    {
        _health = new Health(_maxHealth);
        _healthBar.Initialize(_health);

        _inputReader = GetComponent<InputReader>();
        _playerMotion = GetComponent<Mover>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _playerAttacker = GetComponent<PlayerAttacker>();
        _audio = GetComponent<PlayerSounds>();
    }

    private void OnEnable()
    {
        Health.PlayerDied += Ondied;
        _collisionHandler.CollisionHappend += OnCollisionHappend;
    }
    private void OnDisable()
    {
        Health.PlayerDied -= Ondied;
        _collisionHandler.CollisionHappend -= OnCollisionHappend;
    }
    void FixedUpdate()
    {
        if (TimeManager.IsPaused) return;

        _playerMotion.Move(_inputReader.GetMoveInput(), _audio);

        _playerAnimator.SetMoveAnimation(_inputReader.GetMoveInput());
        if (_playerAttacker.Attack(_inputReader, _playerAnimator))
        {
            _audio.PlayAttackSound();
        }

        if (_inputReader.GetIsInteract() && _interactable != null)
        {
            _interactable.Interact();
        }
    }
    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);
        if (_health.Value > 0)
        {
            _audio.PlayHitSound();
            PlayerHitEffect?.Invoke(transform.position, transform.rotation);
        }
        else
        {
            StartDeathEffects?.Invoke(transform.position);
            _audio.PlayDeathSound();
            Died?.Invoke();
        }
    }
    public void Heal(int value)
    {
        _health.Heal(value);
    }

    private void OnCollisionHappend(IInteractable interactable)
    {
        _interactable = interactable;
    }
    private void Ondied()
    {
        Died?.Invoke();
    }
}
