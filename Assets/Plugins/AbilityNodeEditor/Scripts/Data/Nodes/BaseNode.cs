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
        [field: SerializeField] public BaseAbility Ability { get; internal set; }
        [SerializeField] internal string NodeName;
        [SerializeField] internal Rect NodeRect;
        internal AbilityNodeGraph parentGraph;
        internal NodeType NodeType { get; set; }

        protected GUISkin modeSkin;

        [SerializeField] public bool IsEnable;
        [SerializeField] public bool IsUsed;
        [SerializeField] protected bool isSelected;
        [SerializeField] internal NodeOutput Output;

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
            var abilityName = NodeUtils.DrawTextProperty("AbilityName: ", ref NodeName);
            var isUsed = NodeUtils.DrawBoolProperty("IsUsed", ref IsUsed);
            Ability = EditorGUILayout.ObjectField("Ability: ", Ability, typeof(BaseAbility)) as BaseAbility;

            if (Ability != null)
            {
                Ability?.SetName(abilityName);
                Ability?.SetUsed(isUsed);
            }
        }
#endif
        private void ProcessEvent(Event e, Rect viewRect) 
        {
            if (!isSelected) return;
            if (e.type == EventType.MouseDrag && e.button == 0 && viewRect.Contains(e.mousePosition))
            { 
                NodeRect.x += e.delta.x;
                NodeRect.y += e.delta.y;
            }
        }
    }
}
