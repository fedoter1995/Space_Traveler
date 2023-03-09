using CustomTools;
using GameStructures.Stats;
using UnityEngine;

namespace GameStructures.Equipment.Weapons
{
    public abstract class ShotPreset : ScriptableObject
    {
        public abstract void Shot(ShotStats shotStats, HitStats hitStats, Pool<Projectile> projectilePool);
    }
}
