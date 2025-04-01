using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private int _maxHealth;

    protected Health Health { get; private set; }

    protected virtual void Awake()
    {
        Health = new Health(_maxHealth);
        _healthBar.Initialize(Health);
    }

    protected virtual void OnEnable()
    {
        Health.TakingDamage += OnTakingDamage;
        Health.Died += OnDied;
    }

    protected virtual void OnDisable()
    {
        Health.TakingDamage -= OnTakingDamage;
        Health.Died -= OnDied;

    }

    public void ApplyDamage(int damage)
    {
        Health.ApplyDamage(damage);

    }

    public void Heal(int value)
    {
        Health.Heal(value);
    }

    protected abstract void OnTakingDamage();

    protected abstract void OnDied();
}
