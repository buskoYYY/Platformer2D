using System;
using UnityEngine;

[RequireComponent(typeof(EnemySound))]
public class EnemyBrain : MonoBehaviour
{
    public static event Action<Vector2> StartDeathEffects;
    public static event Action<Vector2, Quaternion> EnemyHitEffect;

    [Header("Elements")]
    [SerializeField] private StateMachineState[] _states;
    [SerializeField] private HealthBar _healthBar;
    private EnemySound _sound;
    private Health _health;

    [Header("Settings")]
    [SerializeField] private string _initState;
    [SerializeField] private int _maxHealth;
    public  Transform Player { get; set; }
    public StateMachineState CurrentState { get; set; }
    private void Awake()
    {
        _health = new Health(_maxHealth);
        _healthBar.Initialize(_health);
        _sound = GetComponent<EnemySound>();
    }
    private void Start()
    {
        ChangeState(_initState);
    }
    private void FixedUpdate()
    {
        CurrentState?.UpadateState(this);
    }

    public void ChangeState(string newStateID)
    {
        StateMachineState newState = GetState(newStateID);
        if (newState == null) return;
        CurrentState = newState;
    }

    private StateMachineState GetState(string newStateID)
    {
        for (int i = 0; i < _states.Length; i++)
        {
            if (_states[i].ID == newStateID)
            {
                return _states[i];
            }
        }
        return null;
    }
    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);
        if (_health.Value <= 0)
        {
            _sound.PlayDeathSound();
            StartDeathEffects?.Invoke(transform.position);
            Destroy(gameObject);
        }
        else
        {
            _sound.PlayHitSound();
            EnemyHitEffect?.Invoke(transform.position, transform.rotation);
        }
    }
}
