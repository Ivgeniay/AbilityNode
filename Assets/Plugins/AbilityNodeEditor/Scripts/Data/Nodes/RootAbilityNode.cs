using System;
using UnityEngine;
using UnityEngine.Windows;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AbilityNodeEditor
{
    [Serializable]
    public class RootAbilityNode : BaseNode
    {
        public RootAbilityNode() {
            Output = new(this);
        }

        internal override void InitNode()
        {
            base.InitNode();
            NodeType = NodeType.RootAbilityNode;
            NodeRect = new Rect(10f, 10f, 200f, 65f);
        }

        protected override void DisplaySkin(GUISkin viewSkin)
        {
            if (isSelected) GUI.Box(NodeRect, NodeName, viewSkin.GetStyle("NodeSelectedRoot"));
            else GUI.Box(NodeRect, NodeName, viewSkin.GetStyle("NodeDefaultRoot"));
        }
        internal override void DrawNodeProperties()
        {
            base.DrawNodeProperties();

            if (IsUsed) IsEnable = true;
            else IsEnable = false;

            Ability?.SetEnable(IsEnable);
            NodeUtils.DrawBoolProperty("IsEnable", ref IsEnable);
        }

        public override NodeOutput GetNodeOutput() => Output;

#if UNITY_EDITOR
        internal override void UpdateGraphGUI(Event e, Rect viewRect, GUISkin viewSkin)
        {
            base.UpdateGraphGUI(e, viewRect, viewSkin);
            if (viewSkin == null) return;
            if (Output == null) return;

            Output.Position = new Vector3(NodeRect.x + NodeRect.width, NodeRect.y + NodeRect.height / 2 - 12f, 0);
            Output.PointConnection = NodeUtils.GetNodeConnectionPosition(Output.Position, ConnectionNodeSide.Right);

            if (GUI.Button(
                new Rect(Output.Position.x, Output.Position.y, 24f, 24f),
                "",
                viewSkin.GetStyle("NodeOutput")))
            {
                if(parentGraph != null)
                {
                    parentGraph.WantsConnection = true;
                    parentGraph.ConnectionNode = this;
                }
            }
        }
#endif
    }
}
