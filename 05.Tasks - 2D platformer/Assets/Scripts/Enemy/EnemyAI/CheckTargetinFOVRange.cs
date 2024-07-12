using BehaviorTree;
using System.Linq;
using UnityEngine;

public class CheckTargetinFOVRange : Node
{
    public override NodeState Evaluate()
    {
        if (context.target == null)
        {
            Player player = context.unit.Patrol.GetUnitsInFOV<Player>().FirstOrDefault();

            if (player != null)
            {
                context.target = player;

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        float distance = Vector2.Distance(context.unit.gameObject.transform.position, context.target.gameObject.transform.position);

        if (!context.unit.Patrol.IsUnitInFOV(context.target))
        {
            context.target = null;

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}

