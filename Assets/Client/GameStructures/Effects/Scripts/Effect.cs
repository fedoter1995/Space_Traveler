using System;

namespace SpaceTraveler.GameStructures.Effects
{
    [Serializable]
    public abstract class Effect
    {
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
