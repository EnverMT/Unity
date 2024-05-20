using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private ObjectPool<Enemy> _pool;
    private Rigidbody _rigidBody;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        gameObject.transform.Translate(_direction * _speed * Time.deltaTime);

        if (gameObject.transform.position.y < 0)
            _pool.Release(this);
    }

    public void Init(Vector3 spawnPosition, Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
        gameObject.transform.position = spawnPosition;
    }

    public void SetPool(ObjectPool<Enemy> pool)
    {
        _pool = pool;
    }
}
