using System.Collections.Generic;

namespace BehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public enum Data
    {
        TARGET
    }

    public class Node
    {
        public Node Parent;

        protected NodeState state;
        protected List<Node> children = new();

        private Dictionary<Data, object> _dataContext = new();

        public Node()
        {
            Parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
                Attach(child);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(Data key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(Data key)
        {
            object value = null;

            if (_dataContext.TryGetValue(key, out value))
                return value;

            Node node = Parent;

            while (node != null)
            {
                value = node.GetData(key);

                if (value != null)
                    return value;

                node = node.Parent;
            }

            return value;
        }

        public bool ClearData(Data key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = Parent;

            while (node != null)
            {
                if (node.ClearData(key))
                    return true;

                node = node.Parent;
            }

            return false;
        }

        private void Attach(Node node)
        {
            node.Parent = this;
            children.Add(node);
        }
    }
}

