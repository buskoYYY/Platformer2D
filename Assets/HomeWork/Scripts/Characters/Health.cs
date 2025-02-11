using System;
using UnityEngine;

public class Health
{
    public Health(int maxValue)
    {
        MaxValue = maxValue;
        Value = maxValue;
    }

    public int MaxValue {  get; private set; }
    public int Value {  get; private set; }

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            return;
        ChangeValue(-damage);
    }
    public void Heal ( int value)
    {
        if (value < 0)
            return;
        ChangeValue(value);
    }
    public void ChangeValue(int value)
    { Value = Mathf.Clamp(Value + value, 0, MaxValue); }
}
