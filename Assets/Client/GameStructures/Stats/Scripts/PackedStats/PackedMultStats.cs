using SpaceTraveler.GameStructures.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Client.GameStructures.Stats.PackedStats
{

    public class PackedMultStats
    {
        public float Chance { get; private set; }
        public float Multiplier { get; private set; }
        public DamageType DamageType { get; private set; }

        public PackedMultStats(float chance, float multiplier, DamageType damageType)
        {
            Chance = chance;
            Multiplier = multiplier;
            DamageType = damageType;
        }
    }
}
