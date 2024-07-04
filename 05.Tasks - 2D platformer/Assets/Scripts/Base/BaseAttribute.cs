using System;
using UnityEngine;


public abstract class BaseAttribute<T> : MonoBehaviour, IAttribute<T>
{
    public abstract T Value { get; protected set; }
    public abstract T MaxValue { get; set; }

    public abstract event Action<IAttribute<T>> ValueChanged;
}
