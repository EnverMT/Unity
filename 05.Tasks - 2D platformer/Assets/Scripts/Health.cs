using System;
using UnityEngine;

public class Health : BaseAttribute
{
    [SerializeField] public bool _isImmortal;
    [SerializeField] private float _currentHP;
    [SerializeField] private float _maxHP = 100f;

    public override event Action<IAttribute<float>> ValueChanged;
    public event Action<IAttribute<float>> Died;

    public override float Value
    {
        get => _currentHP;

        protected set
        {
            _currentHP = (uint)Mathf.Clamp(value, 0, (int)MaxValue);
            ValueChanged?.Invoke(this);

            if (_currentHP == 0)
                Died?.Invoke(this);

            Debug.Log($"CurrentHP={Value}");
        }
    }

    public override float MaxValue
    {
        get => _maxHP;

        set
        {
            _maxHP = (uint)Mathf.Clamp(value, 0, int.MaxValue); ;

            if (Value > _maxHP)
                Value = _maxHP;

            ValueChanged?.Invoke(this);
        }
    }

    public bool IsAlive => Value > 0;


    private void OnEnable()
    {
        Value = MaxValue;
    }

    public void TakeDamage(uint amount)
    {
        if (_isImmortal)
            return;

        Value -= amount;
    }

    public void Heal(uint amount)
    {
        Value += amount;
    }
}
