using BehaviorTree;
using UnityEngine;

public class CheckTargetInAttackRange : Node
{
    private readonly Animator _animator;
    private readonly Rigidbody2D _rb;
    private readonly float _attackRadius;

    public CheckTargetInAttackRange(Animator animator, Rigidbody2D rigidbody2D, float attackRadius)
    {
        _animator = animator;
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
            _animator.SetBool(AnimatorParams.Params.IsAttacking, true);

            state = NodeState.SUCCESS;
            return state;
        }

        _animator.SetBool(AnimatorParams.Params.IsAttacking, false);

        state = NodeState.FAILURE;
        return state;
    }
}
