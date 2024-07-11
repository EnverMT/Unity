using System;
using UnityEngine;

public class HealthAttribute : BaseAttribute<float>
{
    [SerializeField] private bool _isImmortal;

    public event Action<HealthAttribute> Died;

    public override IAttribute<float> ChangeValue(float value)
    {
        if (_isImmortal && value < 0)
            return this;

        Value = Mathf.Clamp(Value + value, 0f, MaxValue);

        if (Value <= 0f)
            Died?.Invoke(this);


        return this;
    }
}