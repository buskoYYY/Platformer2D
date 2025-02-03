using UnityEngine;

[RequireComponent (typeof(InputReader), typeof(PlayerMotion), typeof(PlayerSpriteVariation))]
[RequireComponent(typeof(CollisionHandler))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMotion _playerMotion;
    private PlayerSpriteVariation _playerSpriteVariation;
    private CollisionHandler _collisionHandler;

    private IInteractable _interactable;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerMotion = GetComponent<PlayerMotion>();
        _playerSpriteVariation = GetComponent<PlayerSpriteVariation>();
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
        _playerSpriteVariation.ChangeSprites(_inputReader.GetMoveInput());
        
        if(_inputReader.GetIsInteract() && _interactable != null)
        {
            _interactable.Interact();
        }
    }
    private void OnCollisionHappend(IInteractable interactable)
    {
        _interactable = interactable;
    }
}
