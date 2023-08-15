using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Zones;
using System;

namespace SpaceTraveler.GameStructures.Hits
{
    public interface ITakeHit : ITriggerObject
    {
        event Action OnTakeHitEvent;
        IHaveTakeHitHandler Obj { get; }

        void TakeHit(object sender, Hit hit);
    }
    public struct TakeHitMessage
    {
        private object sender;
        private HitStats stats;

        public TakeHitMessage(object sender, HitStats stats)
        {
            this.sender = sender;
            this.stats = stats;
        }
        public override string ToString()
        {
            return $"{sender} take {stats} damage";
        }
    }
}
