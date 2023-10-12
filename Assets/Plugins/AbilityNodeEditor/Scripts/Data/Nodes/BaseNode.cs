using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AbilityNodeEditor
{
    [Serializable]
    public abstract partial class BaseNode : ScriptableObject
    {
        public BaseAbility Ability { get; set; }
        internal string NodeName;
        internal Rect NodeRect;
        internal NodeAbilityGraph parentGraph;
        internal NodeType NodeType { get; set; }

        protected GUISkin modeSkin;

        [SerializeField] public bool IsEnable;
        [SerializeField] protected bool isSelected;

        public void SetEnable(bool isEnable) => IsEnable = isEnable;
        internal virtual NodeOutput GetNodeOutput() => null;
        internal virtual List<NodeInput> GetNodeInput() => null;

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
            NodeUtils.DrawTextProperty("AbilityName: ", ref NodeName);
            NodeUtils.DrawBoolProperty("IsEnable", ref IsEnable, () =>
            {
                if (this is RootAbilityNode root) return true;
                var outputNode = GetNodeInput();
                if (outputNode == null || outputNode.Count == 0) return false;

                if (outputNode.Any(input => input != null && input.InputNode != null && input.InputNode.IsEnable)) return true;

                return false;
            });
            Ability = EditorGUILayout.ObjectField("Ability: ", Ability, typeof(BaseAbility)) as BaseAbility;
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
    }
}
