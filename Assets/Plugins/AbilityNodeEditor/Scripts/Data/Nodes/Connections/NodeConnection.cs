using UnityEngine;
using System;

#if UNITY_EDITOR
#endif

namespace AbilityNodeEditor
{
    [Serializable]
    internal class NodeConnection
    {
        public NodeConnection() { }
        public NodeConnection(BaseNode ParentNode) { 
            this.ParentNode = ParentNode;
        }

        [field: SerializeField] internal bool IsOccupied { get; set; }
        [field: SerializeField] internal BaseNode ConnectedNode { get; set; }
        internal BaseNode ParentNode { get; set; }
        internal Vector3 Position { get; set; }
        internal Vector3 PointConnection { get; set; }

    }
}
