using Assets.Scripts.BehaviorTree;
using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class SkeletBT : AbstractTree
{
    private Enemy _unit;
    private Context _treeContext;

    private void Awake()
    {
        _unit = GetComponent<Enemy>();
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

        node.SetContext(_treeContext);

        return node;
    }
}