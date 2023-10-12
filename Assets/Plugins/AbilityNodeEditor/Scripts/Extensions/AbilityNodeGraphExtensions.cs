using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilityNodeEditor
{
    public static class AbilityNodeGraphExtensions
    {
        public static IEnumerable<BaseNode> GetNodes(this AbilityNodeGraph abilityGraph)
        {
            foreach (var node in abilityGraph.Nodes)
                yield return node;
        }

        public static BaseNode GetRootAbility(this AbilityNodeGraph abilityGraph) =>
            abilityGraph.Nodes.OfType<RootAbilityNode>().FirstOrDefault();

        //public static BaseNode GetRootAbility(this AbilityNodeGraph abilityGraph, BaseAbility baseAbility)
        
    }
}
