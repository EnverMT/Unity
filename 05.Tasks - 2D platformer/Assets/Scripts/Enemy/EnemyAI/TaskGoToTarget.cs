using BehaviorTree;
using UnityEngine;

public class TaskGoToTarget : Node
{
    private readonly float _minDistance;
    private readonly Rigidbody2D _rb;
    private readonly float _speed;

    public TaskGoToTarget(Rigidbody2D rb, float speed, float minDistance)
    {
        _minDistance = minDistance;
        _rb = rb;
        _speed = speed;
    }

    public override NodeState Evaluate()
    {
        BaseUnit target = GetData(Data.TARGET) as BaseUnit;

        if (Vector2.Distance(target.transform.position, _rb.position) > _minDistance)
        {
            Vector2 direction = (target.transform.position - _rb.transform.position).normalized;
            _rb.velocity = new Vector2(Mathf.Sign(direction.x) * _speed, _rb.velocity.y);
        }

        state = NodeState.RUNNING;
        return state;
    }
}