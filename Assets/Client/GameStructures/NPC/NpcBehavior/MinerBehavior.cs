using GameStructures.NPC.Miners;
using GameStructures.Zones;
using System;
using System.Collections;

namespace GameStructures.NPC.NpcBehavior
{
    public class MinerBehavior : INpcBehavior
    {
        private Miner miner;

        public MinerBehavior(Miner miner)
        {
            this.miner = miner;
        }
        public IEnumerator ActionRoutine()
        {
            yield return null;
        }
    }
}
