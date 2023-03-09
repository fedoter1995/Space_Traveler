using System;
using UnityEngine;

namespace GameStructures.Stats
{
    [CreateAssetMenu(menuName = "Stats/Presets/New_Resistance_Preset")]
    public class ResistancePreset : StatPreset
    {
        [SerializeField]
        private DamageType _type;
        public DamageType Type => _type;
    }
}
