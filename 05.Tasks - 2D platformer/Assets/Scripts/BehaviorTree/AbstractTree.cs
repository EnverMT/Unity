using UnityEngine;

namespace BehaviorTree
{
    public abstract class AbstractTree : MonoBehaviour
    {
        private Node _root;

        protected void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            _root?.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}
