using System;
using SpaceTraveler.GameStructures.Stats;

namespace SpaceTraveler.GameStructures.Hits
{
    public interface ITakeDamage
    {
        event Action<object, DamageTypeValue> OnTakeDamageEvent;
        void TakeDamage(object sender, HitDamage damage);
    }
    public struct TakeDamageMessage
    {
        private object sender;
        private DamageTypeValue dmg;

        public TakeDamageMessage(object sender, DamageTypeValue dmg)
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

