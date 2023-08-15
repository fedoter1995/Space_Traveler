using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    public interface IHaveShootStats
    {
        HitStats GetHitStats();
        ShotStats GetShotStats(Vector3 dirrection);
    }
}
