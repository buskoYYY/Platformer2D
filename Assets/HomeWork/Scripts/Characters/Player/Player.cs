using System;
using UnityEngine;

[RequireComponent(typeof(PlayerSounds), typeof(PlayerMover), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionHandler), typeof(PlayerAttacker))]
public class Player : Character
{
    [SerializeField] private Canvas _interactableCanvas;
    [SerializeField] private InventoryView _inventoryView;

    private PlayerMover _playerMotion;
    private PlayerAnimator _playerAnimator;
    private PlayerAttacker _playerAttacker;
    private CollisionHandler _collisionHandler;
    private PlayerSounds _audio;
    private Inventory _inventory;

    public event Action Died;
    public event Action<Vector2> DeathEffectsTriggered;
    public event Action<Vector2, Quaternion> HitEffectsTriggered;
    public event Action<Vector2, Quaternion> PuffEffectsTriggered;

    private IInteractable _interactable;
    private IInputReader _inputReader;

    protected override void Awake()
    {
        base.Awake();

        _inventory = new Inventory();

        _playerMotion = GetComponent<PlayerMover>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _playerAttacker = GetComponent<PlayerAttacker>();
        _audio = GetComponent<PlayerSounds>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _collisionHandler.InteractacleFounded += OnInteractableFounded;
        _collisionHandler.MedKitFounded += OnMedKitFounded;
        _collisionHandler.KeyFounded += OnKeyFounded;

        _inventory.ItemAdded += AddItemToInventory;
        _inventory.ItemRemoved += _inventoryView.Remove;
    }

    protected override void OnDisable()
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
            _playerMotion.SetCurrentSpeed(true);
            PuffEffectsTriggered?.Invoke(transform.position, transform.rotation);
        }
        else
        {
            _playerMotion.SetCurrentSpeed(false);
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

    protected override void OnTakingDamage()
    {
        _audio.PlayHitSound();
        HitEffectsTriggered?.Invoke(transform.position, transform.rotation);
    }

    protected override void OnDied()
    {
        DeathEffectsTriggered?.Invoke(transform.position);
        _audio.PlayDeathSound();
        Died?.Invoke();
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
        if (Health.Value < Health.MaxValue)
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
