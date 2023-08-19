using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats.Presets
{
    [CreateAssetMenu(menuName = "Stats/New_Multiplier_Preset")]
    public class MultiplierPreset : StatPreset
    {
        [SerializeField]
        private DamageType _damageType = DamageType.Physical;
        public DamageType DamageType => _damageType;
    }
}
