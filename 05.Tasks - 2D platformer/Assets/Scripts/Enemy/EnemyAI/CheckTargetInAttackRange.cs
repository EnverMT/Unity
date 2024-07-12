using BehaviorTree;

public class CheckTargetInAttackRange : Node
{
    private readonly BaseUnit _baseUnit;

    public CheckTargetInAttackRange(BaseUnit baseUnit)
    {
        _baseUnit = baseUnit;
    }

    public override NodeState Evaluate()
    {
        if (context.target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        if (_baseUnit.Attack.IsInAttackRange(context.target))
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
