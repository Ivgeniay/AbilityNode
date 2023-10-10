using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AbilityNodeEditor
{
    public class NodePropertyView : BaseView
    {
        public NodePropertyView() : base("Property View") { }
        public override void UpdateView(Rect editorRect, Rect precentageRect, Event e, NodeGraph nodeGraph)
        {
            base.UpdateView(editorRect, precentageRect, e, nodeGraph);

            GUI.Box(ViewRect, ViewTitle, viewSkin.GetStyle("ProperyViewBG"));

            GUILayout.BeginArea(ViewRect);
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
                    if (e.type == EventType.MouseDown)
                    {
                        Debug.Log("Left click in " + ViewTitle);
                    }

                    if (e.type == EventType.MouseDrag)
                    {
                        Debug.Log("Mouse drag in " + ViewTitle);
                    }

                    if (e.type == EventType.MouseUp)
                    {
                        Debug.Log("Mouse up in " + ViewTitle);
                    }
                }
                if (e.button == 1)
                {
                    if (e.type == EventType.MouseDown)
                    {
                        Debug.Log("Right click in " + ViewTitle);
                    }
                }
            }
        }
    }
}
