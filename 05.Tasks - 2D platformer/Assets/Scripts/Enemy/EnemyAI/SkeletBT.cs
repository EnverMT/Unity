using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(BaseUnit))]
[RequireComponent(typeof(Rigidbody2D))]
public class SkeletBT : AbstractTree
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _enemySearchRadius = 10f;

    private PlayerAttack _attack;
    private BaseUnit _baseUnit;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _attack = GetComponent<PlayerAttack>();
        _baseUnit = GetComponent<BaseUnit>();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override Node SetupTree()
    {
        Node node = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                    {
                        new CheckTargetInAttackRange(_baseUnit),
                        new TaskAttack(_baseUnit)
                    }),
                new Sequence(new List<Node>
                    {
                        new CheckTargetinFOVRange(_rb, _enemySearchRadius),
                        new TaskGoToTarget(_rb, _speed, _attack.Range)
                    }),
                new TaskPatrol(_rb, _waypoints, _speed)
            });

        return node;
    }
}

