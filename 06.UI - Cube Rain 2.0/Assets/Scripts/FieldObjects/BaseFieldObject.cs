using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public abstract class BaseFieldObject : MonoBehaviour
{
    protected Rigidbody _rb;
    protected Renderer _renderer;

    public event Action<BaseFieldObject> Died;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public void Init(Transform parent, Quaternion rotation, Vector3 position)
    {
        gameObject.transform.SetParent(parent.gameObject.transform, false);
        gameObject.transform.rotation = UnityEngine.Random.rotation;
        gameObject.transform.position = (Vector3)position;
    }

    protected void OnDied<T>(T obj) where T : BaseFieldObject
    {
        Died?.Invoke(obj);
    }
}