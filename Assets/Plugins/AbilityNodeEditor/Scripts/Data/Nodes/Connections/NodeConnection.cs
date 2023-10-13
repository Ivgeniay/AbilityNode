using UnityEngine;
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
#endif

namespace AbilityNodeEditor
{
    [Serializable]
    public abstract class NodeConnection
    {
        public NodeConnection() { }
        public NodeConnection(BaseNode ParentNode) =>
            this.ParentNode = ParentNode;
        

        [field: SerializeField] internal virtual bool IsOccupied { get => ConnectedNodes.Count > 0; }// set; }
        [field: SerializeField] internal List<BaseNode> ConnectedNodes { get; set; }
        internal BaseNode ParentNode { get; set; }
        internal Vector3 Position { get; set; }
        internal Vector3 PointConnection { get; set; }

        internal virtual void AddConnectedNode(BaseNode baseNode) { }
        internal virtual void RemoveConnectedNode(BaseNode baseNode) { }
    }
}
