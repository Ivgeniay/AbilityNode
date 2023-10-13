using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

#if UNITY_EDITOR
#endif

namespace AbilityNodeEditor
{
    [Serializable]
    public abstract class NodeConnection
    {
        public NodeConnection() { }
        public NodeConnection(BaseNode ParentNode)
        {
            this.ParentNode = ParentNode;
            if (ConnectedNodes == null) ConnectedNodes = new();
        }
        

        [field: SerializeField] internal virtual bool IsOccupied { get =>
                ConnectedNodes.Count > 0; 
        }
        [field: SerializeField] internal List<BaseNode> ConnectedNodes { get; set; }
        internal BaseNode ParentNode { get; set; }
        internal Vector3 Position { get; set; }
        internal Vector3 PointConnection { get; set; }

        public IEnumerable<BaseNode> GetConnectedNodes()
        {
            foreach (var node in ConnectedNodes)
                yield return node;
        }

        internal virtual void AddConnectedNode(BaseNode baseNode) { }
        internal virtual void RemoveConnectedNode(BaseNode baseNode) { }
    }
}
