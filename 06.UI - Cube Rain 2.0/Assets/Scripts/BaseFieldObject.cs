using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public abstract class BaseFieldObject : MonoBehaviour
{
    public Action<BaseFieldObject> Died;

    protected Rigidbody _rb;
    protected Renderer _renderer;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }
}
