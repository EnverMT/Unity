using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Enemy : MonoBehaviour
{
    private const float DistanceToDie = 0.1f;

    private Target _target;
    private float _speed;
    private Renderer _renderer;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        Vector3 direction = (_target.gameObject.transform.position - gameObject.transform.position).normalized;

        gameObject.transform.Translate(direction * _speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(gameObject.transform.position, _target.gameObject.transform.position) <= DistanceToDie)
            Died?.Invoke(this);
    }

    public void Init(Vector3 spawnPosition, Target target, float speed, Color color)
    {
        _renderer.material.color = color;
        _target = target;
        _speed = speed;
        gameObject.transform.position = spawnPosition;
    }
}
