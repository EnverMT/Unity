using System;
using UnityEngine;


public abstract class BaseAttr<T> : MonoBehaviour, IAttr<T>
{
    public abstract T Value { get; protected set; }
    public abstract T MaxValue { get; set; }

    public abstract event Action<IAttr<T>> ValueChanged;
}
