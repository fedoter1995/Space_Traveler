using CustomTools;
using GameStructures.Hits;
using GameStructures.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStructures.Gear.Weapons
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
