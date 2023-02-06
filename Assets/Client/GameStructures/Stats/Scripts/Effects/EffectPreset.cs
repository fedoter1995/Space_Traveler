using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Stats
{
    [CreateAssetMenu(menuName = "Stats/Presets/New_Effect_Preset")]
    public class EffectPreset : ScriptableObject
    {
        [SerializeField]
        private string _name = "New Effect";
        [SerializeField]
        private EffectType _type = EffectType.Permanent;

        public string Name => _name;
        public EffectType Type => _type;
    }
}

