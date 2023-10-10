using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AbilityNodeEditor
{
    [Serializable]
    public abstract class BaseView 
    {
        public string ViewTitle { get; set; }
        public Rect ViewRect;

        private string defaultViewTitle;
        protected GUISkin viewSkin;
        protected NodeGraph curGraph;

        public BaseView(string title)
        {
            defaultViewTitle = title;
            ViewTitle = title;
            GetEditorSkin();
        }

        public virtual void OnChangeGraphHandler(NodeGraph nodeGraph)
        {
            curGraph = nodeGraph;
            if (curGraph) ViewTitle = curGraph.GraphName + " " + defaultViewTitle; 
            else ViewTitle = "No Graph";
        }

        public virtual void UpdateView(Rect editorRect, Rect precentageRect, Event e, NodeGraph nodeGraph)
        {
            if (viewSkin == null) 
            {
                GetEditorSkin();
                return;
            }

            ViewRect = new Rect(editorRect.x * precentageRect.x,
                                editorRect.y * precentageRect.y, 
                                editorRect.width * precentageRect.width,
                                editorRect.height * precentageRect.height);
        }

        public virtual void ProcessEvents(Event e) { }
        protected void GetEditorSkin() { 
            viewSkin = Resources.Load<GUISkin>("GUISkins/EditorSkins/NodeEditorSkin");
        }

    }
}
