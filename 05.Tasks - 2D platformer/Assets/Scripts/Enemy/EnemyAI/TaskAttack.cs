using BehaviorTree;


public class TaskAttack : Node
{
    public override NodeState Evaluate()
    {
        if (context.target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        context.unit.BaseMovement.Stop();
        context.unit.Attack.DealDamage(context.target);

        if (!context.target.Health.IsAlive)
            context.target = null;

        state = NodeState.RUNNING;
        return state;
    }
}