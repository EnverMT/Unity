using System;
using UnityEngine;


public abstract class BaseAttribute<T> : MonoBehaviour, IAttribute<T>
{
    public virtual event Action<IAttribute<T>> ValueChanged;    

    [SerializeField] protected T _value;
    [SerializeField] protected T _maxValue;

    public virtual T Value
    {
        get => _value;

        protected set
        {
            _value = value;
            ValueChanged?.Invoke(this);
        }
    }

    public virtual T MaxValue
    {
        get => _maxValue;

        set
        {
            _maxValue = value;
            ValueChanged?.Invoke(this);
        }
    }

    public abstract void ChangeValue(T value);
}