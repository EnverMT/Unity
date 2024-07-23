using BehaviorTree;

namespace Platformer.Enemy.AI
{
    public class TaskPatrol : Node
    {
        public override NodeState Evaluate()
        {
            Context.unit.Patrol.StartPatrol();

            state = NodeState.RUNNING;
            return state;
        }
    }

}