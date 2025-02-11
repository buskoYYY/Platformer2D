using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionHandler))]
public class Player : MonoBehaviour
{
    [Header("Elements")]
    private InputReader _inputReader;
    private Mover _playerMotion;
    private PlayerAnimator _playerAnimator;
    private CollisionHandler _collisionHandler;
    private Health _health;

    [Header("Interface")]
    private IInteractable _interactable;

    [Header("Elements")]
    [SerializeField] private int _maxHealth;

    private void Awake()
    {
        _health = new Health(_maxHealth);

        _inputReader = GetComponent<InputReader>();
        _playerMotion = GetComponent<Mover>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionHappend += OnCollisionHappend;
    }
    private void OnDisable()
    {
        _collisionHandler.CollisionHappend -= OnCollisionHappend;
    }

    void FixedUpdate()
    {
        _playerMotion.Move(_inputReader.GetMoveInput());
        _playerAnimator.SetMoveAnimation(_inputReader.GetMoveInput());
        _playerAnimator.SetAttackAnimation(_inputReader.GetAttack());

        if (_inputReader.GetIsInteract() && _interactable != null)
        {
            _interactable.Interact();
        }
    }
    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);
    }
    public void Heal(int value)
    {
        _health.Heal(value);
    }

    private void OnCollisionHappend(IInteractable interactable)
    {
        _interactable = interactable;
    }
}
