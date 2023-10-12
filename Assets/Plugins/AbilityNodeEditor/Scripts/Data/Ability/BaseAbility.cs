using System;
using UnityEngine;

namespace AbilityNodeEditor
{
    [Serializable]
    public class BaseAbility : ScriptableObject
    {
        public string AbilityName { get; internal set; }
        public bool IsUsed { get; set; }
        public bool IsEnable { get; internal set; }

        internal void SetEnable(bool isEnable) =>
            this.IsEnable = isEnable;

        internal void SetName(string abilityName) =>
            AbilityName = abilityName;

        internal void SetUsed(bool isUsed) =>
            this.IsUsed = isUsed;
    }
}
