using System.Collections.Generic;

namespace BehaviorTree
{
    public class Selector_Y : Node_Y
    {
        public Selector_Y() : base() { }
        public Selector_Y(List<Node_Y> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node_Y node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }

    }

}
