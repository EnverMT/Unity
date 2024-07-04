using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public bool _isImmortal;
    [SerializeField] private uint _currentHP;
    [SerializeField] private uint _maxHP = 100;

    public event Action<Health> HealthChanged;
    public event Action<Health> Died;

    public uint CurrentHP
    {
        get => _currentHP;

        private set
        {
            _currentHP = (uint)Mathf.Clamp(value, 0, (int)MaxHP);
            HealthChanged?.Invoke(this);

            if (_currentHP == 0)
                Died?.Invoke(this);

            Debug.Log($"CurrentHP={CurrentHP}");
        }
    }

    public uint MaxHP
    {
        get => _maxHP;

        set
        {
            _maxHP = (uint)Mathf.Clamp(value, 0, int.MaxValue); ;

            if (CurrentHP > _maxHP)
                CurrentHP = _maxHP;

            HealthChanged?.Invoke(this);
        }
    }

    public bool IsAlive => CurrentHP > 0;


    private void OnEnable()
    {
        CurrentHP = MaxHP;
    }

    public void TakeDamage(uint amount)
    {
        if (_isImmortal)
            return;

        CurrentHP -= amount;
    }

    public void Heal(uint amount)
    {
        CurrentHP += amount;
    }
}
