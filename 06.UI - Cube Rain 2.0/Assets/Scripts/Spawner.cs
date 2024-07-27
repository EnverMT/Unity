using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private BaseFieldObject[] _spawnableObjects;
    [SerializeField, Range(0f, 9f)] private float _spawnRadius = 9f;

    private readonly List<BaseFieldObject> _currentSpawns = new();
    private readonly Dictionary<string, int> _totalSpawns = new(); // Class name, count

    public event Action<BaseFieldObject> Spawned;
    public event Action ValueChanged;


    public BaseFieldObject Spawn<T>(Vector3? position) where T : BaseFieldObject
    {
        T prefab = _spawnableObjects.OfType<T>().FirstOrDefault();

        if (prefab == null)
            throw new ArgumentException("Spawn object must be BaseFieldObject");

        BaseFieldObject obj = Instantiate(prefab);

        if (_totalSpawns.ContainsKey(typeof(T).Name))
            _totalSpawns[typeof(T).Name] += 1;
        else
            _totalSpawns.Add(typeof(T).Name, 1);


        _currentSpawns.Add(obj);
        obj.Died += OnDied;

        obj.Init(gameObject.transform, UnityEngine.Random.rotation, position ?? GetStandardSpawnPosition());

        Spawned?.Invoke(obj);
        ValueChanged?.Invoke();

        return obj;
    }

    public int GetCurrentSpawns<T>() where T : BaseFieldObject
    {
        return _currentSpawns.OfType<T>().Count();
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

    private void OnDied(BaseFieldObject obj)
    {
        _currentSpawns.Remove(obj);
        Destroy(obj.gameObject);
        ValueChanged?.Invoke();
    }
}