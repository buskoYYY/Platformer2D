using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string GROUND_TAG = "Ground";

    [Header("Elements")]
    private Rigidbody2D _rigidbody;

    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForse;
    private Vector2 _moveInput;
    private float _direction;
    private bool _isGround = true;
    private bool _isJump;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _direction = Input.GetAxis(HORIZONTAL_AXIS);

        if (_isGround && Input.GetKeyDown(KeyCode.Space))
        {
            _isJump = true;
        }
    }
   
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction * _speed * Time.fixedDeltaTime, _rigidbody.velocity.y);

        if (_isJump)
        {
            Debug.Log("IsJump");
            _rigidbody.AddForce(transform.up * _jumpForse, ForceMode2D.Impulse);
            _isJump = false;
            _isGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GROUND_TAG)
        {
            _isGround = true;
        }
    }
}
