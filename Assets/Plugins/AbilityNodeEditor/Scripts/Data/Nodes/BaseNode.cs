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
        public BaseAbility Ability { get; set; }
        internal string NodeName;
        internal Rect NodeRect;
        internal NodeGraph parentGraph;
        internal NodeType NodeType { get; set; }

        protected GUISkin modeSkin;

        [SerializeField] protected bool isSelected;

        internal virtual NodeOutput GetNodeOutput() => null;
        internal virtual NodeInput GetNodeInput() => null;

        internal virtual void InitNode() 
        { 
            
        }

        internal virtual void UpdateNode(Event e, Rect viewRect)
        {
            ProcessEvent(e, viewRect);
        }

        internal virtual void SelectNode(bool isSelect = true)
        {
            isSelected = isSelect;
            //Debug.Log(this.name + $" is selected: {isSelect}");
        }

        protected abstract void DisplaySkin(GUISkin viewSkin);

#if UNITY_EDITOR
        internal virtual void UpdateGraphGUI(Event e, Rect viewRect, GUISkin viewSkin) 
        {
            ProcessEvent(e, viewRect);
            DisplaySkin(viewSkin);
             
            EditorUtility.SetDirty(this);
        }

        internal virtual void DrawNodeProperties()
        {

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
        internal class NodeInput
        {
            [field: SerializeField] internal bool IsOccupied { get; set; }
            [field: SerializeField] internal BaseNode InputNode { get; set; } 
            internal Vector3 Position { get; set; }
            internal Vector3 PointConnection { get; set; }
        }

        [Serializable]
        internal class NodeOutput
        {
            [field: SerializeField] internal bool IsOccupied;
            internal Vector3 Position { get; set; }
            internal Vector3 PointConnection { get; set; }
        }
    }
}
