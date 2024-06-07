using BehaviorTree;
using UnityEngine;

public class CheckTargetInAttackRange : Node
{
    private readonly Rigidbody2D _rb;
    private readonly float _attackRadius;

    public CheckTargetInAttackRange(Rigidbody2D rigidbody2D, float attackRadius)
    {
        _rb = rigidbody2D;
        _attackRadius = attackRadius;
    }

    public override NodeState Evaluate()
    {
        CapsuleCollider2D target = GetData(Data.TARGET) as CapsuleCollider2D;

        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        float distance = Vector2.Distance(_rb.gameObject.transform.position, target.gameObject.transform.position);

        if (distance <= _attackRadius)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
