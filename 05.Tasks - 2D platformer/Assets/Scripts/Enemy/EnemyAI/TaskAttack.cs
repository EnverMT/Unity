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
        BaseUnit target = GetData(Data.TARGET) as BaseUnit;

        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        _unit.Rigidbody2D.velocity = UnityEngine.Vector2.zero;
        _unit.Attack.DealDamage(target);

        if (!target.Health.IsAlive)
            ClearData(Data.TARGET);

        state = NodeState.RUNNING;
        return state;
    }
}