using System;
using SpaceTraveler.GameStructures.Stats;

namespace SpaceTraveler.GameStructures.Hits
{
    public interface ITakeDamage : ITakeHit
    {
        event Action<object, DamageAttributes> OnTakeDamageEvent;
        void TakeDamage(object sender, DamageAttributes damage);
    }
    public struct TakeDamageMessage
    {
        private object sender;
        private DamageAttributes dmg;

        public TakeDamageMessage(object sender, DamageAttributes dmg)
        {
            this.sender = sender;
            this.dmg = dmg;
        }

        public override string ToString()
        {
            return $"{sender} take {dmg.Value} {dmg.Type} damage";
        }
    }
}

