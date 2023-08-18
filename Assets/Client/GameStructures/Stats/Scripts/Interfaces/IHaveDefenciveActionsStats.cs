using SpaceTraveler.GameStructures.Stats.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTraveler.GameStructures.Stats
{
    public interface IHaveDefenciveActionsStats
    {
        public List<ActionChance> GetDefenciveActionStats();
    }
}
