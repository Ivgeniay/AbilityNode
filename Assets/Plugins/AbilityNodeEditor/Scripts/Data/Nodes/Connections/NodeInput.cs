using UnityEngine;
using System;

#if UNITY_EDITOR
#endif

namespace AbilityNodeEditor
{

    [Serializable]
    internal class NodeInput : NodeConnection
    {
        public NodeInput() { }
        public NodeInput(BaseNode ParentNode) : base(ParentNode) { }

        //[field: SerializeField] internal BaseNode ConnectedNode { get; set; } 
    } 
}
