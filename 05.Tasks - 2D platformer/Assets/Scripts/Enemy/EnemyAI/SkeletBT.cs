using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseAttack))]
[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Rigidbody2D))]
public class SkeletBT : AbstractTree
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _enemySearchRadius = 10f;

    private BaseAttack _attack;
    private Enemy _unit;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _attack = GetComponent<BaseAttack>();
        _unit = GetComponent<Enemy>();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override Node SetupTree()
    {
        Node node = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                    {
                        new CheckTargetInAttackRange(_unit),
                        new TaskAttack(_unit)
                    }),
                new Sequence(new List<Node>
                    {
                        new CheckTargetinFOVRange(_unit),
                        new TaskGoToTarget(_unit)
                    }),
                new TaskPatrol(_unit)
            });

        return node;
    }
}

