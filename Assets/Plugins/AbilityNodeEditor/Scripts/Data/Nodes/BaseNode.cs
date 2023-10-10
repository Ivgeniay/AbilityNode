using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AbilityNodeEditor
{
    [Serializable]
    public abstract class BaseNode : ScriptableObject
    {
        public string NodeName;
        public Rect NodeRect;
        public NodeGraph parentGraph;
        public NodeType NodeType { get; set; }

        protected GUISkin modeSkin;

        [SerializeField] protected bool isSelected;

        public virtual NodeOutput GetNodeOutput() => null;
        public virtual NodeInput GetNodeInput() => null;

        public virtual void InitNode() 
        { 
            
        }

        public virtual void UpdateNode(Event e, Rect viewRect)
        {
            ProcessEvent(e, viewRect);
        }

        public virtual void SelectNode(bool isSelect = true)
        {
            isSelected = isSelect;
            //Debug.Log(this.name + $" is selected: {isSelect}");
        }

        protected abstract void DisplaySkin(GUISkin viewSkin);

#if UNITY_EDITOR
        public virtual void UpdateGraphGUI(Event e, Rect viewRect, GUISkin viewSkin) 
        {
            ProcessEvent(e, viewRect);
            DisplaySkin(viewSkin);
             
            EditorUtility.SetDirty(this);
        }
#endif
        private void ProcessEvent(Event e, Rect viewRect) 
        {
            if (!isSelected) return;
            if (e.type == EventType.MouseDrag)
            { 
                NodeRect.x += e.delta.x;
                NodeRect.y += e.delta.y;
            }

        }

        [Serializable]
        public class NodeInput
        {
            [field: SerializeField] public bool IsOccupied { get; set; }
            [field: SerializeField] public BaseNode InputNode { get; set; } 
            public Vector3 Position { get; set; }
            public Vector3 PointConnection { get; set; }
        }

        [Serializable]
        public class NodeOutput
        {
            [field: SerializeField] public bool IsOccupied;
            public Vector3 Position { get; set; }
            public Vector3 PointConnection { get; set; }
        }
    }
}
