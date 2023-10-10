using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AbilityNodeEditor
{
    [Serializable]
    public class NodeGraph : ScriptableObject
    {
        [field: SerializeField]public string GraphName { get; set; } = "New Graph";
        public List<BaseNode> Nodes;
        public BaseNode SelectedNode {  get; private set; }
        
        public bool WantsConnection = false;
        public BaseNode ConnectionNode = null;

        private void OnEnable()
        {
            if (Nodes == null) Nodes = new();
        }

        public void InitGraph()
        {
            if (Nodes.Count > 0)
            {
                Nodes.ForEach(n =>
                {
                    n.InitNode();
                });
            }
        }
        public void UpdateGraph()
        {
        }

#if UNITY_EDITOR
        public void UpdateGraphGUI(Event e, Rect viewRect, GUISkin viewSkin) 
        {
            if (Nodes.Count > 0)
            {
                ProcessEvent(e, viewRect);
                Nodes.ForEach(node =>
                {
                    node.UpdateGraphGUI(e, viewRect, viewSkin);
                });
            }

            if (WantsConnection)
            {
                if (ConnectionNode != null)
                {
                    DrawConnectionToMouse(e.mousePosition);
                }
            }

            EditorUtility.SetDirty(this);
        }

        private void DrawConnectionToMouse(Vector2 mousePosition)
        {
            Handles.BeginGUI();
            Handles.color = Color.white;
            Handles.DrawLine(new Vector3(ConnectionNode.NodeRect.x + ConnectionNode.NodeRect.width + 24f,
                ConnectionNode.NodeRect.y + ConnectionNode.NodeRect.height * 0.5f, 
                0f),
                new Vector3(mousePosition.x, mousePosition.y, 0));
            Handles.EndGUI();
        }
#endif
        private void ProcessEvent(Event e, Rect viewRect)
        {
            if (viewRect.Contains(e.mousePosition))
            {
                if (e.button == 0)
                {
                    if (e.type == EventType.MouseDown)
                    {
                        DeselectAllNodes(); 
                        bool setNode = false;
                        SelectedNode = null;
                        foreach (var node in Nodes)
                        {
                            if (node.NodeRect.Contains(e.mousePosition))
                            {
                                node.SelectNode(true);
                                SelectedNode = node;
                                setNode = true;
                                break;
                            } 
                        }

                        if (!setNode)
                        {
                            DeselectAllNodes();
                            //WantsConnection = false;
                            //ConnectionNode = null;
                        }

                        if (WantsConnection)
                        {
                            WantsConnection = false;
                        }
                    }
                }
            }
        }

        public void RemoveNode(BaseNode node)
        {
            if (Nodes.Contains(node))
                Nodes.Remove(node);
        }
        public void AddNode(BaseNode node)
        {
            if(!Nodes.Contains(node))
                Nodes.Add(node); 
        }
        public bool IsThereRootNode()
        {
            BaseNode node = null;
            Nodes.ForEach(el =>
            {
                if (el is RootAbilityNode root) node = root;
            });
            return node != null;
        }

        private void DeselectAllNodes()
        {
            Nodes.ForEach(node =>
            {
                node.SelectNode(false);
            });
        }
    }
}
