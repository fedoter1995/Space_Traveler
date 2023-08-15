using UnityEngine;


namespace SpaceTraveler.GameStructures.Stats.Presets
{
    [CreateAssetMenu(menuName = "Stats/New_Chance_Preset")]
    public class ChancePreset : StatPreset
    {
        [SerializeField]
        private EffectStatPreset _effectRef;
        public EffectStatPreset EffectRef => _effectRef;
    }
}
