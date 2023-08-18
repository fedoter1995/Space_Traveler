﻿using SpaceTraveler.GameStructures.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTraveler.GameStructures.Stats.Chances
{
    public abstract class Chance : BaseStat
    {
        public abstract void GetSuitableStats(StatsHandler handler);
    }
}