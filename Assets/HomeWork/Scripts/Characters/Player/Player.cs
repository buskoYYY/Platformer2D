using System;
using UnityEngine;

[RequireComponent(typeof(PlayerSounds), typeof(PlayerMover), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionHandler), typeof(PlayerAttacker))]
public class Player : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Canvas _interactableCanvas;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private int _maxHealth;

    private PlayerMover _playerMotion;
    private PlayerAnimator _playerAnimator;
    private PlayerAttacker _playerAttacker;
    private CollisionHandler _collisionHandler;
    private PlayerSounds _audio;
    private Health _health;
    private Inventory _inventory;

    public event Action Died;
    public event Action<Vector2> DeathEffectsCreated;
    public event Action<Vector2, Quaternion> HitEffectsCreated;

    private IInteractable _interactable;
    private IInputReader _inputReader;

    private void Awake()
    {
        _health = new Health(_maxHealth);
        _inventory = new Inventory();
        _healthBar.Initialize(_health);

        _playerMotion = GetComponent<PlayerMover>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _playerAttacker = GetComponent<PlayerAttacker>();
        _audio = GetComponent<PlayerSounds>();
    }

    private void OnEnable()
    {
        _collisionHandler.InteractacleFounded += OnInteractableFounded;
        _collisionHandler.MedKitFounded += OnMedKitFounded;
        _collisionHandler.KeyFounded += OnKeyFounded;

        _inventory.ItemAdded += AddItemToInventory;
        _inventory.ItemRemoved += _inventoryView.Remove;
    }

    private void OnDisable()
    {
        _collisionHandler.InteractacleFounded -= OnInteractableFounded;
        _collisionHandler.MedKitFounded -= OnMedKitFounded;
        _collisionHandler.KeyFounded -= OnKeyFounded;

        _inventory.ItemAdded -= AddItemToInventory;
        _inventory.ItemRemoved -= _inventoryView.Remove;
    }

    private void FixedUpdate()
    {
        if (TimeManager.IsPaused)
            return;

        _playerMotion.Move(_inputReader.Direction, _audio);
        _playerAnimator.SetMoveAnimation(_inputReader.Direction);

        if (_inputReader.GetAttack())
        {
            if (_playerAttacker.Attack(_playerAnimator))
            {
                _audio.PlayAttackSound();
            }
        }

        if(_inputReader.GetSpeedUp())
        {
            _playerMotion.CheckAcceleration(true);
        }
        else
        {
            _playerMotion.CheckAcceleration(false);
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

    public void Initialize(IInputReader inputReader)
    {
        _inputReader = inputReader;
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);

        if (_health.Value > 0)
        {
            _audio.PlayHitSound();
            HitEffectsCreated?.Invoke(transform.position, transform.rotation);
        }
        else
        {
            DeathEffectsCreated?.Invoke(transform.position);
            _audio.PlayDeathSound();
            Died?.Invoke();
        }
    }

    public void Heal(int value)
    {
        _health.Heal(value);
    }

    private void AddItemToInventory(IItem item)
    {
        _inventoryView.Add(item);
        item.Collect();
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
}
