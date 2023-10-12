using UnityEngine;
using System;

#if UNITY_EDITOR
#endif

namespace AbilityNodeEditor
{
    [Serializable]
    internal class NodeOutput : NodeConnection
    {
        public NodeOutput(BaseNode ParentNode) : base(ParentNode) { }
    }
}
