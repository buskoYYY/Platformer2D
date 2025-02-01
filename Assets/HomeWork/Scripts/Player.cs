using UnityEngine;

[RequireComponent (typeof(InputReader), typeof(PlayerMotion), typeof(PlayerSpriteVariation))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMotion _playerMotion;
    private PlayerSpriteVariation _playerSpriteVariation;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerMotion = GetComponent<PlayerMotion>();
        _playerSpriteVariation = GetComponent<PlayerSpriteVariation>();
    }

    void Update()
    {
        _playerMotion.Move(_inputReader.GetMoveInput());
        _playerSpriteVariation.ChangeSprites(_inputReader.GetMoveInput());
    }
}
