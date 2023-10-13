using UnityEngine;
using System;

#if UNITY_EDITOR
#endif

namespace AbilityNodeEditor
{
    [Serializable]
    public class NodeOutput : NodeConnection
    {
        public NodeOutput(BaseNode ParentNode) : base(ParentNode) { }

        internal override void AddConnectedNode(BaseNode baseNode) {
            base.AddConnectedNode(baseNode);
            if (!ConnectedNodes.Contains(baseNode))
                ConnectedNodes.Add(baseNode);
        }

        internal override void RemoveConnectedNode(BaseNode baseNode) {
            base.RemoveConnectedNode(baseNode);

            if (ConnectedNodes.Contains(baseNode))
            {
                var targetInputs = baseNode.GetNodeInput();

                int counter = 0; 
                targetInputs.ForEach(el =>
                {
                    if (el.ConnectedNode == ParentNode) counter++;
                });
                if (counter == 1)
                    ConnectedNodes.Remove(baseNode);
            }
        }
    }
}
