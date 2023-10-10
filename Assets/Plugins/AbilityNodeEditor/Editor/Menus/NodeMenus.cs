using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AbilityNodeEditor
{
    internal class NodeMenus
    {
        [MenuItem("DES/Launch Ability Editor")]
        internal static void InitNodeEditor()
        {
            NodeEditorWindow.InitEditorWindow();
        }


    }

}
