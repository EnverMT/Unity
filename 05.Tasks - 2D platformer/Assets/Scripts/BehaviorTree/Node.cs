using Assets.Scripts.BehaviorTree;
using System.Collections.Generic;

namespace BehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        public Node Parent;

        protected NodeState state;
        protected List<Node> children = new();
        protected Context context;

        public Node()
        {
            Parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                child.Parent = this;
                children.Add(child);
            }
        }

        public virtual void SetContext(Context context)
        {
            this.context = context;

            foreach (Node node in children)
                node.SetContext(context);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;
    }
}