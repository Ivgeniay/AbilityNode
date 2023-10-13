using System;
using UnityEngine;
using static AbilityNodeEditor.BaseNode;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AbilityNodeEditor
{
    [Serializable]
    public class AbilityNode : BaseNode
    {
        [SerializeField] internal NodeInput InputA;
        [SerializeField] internal NodeInput InputB;

        internal AbilityNode()
        {
            Output = new(this);
            InputA = new(this);
            InputB = new(this);
        }

        public NodeInput GetInputA() => InputA;
        public NodeInput GetInputB() => InputB;

        internal override void InitNode()
        {
            base.InitNode();
            NodeType = NodeType.AbilityNode;
            NodeRect = new Rect(10f, 10f, 150f, 65f);
        }

        internal override void DrawNodeProperties()
        {
            base.DrawNodeProperties(); 

            if ((InputA != null && InputA.ConnectedNode != null && InputA.ConnectedNode.IsUsed) || 
                (InputB != null && InputB.ConnectedNode != null && InputB.ConnectedNode.IsUsed))
                IsEnable = true;
            else
                IsEnable = false;

            SetEnable(IsEnable);
            NodeUtils.DrawBoolProperty("IsEnable", ref IsEnable);
        }

        internal override void UpdateNode(Event e, Rect viewRect)
        {
            base.UpdateNode(e, viewRect);
        }

        public override NodeOutput GetNodeOutput() => Output;
        public override List<NodeInput> GetNodeInput() =>
            new List<NodeInput>() { InputA, InputB };
        

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

        private void OccupiedNode(NodeInput nodeInput) =>
            nodeInput.AddConnectedNode(parentGraph.ConnectionNode);
        

        protected override void DisplaySkin(GUISkin viewSkin)
        {
            if (isSelected) GUI.Box(NodeRect, NodeName, viewSkin.GetStyle("NodeSelected"));
            else GUI.Box(NodeRect, NodeName, viewSkin.GetStyle("NodeDefault"));
        }

        private void DrawInputLines()
        {
            DrawFromInput(InputA);
            DrawFromInput(InputB);
        }

        private void DrawFromInput(NodeInput nodeInput)
        {
            if (nodeInput != null && nodeInput.IsOccupied && nodeInput.ConnectedNode != null)
            {
                var nodeOutput = nodeInput.ConnectedNode.GetNodeOutput();
                if (nodeOutput != null)
                {
                    if (nodeInput.ParentNode.IsUsed && nodeInput.ConnectedNode.IsUsed)
                        NodeUtils.DrawLineBetween(nodeInput, nodeOutput, Color.yellow);
                    else if (nodeInput.ParentNode.IsEnable && nodeInput.ConnectedNode.IsEnable && nodeInput.ConnectedNode.IsUsed)
                        NodeUtils.DrawLineBetween(nodeInput, nodeOutput, Color.white);
                    else
                        NodeUtils.DrawLineBetween(nodeInput, nodeOutput, Color.gray);
                }
            }
        }
#endif

    }
}
