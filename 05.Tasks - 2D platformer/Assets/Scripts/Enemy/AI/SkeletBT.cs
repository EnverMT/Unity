using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Enemy.AI
{
    [RequireComponent(typeof(EnemyUnit))]
    [RequireComponent(typeof(Patrol))]
    public class SkeletBT : AbstractTree
    {
        private readonly Context _context = new();

        private void Awake()
        {
            _context.unit = GetComponent<EnemyUnit>();
        }

        protected override Node SetupTree()
        {
            Node node = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                    {
                        new CheckTargetInAttackRange(),
                        new TaskAttack()
                    }),
                new Sequence(new List<Node>
                    {
                        new CheckTargetinFOVRange(),
                        new TaskGoToTarget()
                    }),
                new TaskPatrol()
            });

            node.Context = _context;

            return node;
        }
    }
}