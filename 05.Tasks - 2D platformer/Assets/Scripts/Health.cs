using System;
using UnityEngine;

public class Health : MonoBehaviour, IAttribute
{
    [SerializeField] public bool _isImmortal;
    [SerializeField] private uint _currentHP;
    [SerializeField] private uint _maxHP = 100;

    public event Action<IAttribute> ValueChanged;
    public event Action<IAttribute> Died;

    public uint Value
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

    public uint MaxValue
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
