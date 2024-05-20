using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public event Action<Enemy> Died;

    private Rigidbody _rigidBody;
    private Vector3 _direction;
    private float _speed;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        gameObject.transform.Translate(_direction * _speed * Time.deltaTime);

        if (gameObject.transform.position.y < 0)
            Died?.Invoke(this);
    }

    public void Init(Vector3 spawnPosition, Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
        gameObject.transform.position = spawnPosition;
    }
}
