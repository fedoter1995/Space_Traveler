using SpaceTraveler.GameStructures.Stats;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Hits
{
    public abstract class TakeHitHandler : MonoBehaviour
    {
        
        public abstract event Action<HitStats> OnTakeHitEvent;
        public abstract void TakeHit(object sender, HitStats hit);
    }
}
