using CustomTools;
using GameStructures.Hits;
using GameStructures.Stats;
using UnityEngine;

namespace GameStructures.Gear.Weapons
{
    public abstract class ShotPreset : ScriptableObject
    {
        public abstract void Shot(object sender, ShotStats shotStats, HitStats hitStats, Pool<Projectile> projectilePool);
    }
}
