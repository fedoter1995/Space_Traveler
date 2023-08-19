using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Stats;

namespace SpaceTraveler.GameStructures.Stats.PackedStats
{
    public class PackedDotStats
    {
        public float Chance { get; private set; }
        public int Duration { get; private set; }
        public int Frequency { get; private set; }
        public DamageAttributes Damage { get; private set; }

        public PackedDotStats(DamageAttributes damage, float chance, int frequency, int duration)
        {
            Damage = damage;
            Chance = chance;
            Duration = duration;
            Frequency = frequency;
        }
    }
}
