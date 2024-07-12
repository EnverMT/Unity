using BehaviorTree;

public class CheckTargetInAttackRange : Node
{
    public override NodeState Evaluate()
    {
        if (Context.target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        if (Context.unit.Attack.IsInAttackRange(Context.target))
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
