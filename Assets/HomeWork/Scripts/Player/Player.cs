using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionHandler))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private Mover _playerMotion;
    private PlayerAnimator _playerAnimator;
    private CollisionHandler _collisionHandler;

    private IInteractable _interactable;

    private void Awake()
    {
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
    private void OnCollisionHappend(IInteractable interactable)
    {
        _interactable = interactable;
    }
}
