using BehaviorTree;

public class TaskPatrol : Node
{
    public override NodeState Evaluate()
    {
        Context.unit.Patrol.StartPatrol();

        state = NodeState.RUNNING;
        return state;
    }
}
