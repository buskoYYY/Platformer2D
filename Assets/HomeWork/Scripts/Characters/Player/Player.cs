using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionHandler), typeof(PlayerAttacker))]
public class Player : MonoBehaviour
{
    public event Action Died;
    public static event Action <Vector2> StartDeathEffects;
    public static event Action<Vector2, Quaternion> PlayerHitEffect;

    [Header("Elements")]
    [SerializeField] private HealthBar _healthBar;
    private InputReader _inputReader;
    private Mover _playerMotion;
    private PlayerAnimator _playerAnimator;
    private PlayerAttacker _playerAttacker;
    private CollisionHandler _collisionHandler;
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
    }

    private void OnEnable()
    {
        Health.Died += Ondied;
        _collisionHandler.CollisionHappend += OnCollisionHappend;
    }
    private void OnDisable()
    {
        Health.Died -= Ondied;
        _collisionHandler.CollisionHappend -= OnCollisionHappend;
    }
    void FixedUpdate()
    {
        if (TimeManager.IsPaused) return;

        _playerMotion.Move(_inputReader.GetMoveInput());
        _playerAnimator.SetMoveAnimation(_inputReader.GetMoveInput());
        _playerAttacker.Attack(_inputReader, _playerAnimator);

        if (_inputReader.GetIsInteract() && _interactable != null)
        {
            _interactable.Interact();
        }
    }
    public void ApplyDamage(int damage)
    {
        Debug.Log(_health.Value);
        if (_health.Value <= 0)
        {
            StartDeathEffects?.Invoke(transform.position);
            Destroy(gameObject);
        }
        else
        {
            _health.ApplyDamage(damage);
            PlayerHitEffect?.Invoke(transform.position, transform.rotation);
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
