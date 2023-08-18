using SpaceTraveler.GameStructures.Actions;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats.Presets
{
    [CreateAssetMenu(menuName = "Stats/Chances/ActionChancePreset")]
    public class ActionChancePreset : ChancePreset
    {
        [SerializeField]
        private ActionType _actionType;

        public ActionType ActionType => _actionType;
    }
}
