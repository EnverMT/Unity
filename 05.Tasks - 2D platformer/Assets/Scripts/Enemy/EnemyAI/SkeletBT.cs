using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkeletBT : AbstractTree
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _enemySearchRadius = 10f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override Node SetupTree()
    {
        Node node = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                    {
                        new CheckTargetinFOVRange(_rb, _enemySearchRadius),
                        new TaskGoToTarget(_rb, _speed)
                    }),
                new TaskPatrol(_rb, _waypoints, _speed)
            });

        return node;
    }
}

