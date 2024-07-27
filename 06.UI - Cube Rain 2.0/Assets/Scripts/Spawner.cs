using System;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Action<BaseFieldObject> Spawned;

    [SerializeField] private BaseFieldObject[] _spawnableObjects;

    [Header("Spawn coordinates")]
    [SerializeField] private Vector3 _spawnCenter;
    [SerializeField, Range(0f, 9f)] private float _spawnRadius = 9f;


    public BaseFieldObject Spawn<T>(Vector3? position) where T : BaseFieldObject
    {
        T prefab = _spawnableObjects.OfType<T>().FirstOrDefault();

        if (prefab == null)
            throw new ArgumentException("Spawn object must be BaseFieldObject");

        BaseFieldObject obj = Instantiate(prefab);

        obj.transform.SetParent(gameObject.transform, false);
        obj.transform.rotation = UnityEngine.Random.rotation;

        if (position != null)
            obj.transform.position = (Vector3)position;
        else
            obj.transform.position = GetStandardSpawnPosition();

        Spawned?.Invoke(obj);

        return obj;
    }

    protected virtual Vector3 GetStandardSpawnPosition()
    {
        Vector2 random2 = UnityEngine.Random.insideUnitCircle * _spawnRadius;
        Vector3 random3 = new Vector3(random2.x, 0, random2.y);

        return random3 + _spawnCenter;
    }
}