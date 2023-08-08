using GameStructures.Hits;
using UnityEngine;

namespace GameStructures.Stats
{
    public interface IHaveShootStats
    {
        HitStats GetHitStats();
        ShotStats GetShotStats(Vector3 dirrection);
    }
}
