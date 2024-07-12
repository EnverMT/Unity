using BehaviorTree;


public class TaskAttack : Node
{
    public override NodeState Evaluate()
    {
        if (Context.target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Context.unit.BaseMovement.Stop();
        Context.unit.Attack.DealDamage(Context.target);

        if (!Context.target.Health.IsAlive)
            Context.target = null;

        state = NodeState.RUNNING;
        return state;
    }
}