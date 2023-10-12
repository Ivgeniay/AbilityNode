using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AbilityNodeEditor
{
    [Serializable]
    public class NodeAbilityGraph : ScriptableObject
    {
        [field: SerializeField]internal string GraphName { get; set; } = "New Graph";
        internal List<BaseNode> Nodes;
        internal BaseNode SelectedNode {  get; private set; }
        
        internal bool WantsConnection = false;
        internal BaseNode ConnectionNode = null;

        public IEnumerable<BaseNode> GetNodes()
        {
            foreach (var node in Nodes)
                yield return node;
        }
        public RootAbilityNode GetRootAbility() =>
            Nodes.OfType<RootAbilityNode>().FirstOrDefault();
        

        private void OnEnable()
        {
            if (Nodes == null) Nodes = new();
        }

        internal void InitGraph()
        {
            if (Nodes.Count > 0)
            {
                Nodes.ForEach(n =>
                {
                    n.InitNode();
                });
            }
        }
        internal void UpdateGraph()
        {
        }

#if UNITY_EDITOR
        internal void UpdateGraphGUI(Event e, Rect viewRect, GUISkin viewSkin) 
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

        internal void RemoveNode(BaseNode node)
        {
            if (Nodes.Contains(node))
            {
                Nodes.Remove(node);
                GameObject.DestroyImmediate(node);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
        internal void AddNode(BaseNode node)
        {
            if(!Nodes.Contains(node))
            {
                Nodes.Add(node);
                node.name = node.NodeName;
                AssetDatabase.AddObjectToAsset(node, this);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
        internal bool IsThereRootNode()
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
