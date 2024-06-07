using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class SkeletBT : AbstractTree
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _enemySearchRadius = 10f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _attackTime = 1f;

    private Rigidbody2D _rb;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    protected override Node SetupTree()
    {
        Node node = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                    {
                        new CheckTargetInAttackRange(_rb, _attackRange),
                        new TaskAttack(_rb, _animator, _attackTime)
                    }),
                new Sequence(new List<Node>
                    {
                        new CheckTargetinFOVRange(_rb, _enemySearchRadius),
                        new TaskGoToTarget(_rb, _speed, _attackRange)
                    }),
                new TaskPatrol(_rb, _waypoints, _speed)
            });

        return node;
    }
}

