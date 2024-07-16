using System;
using System.Collections.Generic;
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

        protected set
        {
            _maxValue = value;

            if (Comparer<T>.Default.Compare(Value, MaxValue) > 0)
            {
                Value = value;
                return;
            }

            ValueChanged?.Invoke(this);
        }
    }

    public abstract IAttribute<T> ChangeValue(T value);
}