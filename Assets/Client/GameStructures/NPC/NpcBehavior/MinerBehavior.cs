using SpaceTraveler.GameStructures.NPC.Miners;
using System.Collections;

namespace SpaceTraveler.GameStructures.NPC.NpcBehavior
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
