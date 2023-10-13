using AbilityNodeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComponent : MonoBehaviour
{
    [SerializeField] private AbilityNodeGraph nodeGraph;
    void Start()
    {
        //Get root node
        BaseNode rootNode = nodeGraph.GetRootAbility();

        //Get ability from node
        BaseAbility rootability = rootNode.Ability;

        //Get all abilities that are marked as IsUsed in graph
        IEnumerable<BaseAbility> accessAbility = nodeGraph.GetAbilities();

        //Get all connections to BaseNodes that are connected to the root node
        var connectins = rootNode.GetNodeOutput().GetConnectedNodes();
    }


}
