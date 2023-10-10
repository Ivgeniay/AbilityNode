using UnityEngine;
using UnityEditor;

namespace AbilityNodeEditor
{
    internal class NodeEditorWindow : EditorWindow
    {
        #region Variables
        internal static NodeEditorWindow curWindow;
        internal NodePropertyView propertyView;
        internal NodeWorkView workView;

        internal float viewPercentage = 0.75f; 
        private NodeGraph curGraph { get; set; } = null;
        #endregion

        #region Methods
        internal static void InitEditorWindow()
        {
            curWindow = EditorWindow.GetWindow<NodeEditorWindow>();
            curWindow.titleContent.text = "Ability Node Editor";

            CreateViews();
        }


        private void OnEnable()
        {
            
        }
        private void OnDestroy()
        {
            
        }

        private void Update()
        {
            //propertyView?.UpdateView();
        }

        

        private void OnGUI()
        {
            if (propertyView == null || workView == null)
            {
                CreateViews();
                return;
            }

            Event e = Event.current;
            ProcessEvents(e);

            workView?.UpdateView(
                position,
                new Rect(0f, 0f, viewPercentage, 1f),
                e,
                curGraph);
            propertyView?.UpdateView(
                new Rect(position.width, position.y, position.width, position.height),
                new Rect(viewPercentage, 0f, 1f - viewPercentage, 1f),
                e,
                curGraph);

            Repaint();
        }

        private void ProcessEvents(Event e)
        {
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftArrow)
            {
                if (viewPercentage > 0) viewPercentage -= 0.01f;
            }
            else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.RightArrow)
            {
                if (viewPercentage < 1) viewPercentage += 0.01f;
            }
        }
        #endregion

        #region UtilityMethods
        private static void CreateViews()
        {
            if (curWindow != null)
            {
                curWindow.propertyView = new NodePropertyView();
                curWindow.workView = new NodeWorkView();
            }
            else
            {
                curWindow = EditorWindow.GetWindow<NodeEditorWindow>();
            }
        }

        internal void SetGraph(NodeGraph curGraph)
        {
            this.curGraph = curGraph;
            curWindow.propertyView.OnChangeGraphHandler(curGraph);
            curWindow.workView.OnChangeGraphHandler(curGraph);
        }

        internal void UnloadGraph() => this.curGraph = null;
        
        #endregion
    }
}
