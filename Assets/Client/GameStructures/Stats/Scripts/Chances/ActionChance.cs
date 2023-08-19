using SpaceTraveler.GameStructures.Actions;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Stats.Presets;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats.Chances
{
    public class ActionChance : Chance
    {
        [SerializeField]
        private ActionChancePreset _actionChancePreset;
        public ActionChancePreset ActionChancePreset => _actionChancePreset;

        public ActionType ActionType => ActionChancePreset.ActionType;

        public override void Initialize()
        {
            base.Initialize();

            if (statPreset is null)
                statPreset = _actionChancePreset;
            else
                _actionChancePreset = statPreset as ActionChancePreset;


            if (_actionChancePreset == null)
                throw new Exception("StatPreset cannot be represented as a ActionChancePreset or is null");
        }
    }
}
