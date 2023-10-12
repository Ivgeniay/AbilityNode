using UnityEngine;
using System;

#if UNITY_EDITOR
#endif

namespace AbilityNodeEditor
{
    public abstract partial class BaseNode
    {
        [Serializable]
        internal class NodeOutput
        {
            [field: SerializeField] internal bool IsOccupied;
            internal Vector3 Position { get; set; }
            internal Vector3 PointConnection { get; set; }
        }
    }
}
