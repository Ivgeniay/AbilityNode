using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AbilityNodeEditor
{
    public class NodeMenus
    {
        [MenuItem("DES/Launch Ability Editor")]
        public static void InitNodeEditor()
        {
            NodeEditorWindow.InitEditorWindow();
        }


    }

}
