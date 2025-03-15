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
    [SerializeField] private Canvas _interactableCanvas;
    [SerializeField] private InventoryView _inventoryView;
    private InputReader _inputReader;
    private Mover _playerMotion;
    private PlayerAnimator _playerAnimator;
    private PlayerAttacker _playerAttacker;
    private CollisionHandler _collisionHandler;
    private PlayerSounds _audio;
    private Health _health;
    private Inventory _inventory;

    [Header("Interface")]
    private IInteractable _interactable;

    [Header("Settings")]
    [SerializeField] private int _maxHealth;

    private void Awake()
    {
        _health = new Health(_maxHealth);
        _inventory = new Inventory();
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
        _collisionHandler.InteractacleFounded += OnInteractableFounded;
        _collisionHandler.MedKitFounded += OnMedKitFounded;
        _collisionHandler.KeyFounded += OnKeyFounded;

        _inventory.ItemAdded += AddItemToInventory;
        _inventory.ItemRemoved += _inventoryView.Remove;
    }

    private void OnDisable()
    {
        Health.PlayerDied -= Ondied;
        _collisionHandler.InteractacleFounded -= OnInteractableFounded;
        _collisionHandler.MedKitFounded -= OnMedKitFounded;
        _collisionHandler.KeyFounded -= OnKeyFounded;

        _inventory.ItemAdded -= AddItemToInventory;
        _inventory.ItemRemoved -= _inventoryView.Remove;
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
            if (_interactable.IsLock)
            {
                if (_inventory.Contains(_interactable.Key))
                {
                    _interactable.Unlock((Key)_inventory.Take(_interactable.Key));
                }
                else
                {
                    _interactable.Interact();
                }
            }
            else
            {
                _interactable.Interact();
            }
        }

    }
    private void AddItemToInventory(IItem item)
    {
        _inventoryView.Add(item);
        item.Collect();
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

    private void OnInteractableFounded(IInteractable interactable)
    {
        _interactable = interactable;
        _interactableCanvas.gameObject.SetActive(interactable != null);
    }
    private void OnMedKitFounded(MedKit medKit)
    {
        if (_health.Value < _health.MaxValue)
        {
            Heal(medKit.Value);
            medKit.Collect();
        }
    }
    private void OnKeyFounded(Key key)
    {
        _inventory.Add(key);
    }
    private void Ondied()
    {
        Died?.Invoke();
    }
}
