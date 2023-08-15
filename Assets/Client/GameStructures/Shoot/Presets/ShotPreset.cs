using CustomTools;
using SpaceTraveler.GameStructures.Stats;
using UnityEngine;
using SpaceTraveler.GameStructures.Projectiles;

namespace SpaceTraveler.GameStructures.Gear.Weapons
{
    public abstract class ShotPreset : ScriptableObject
    {
        public abstract void Shot(object sender, ShotStats shotStats, HitStats hitStats, Pool<Projectile> projectilePool);
    }
}
