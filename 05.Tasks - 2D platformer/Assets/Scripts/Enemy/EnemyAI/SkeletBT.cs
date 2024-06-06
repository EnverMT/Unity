using BehaviorTree;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkeletBT : AbstractTree
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 2f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override Node SetupTree()
    {
        return new TaskPatrol(_rb, _waypoints, _speed);
    }
}

