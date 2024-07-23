using System;
using UnityEngine;

public class HealthAttribute : BaseAttribute<float>, IBarIndicator
{
    [SerializeField] private bool _isImmortal;

    public event Action<HealthAttribute> Died;
    public event Action<float> IndicatorValueChanged;

    public bool IsAlive => Value > 0;

    public float InitIndicatorValue => 1f;

    public override IAttribute<float> Increase(float value)
    {
        if (value < 0f)
            throw new ArgumentOutOfRangeException("Value cannot be negative");

        Value = Mathf.Clamp(Value + value, 0f, MaxValue);
        IndicatorValueChanged?.Invoke(Value / MaxValue);

        return this;
    }

    public override IAttribute<float> Decrease(float value)
    {
        if (value < 0f)
            throw new ArgumentOutOfRangeException("Value cannot be negative");

        if (_isImmortal)
            return this;

        if (!IsAlive)
            Died?.Invoke(this);

        Value = Mathf.Clamp(Value - value, 0f, MaxValue);
        IndicatorValueChanged?.Invoke(Value / MaxValue);

        return this;
    }
}