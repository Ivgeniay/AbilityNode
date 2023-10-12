using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityNodeEditor;

#if UNITY_EDITOR
using UnityEditor;
#endif

internal class NodePopupWindow : EditorWindow
{
    private static NodePopupWindow curPopup;
    private string graphName = "New Graph";
    
    internal static void IniNodePopup()
    {
        curPopup = EditorWindow.GetWindow<NodePopupWindow>();
        curPopup.titleContent.text = "Node Popup";
    }

    private void OnGUI()
    {
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);

        GUILayout.BeginVertical();

        EditorGUILayout.LabelField("Create New Graph:", EditorStyles.boldLabel);
        graphName = EditorGUILayout.TextField("Enter Graph Name", graphName);

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Graph", GUILayout.Height(40)))
        {
            if (string.IsNullOrEmpty(graphName) || string.IsNullOrWhiteSpace(graphName) || graphName == "New Graph")
            {
                EditorUtility.DisplayDialog("Node Message:", "Enter valid graph name", "OK");
            }
            else
            {
                NodeUtils.CreateNewGraph(graphName);
                curPopup.Close();
            }
        }
        if (GUILayout.Button("Cancel", GUILayout.Height(40)))
        {
            curPopup.Close();
        }
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.Space(20);
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
    }
}
