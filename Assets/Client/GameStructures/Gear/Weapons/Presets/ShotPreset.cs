using CustomTools;
using GameStructures.Stats;
using UnityEngine;

namespace GameStructures.Gear.Weapons
{
    public abstract class ShotPreset : ScriptableObject
    {
        public abstract void Shot(ShotStats shotStats, HitStats hitStats, Pool<Projectile> projectilePool);
    }
}
