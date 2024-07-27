using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ObjectsPool))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private BaseFieldObject[] _spawnableObjects;
    [SerializeField, Range(0f, 9f)] private float _spawnRadius = 9f;

    private readonly Dictionary<string, int> _totalSpawns = new(); // Class name, count    
    [SerializeField] private ObjectsPool _pool;

    public event Action<BaseFieldObject> Spawned;
    public event Action ValueChanged;


    public T Spawn<T>(Vector3? position) where T : BaseFieldObject
    {
        T prefab = _spawnableObjects.OfType<T>().FirstOrDefault();

        if (prefab == null)
            throw new ArgumentException("Spawn object must be BaseFieldObject");

        //T obj = Instantiate(prefab);
        T obj = _pool.GetObject(prefab);

        if (_totalSpawns.ContainsKey(typeof(T).Name))
            _totalSpawns[typeof(T).Name] += 1;
        else
            _totalSpawns.Add(typeof(T).Name, 1);

        obj.Died += OnDied;

        obj.Init(gameObject.transform, UnityEngine.Random.rotation, position ?? GetStandardSpawnPosition());

        Spawned?.Invoke(obj);
        ValueChanged?.Invoke();

        return obj;
    }

    public int GetCurrentSpawns<T>() where T : BaseFieldObject
    {
        return _pool.GetCount<T>();
    }

    public int GetTotalSpawns<T>() where T : BaseFieldObject
    {
        return _totalSpawns.TryGetValue(typeof(T).Name, out var value) ? value : 0;
    }

    protected virtual Vector3 GetStandardSpawnPosition()
    {
        Vector2 random2 = UnityEngine.Random.insideUnitCircle * _spawnRadius;
        Vector3 random3 = new Vector3(random2.x, 0, random2.y);

        return random3 + gameObject.transform.position;
    }

    private void OnDied<T>(T obj) where T : BaseFieldObject
    {
        _pool.ReturnObject<T>(obj);
        ValueChanged?.Invoke();
    }
}