using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseFieldObject : MonoBehaviour
{
    public Action<BaseFieldObject> Collided;
    public Action<BaseFieldObject> Died;

    protected Rigidbody _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
}
