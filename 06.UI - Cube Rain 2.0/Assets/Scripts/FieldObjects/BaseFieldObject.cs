using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public abstract class BaseFieldObject : MonoBehaviour
{
    protected Rigidbody _rb;
    protected Renderer _renderer;

    public Action<BaseFieldObject> Died;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }
}
