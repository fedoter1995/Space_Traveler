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
        private object recipient;
        private DamageAttributes dmg;

        public TakeDamageMessage(object sender, object recipient, DamageAttributes dmg)
        {
            this.sender = sender;
            this.recipient = recipient;
            this.dmg = dmg;
        }

        public override string ToString()
        {
            return $"{recipient} take {dmg.Value} {dmg.Type} damage from {sender}";
        }
    }
}

