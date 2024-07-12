using Assets.Scripts.BehaviorTree;
using BehaviorTree;
using System.Collections.Generic;

//[RequireComponent(typeof(Enemy))]
public class SkeletBT : AbstractTree
{
    private Context _treeContext;

    private void Awake()
    {
        _treeContext.unit = GetComponent<Enemy>();
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

        node.SetContext(_treeContext);

        return node;
    }
}