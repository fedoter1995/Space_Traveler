using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameStructures.Stats
{
    [CreateAssetMenu(menuName = "Stats/Presets/New_Damage_Preset")]
    public class DamagePreset : StatPreset
    {
        [SerializeField]
        private DamageType _type;
        public DamageType Type => _type;
    }
}

