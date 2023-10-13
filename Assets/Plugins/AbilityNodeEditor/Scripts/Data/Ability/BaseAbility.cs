using System;
using UnityEngine;

namespace AbilityNodeEditor
{
    [Serializable]
    public class BaseAbility : ScriptableObject
    {
        [field: SerializeField] public string AbilityName { get; internal set; }
        [field: SerializeField] public bool IsUsed { get; set; }
        [field: SerializeField] public bool IsEnable { get; internal set; }

        internal void SetEnable(bool isEnable) =>
            this.IsEnable = isEnable;

        internal void SetName(string abilityName) =>
            AbilityName = abilityName;

        internal void SetUsed(bool isUsed) =>
            this.IsUsed = isUsed;
    }
}
