using BehaviorTree;
using UnityEngine;

public class TaskGoToTarget : Node
{
    private const float MinDistance = 0.1f;

    private readonly Rigidbody2D _rb;
    private readonly float _speed;

    public TaskGoToTarget(Rigidbody2D rb, float speed)
    {
        _rb = rb;
        _speed = speed;
    }

    public override NodeState Evaluate()
    {
        CapsuleCollider2D target = (CapsuleCollider2D)GetData(Data.TARGET);

        if (Vector2.Distance(target.transform.position, _rb.position) > MinDistance)
        {
            Vector2 direction = (target.transform.position - _rb.transform.position).normalized;
            _rb.velocity = new Vector2(Mathf.Sign(direction.x) * _speed, _rb.velocity.y);
        }

        state = NodeState.RUNNING;
        return state;
    }
}