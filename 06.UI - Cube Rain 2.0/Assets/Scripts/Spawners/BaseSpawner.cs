using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseSpawner<T> : MonoBehaviour where T : BaseFieldObject
{
    public ObjectPool<T> Pool => _pool;
    public abstract T Prefab { get; }

    private ObjectPool<T> _pool;

    public event Action<T> Spawned;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(Prefab),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject)
        );
    }

    public virtual T Spawn(Vector3 position, Color color)
    {
        if (Prefab == null)
            throw new ArgumentException("Spawn object must be BaseFieldObject");

        T obj = Pool.Get();

        obj.Died += OnDied;
        obj.Init(gameObject.transform, UnityEngine.Random.rotation, position, color);

        Spawned?.Invoke(obj);

        return obj;
    }

    protected virtual void OnDied(BaseFieldObject obj)
    {
        obj.Died -= OnDied;
        Pool.Release((T)obj);
    }
}