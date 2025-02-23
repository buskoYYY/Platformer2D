using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    private Health _health;

    [SerializeField] private string _initState;
    [SerializeField] private StateMachineState[] _states;
    [SerializeField] private int _maxHealth;
    public  Transform Player { get; set; }
    public StateMachineState CurrentState { get; set; }
    private void Awake()
    {
        _health = new Health(_maxHealth);
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
            Destroy(gameObject);
    }
}
