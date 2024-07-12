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

        private Context _context;

        public Context Context
        {
            get
            {
                if (Parent == null)
                    return _context;

                return Parent.Context;
            }
            set
            {
                _context = value;
            }
        }

        protected NodeState state;
        protected List<Node> children = new();

        public Node()
        {
            Parent = null;
        }

        public Node(List<Node> nodes)
        {
            foreach (Node node in nodes)
            {
                node.Parent = this;
                children.Add(node);
            }
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;
    }
}