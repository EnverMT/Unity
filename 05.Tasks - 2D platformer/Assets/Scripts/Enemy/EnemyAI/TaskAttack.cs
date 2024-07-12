using BehaviorTree;


public class TaskAttack : Node
{
    private readonly BaseUnit _unit;

    public TaskAttack(BaseUnit unit)
    {
        _unit = unit;
    }

    public override NodeState Evaluate()
    {
        if (context.target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        _unit.Rigidbody2D.velocity = UnityEngine.Vector2.zero;
        _unit.Attack.DealDamage(context.target);

        if (!context.target.Health.IsAlive)
            context.target = null;

        state = NodeState.RUNNING;
        return state;
    }
}