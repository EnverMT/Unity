using BehaviorTree;
using UnityEngine;

public class CheckTargetinFOVRange : Node
{
    private const int TargetLayer = 1 << 7;

    private readonly Rigidbody2D _rb;
    private readonly float _radius;

    public CheckTargetinFOVRange(Rigidbody2D rigidbody2D, float radius)
    {
        _rb = rigidbody2D;
        _radius = radius;
    }

    public override NodeState Evaluate()
    {
        BaseUnit target = GetData(Data.TARGET) as BaseUnit;

        if (target == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_rb.gameObject.transform.position, _radius, TargetLayer);

            if (colliders.Length > 0)
            {
                Parent.Parent.SetData(Data.TARGET, colliders[0]);

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        float distance = Vector2.Distance(_rb.gameObject.transform.position, target.gameObject.transform.position);

        if (distance > _radius)
        {
            ClearData(Data.TARGET);

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}

