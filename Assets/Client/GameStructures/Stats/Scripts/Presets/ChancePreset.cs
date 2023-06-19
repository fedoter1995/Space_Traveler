using GameStructures.Effects;
using UnityEngine;


namespace GameStructures.Stats
{
    [CreateAssetMenu(menuName = "Stats/New_Chance_Preset")]
    public class ChancePreset : StatPreset
    {
        [SerializeField]
        private EffectStatPreset _effectRef;
        public EffectStatPreset EffectRef => _effectRef;
    }
}
