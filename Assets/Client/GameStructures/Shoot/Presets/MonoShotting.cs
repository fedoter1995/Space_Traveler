using CustomTools;
using SpaceTraveler.GameStructures.Gear.Weapons;
using SpaceTraveler.GameStructures.Projectiles;
using SpaceTraveler.GameStructures.Stats;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Gear.Weapons
{
    [CreateAssetMenu(menuName = "Presets/Mono_Shotting")]
    public class MonoShotting : ShotPreset
    {
        public override void Shot(object sender, ShotStats shotStats, HitStats hitStats, Pool<Projectile> projectilePool)
        {
            var proj = projectilePool.GetFreeObject();
            var projSettings = new ProjSettings(shotStats.ShotDir, shotStats.ShotSpeed);
            proj.Initialize(sender, projSettings, hitStats);
            proj.transform.position = shotStats.ShotPos[0];
            proj.transform.rotation = shotStats.Rotation[0];
            proj.transform.up = shotStats.ShotDir.normalized;
            proj.Move();
        }
    }
}
