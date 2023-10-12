using UnityEngine;
using System;

#if UNITY_EDITOR
#endif

namespace AbilityNodeEditor
{
    public abstract partial class BaseNode
    {
        [Serializable]
        internal class NodeInput
        {
            [field: SerializeField] internal bool IsOccupied { get; set; }
            [field: SerializeField] internal BaseNode InputNode { get; set; } 
            internal Vector3 Position { get; set; }
            internal Vector3 PointConnection { get; set; }
        }
    }
}
