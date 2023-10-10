using DG.DemiEditor.DeGUINodeSystem;
using System;
using System.Threading;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace AbilityNodeEditor
{
    public class NodeWorkView : BaseView
    {
        private Vector2 mousePos;
        public NodeWorkView() : base("WorkNode View") { }
        private NodeGraph nodeGraph;

        public override void UpdateView(Rect editorRect, Rect precentageRect, Event e, NodeGraph nodeGraph)
        {
            base.UpdateView(editorRect, precentageRect, e, nodeGraph);
            this.nodeGraph = nodeGraph;

            GUI.Box(ViewRect, ViewTitle, viewSkin.GetStyle("ProperyViewBG")); 

            GUILayout.BeginArea(ViewRect); 
            curGraph?.UpdateGraphGUI(e, ViewRect, viewSkin);
            GUILayout.EndArea();

            ProcessEvents(e);
        }

        public override void ProcessEvents(Event e)
        {
            base.ProcessEvents(e);

            if (ViewRect.Contains(e.mousePosition))
            {
                if (e.button == 0)
                {
                    if (e.type == EventType.MouseDown) {} 
                    if (e.type == EventType.MouseDrag) {} 
                    if (e.type == EventType.MouseUp) {}
                }
                if (e.button == 1)
                {
                    if (e.type == EventType.MouseDown)
                    {
                        mousePos = e.mousePosition;
                        ProcessContextMenu(e);
                    }
                }
            }
        }

        private void ProcessContextMenu(Event e)
        {
            GenericMenu menu = new();
            menu.AddItem(new GUIContent("Create Grath"), false, ContextCallback, "0");
            menu.AddItem(new GUIContent("Load Grath"), false, ContextCallback, "1");

            if (curGraph != null)
            {
                menu.AddSeparator("");
                var isThereRoot = nodeGraph.IsThereRootNode();
                if(!isThereRoot) menu.AddItem(new GUIContent("Create Root Ability"), false, ContextCallback, "2");
                if(isThereRoot) menu.AddItem(new GUIContent("Create New Ability"), false, ContextCallback, "3");
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Unload Grath"), false, ContextCallback, "10");
            }

            menu.ShowAsContext();
            e.Use();
        }

        private void ContextCallback(object obj)
        {
            if (obj is not string response) return;
            switch (response)
            {
                case "0": 
                    NodePopupWindow.IniNodePopup();
                    break;

                case "1":
                    NodeUtils.LoadGraph();
                    break;
                
                case "2":
                    NodeUtils.CreateNode(curGraph, NodeType.RootAbilityNode, mousePos);
                    break;

                case "3":
                    NodeUtils.CreateNode(curGraph, NodeType.AbilityNode, mousePos);
                    break;

                case "10":
                    NodeUtils.UnloadGraph();
                    break;

                default:
                    Debug.Log("Invalid operation");
                    break;
            }
        }

    }
}
