using System;
using UnityEngine;
using static AbilityNodeEditor.BaseNode;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AbilityNodeEditor
{
    [Serializable]
    public class AbilityNode : BaseNode
    {
        internal float nodeSum;

        internal NodeOutput Output;
        internal NodeInput InputA;
        internal NodeInput InputB;

        internal AbilityNode()
        {
            Output = new();
            InputA = new();
            InputB = new();
        }

        internal override void InitNode()
        {
            base.InitNode();
            NodeType = NodeType.AbilityNode;
            NodeRect = new Rect(10f, 10f, 150f, 65f);
        }

        internal override void DrawNodeProperties()
        {
            base.DrawNodeProperties();
            nodeSum = EditorGUILayout.FloatField("Float value: ", nodeSum);
        }

        internal override void UpdateNode(Event e, Rect viewRect)
        {
            base.UpdateNode(e, viewRect);
        }

        internal override NodeOutput GetNodeOutput() => Output;

#if UNITY_EDITOR
        internal override void UpdateGraphGUI(Event e, Rect viewRect, GUISkin viewSkin)
        {
            base.UpdateGraphGUI(e, viewRect, viewSkin);

            Output.Position = new Vector3(NodeRect.x + NodeRect.width, NodeRect.y + NodeRect.height / 2 - 12f, 0);
            Output.PointConnection = new Vector3(Output.Position.x + 24, Output.Position.y + 12, 0);

            if (GUI.Button(
                new Rect(Output.Position.x, Output.Position.y, 24f, 24f), 
                "", 
                viewSkin.GetStyle("NodeOutput")))
            {
                if (parentGraph)
                {
                    parentGraph.WantsConnection = true;
                    parentGraph.ConnectionNode = this;
                }
            }

            InputA.Position = new Vector3(NodeRect.x - 24f, NodeRect.y + NodeRect.height / 3 - 12f, 0f);
            InputA.PointConnection = new Vector3(InputA.Position.x, InputA.Position.y + 12, 0);

            if (GUI.Button(
                new Rect(InputA.Position.x, InputA.Position.y, 24f, 24f),
                "",
                viewSkin.GetStyle("NodeInput")))
            {
                if (parentGraph)
                { 
                    OccupiedNode(InputA);
                    parentGraph.WantsConnection = false;
                    parentGraph.ConnectionNode = null;
                }
            }

            InputB.Position = new Vector3(NodeRect.x - 24f, NodeRect.y + (((NodeRect.height / 3) * 2) - 12f), 0f);
            InputB.PointConnection = new Vector3(InputB.Position.x, InputB.Position.y + 12, 0);

            if (GUI.Button(
                new Rect(InputB.Position.x, InputB.Position.y, 24f, 24f),
                "",
                viewSkin.GetStyle("NodeInput")))
            {
                if (parentGraph)
                {
                    OccupiedNode(InputB);
                    parentGraph.WantsConnection = false;
                    parentGraph.ConnectionNode = null;
                }
            }

            DrawInputLines();
        } 

        private void OccupiedNode(NodeInput nodeInput)
        {
            nodeInput.InputNode = parentGraph.ConnectionNode;
            nodeInput.IsOccupied = nodeInput.InputNode != null ? true : false;
        }

        protected override void DisplaySkin(GUISkin viewSkin)
        {
            if (isSelected) GUI.Box(NodeRect, NodeName, viewSkin.GetStyle("NodeSelected"));
            else GUI.Box(NodeRect, NodeName, viewSkin.GetStyle("NodeDefault"));
        }

        private void DrawInputLines()
        {
            if (InputA.IsOccupied && InputA.InputNode != null)
            {
                var nodeOutput = InputA.InputNode.GetNodeOutput();
                if (nodeOutput != null) NodeUtils.DrawLineBetween(InputA, nodeOutput);
            }
            if (InputB.IsOccupied && InputB.InputNode != null)
            {
                var nodeOutput = InputB.InputNode.GetNodeOutput();
                if (nodeOutput != null) NodeUtils.DrawLineBetween(InputB, nodeOutput);
            }
        }
        
#endif

    }
}
