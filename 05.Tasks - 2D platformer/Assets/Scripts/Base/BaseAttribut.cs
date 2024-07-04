using System;
using UnityEngine;

public abstract class BaseAttribut : MonoBehaviour, IAttribute
{
    public abstract uint Value { get; protected set; }
    public abstract uint MaxValue { get; set; }

    public abstract event Action<IAttribute> ValueChanged;
}
