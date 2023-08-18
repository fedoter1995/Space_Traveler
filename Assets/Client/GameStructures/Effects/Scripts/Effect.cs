using Assets.Client.GameStructures.Stats.Scripts.Presets;
using SpaceTraveler.GameStructures.Stats;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Effects
{
    [Serializable]
    public abstract class Effect 
    {
        [SerializeField]
        private EffectChancePreset _effectChanceRef;

        public EffectChancePreset ChanceRef => _effectChanceRef;
        public abstract void OnEffectAplly();
    }
    public abstract class TriggeredEffectStats
    {
        
        protected object sender;
        public object Sender => sender;

        public TriggeredEffectStats(object sender)
        {
            this.sender = sender;
        }
    }
}
