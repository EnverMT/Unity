using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseSpawner<T> : MonoBehaviour where T : BaseFieldObject
{
    [SerializeField] private T _prefab;
    private ObjectPool<T> _pool;

    public event Action<T> Spawned;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject)
        );
    }

    public virtual T Spawn(Vector3 position, Color color)
    {
        if (_prefab == null)
            throw new ArgumentException("Spawn object must be BaseFieldObject");

        T obj = _pool.Get();

        obj.Died += OnDied;
        obj.Init(gameObject.transform, UnityEngine.Random.rotation, position, color);

        Spawned?.Invoke(obj);

        return obj;
    }

    protected virtual void OnDied(BaseFieldObject obj)
    {
        obj.Died -= OnDied;
        _pool.Release((T)obj);
    }
}