using SpaceTraveler.GameStructures.Stats;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Hits
{
    public abstract class TakeHitHandler
    {
        
        public abstract event Action OnTakeHitEvent;
        public abstract void TakeHit(HitStats hit);
    }
}
