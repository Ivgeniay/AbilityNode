using UnityEngine;
using System;

#if UNITY_EDITOR
#endif

namespace AbilityNodeEditor
{

    [Serializable]
    public class NodeInput : NodeConnection
    {
        public NodeInput() { }
        public NodeInput(BaseNode ParentNode) : base(ParentNode) { }
        [field: SerializeField] internal BaseNode ConnectedNode { get; set; }

        internal override void AddConnectedNode(BaseNode baseNode)
        {
            base.AddConnectedNode(baseNode);

            if (baseNode == null)
            {
                ConnectedNodes.ForEach(el =>
                {
                    el.GetNodeOutput().RemoveConnectedNode(ParentNode);
                });
                ConnectedNodes.Clear();
            }
            else if (!ConnectedNodes.Contains(baseNode))
            {
                ConnectedNodes.Add(baseNode);
                baseNode.GetNodeOutput().AddConnectedNode(ParentNode);
            }

            ConnectedNode = baseNode;
            //IsOccupied = ConnectedNode != null ? true : false;
        }

        internal override void RemoveConnectedNode(BaseNode baseNode)
        {
            base.RemoveConnectedNode(baseNode);
        }
    } 
}
