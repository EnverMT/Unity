using System;
using UnityEngine;


public abstract class BaseAttribute : MonoBehaviour, IAttribute<float>
{
    public abstract float Value { get; protected set; }
    public abstract float MaxValue { get; set; }

    public abstract event Action<IAttribute<float>> ValueChanged;
}
