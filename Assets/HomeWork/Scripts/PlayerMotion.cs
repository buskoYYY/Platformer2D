using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotion : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Sprite[] _directionSprites;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    [Header("Settings")]
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _extraSpeed;
    private float _currentSpeed;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _currentSpeed = _normalSpeed;
    }
    private void Update()
    {
        SpeedBoost();
        ChangeSprites();
    }
    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        _rigidbody.velocity = _moveInput * _currentSpeed;
    }
    private void SpeedBoost()
    {
        if (Input.GetKey(KeyCode.B))
        {
            _currentSpeed = _extraSpeed;
        }
        else
        {
            _currentSpeed = _normalSpeed;
        }
    }

    private void ChangeSprites()
    {
        if (_moveInput.y > 0)

        {
            _spriteRenderer.sprite = _directionSprites[0]; // ¬верх
        }

        else if (_moveInput.y < 0)

        {
            _spriteRenderer.sprite = _directionSprites[1]; // ¬низ
        }

        else if (_moveInput.x < 0)

        {
            _spriteRenderer.sprite = _directionSprites[2]; // ¬лево
        }

        else if (_moveInput.x > 0)

        {
            _spriteRenderer.sprite = _directionSprites[3]; // ¬право
        }
    }
}
