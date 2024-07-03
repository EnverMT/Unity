using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHP;
    [SerializeField] private uint _maxHP;
    [SerializeField] public bool _isImmortal;

    public int CurrentHP
    {
        get => _currentHP;

        private set
        {
            _currentHP = value;
            HealthChanged?.Invoke(this);
        }
    }

    public uint MaxHP
    {
        get => _maxHP;

        set
        {
            _maxHP = value;

            if (CurrentHP > value)
                CurrentHP = (int)value;

            HealthChanged?.Invoke(this);
        }
    }

    public bool IsAlive => CurrentHP > 0;

    public event Action<Health> HealthChanged;

    private void OnEnable()
    {
        MaxHP = 100;
        CurrentHP = (int)MaxHP;
    }

    public void TakeDamage(uint amount)
    {
        if (_isImmortal)
            return;

        CurrentHP = Mathf.Clamp(CurrentHP - (int)amount, 0, (int)_maxHP);
        Debug.Log($"CurrentHP={CurrentHP}");
    }

    public void Heal(uint amount)
    {
        CurrentHP = Mathf.Clamp(CurrentHP + (int)amount, 0, (int)_maxHP);
    }
}
