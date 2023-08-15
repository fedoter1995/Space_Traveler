using UnityEngine;
namespace SpaceTraveler.GameStructures.Stats.Presets
{
    [CreateAssetMenu(menuName = "Stats/Presets/New_Damage_Preset")]
    public class DamagePreset : StatPreset
    {
        [SerializeField]
        private DamageType _type;
        public DamageType Type => _type;
    }
}

