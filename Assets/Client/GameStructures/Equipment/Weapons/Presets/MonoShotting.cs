using CustomTools;
using GameStructures.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStructures.Equipment.Weapons
{
    [CreateAssetMenu(menuName = "Presets/Mono_Shotting")]
    public class MonoShotting : ShotPreset
    {
        public override void Shot(ShotStats shotStats, HitStats hitStats, Pool<Projectile> projectilePool)
        {
            var proj = projectilePool.GetFreeObject();
            var projSettings = new ProjSettings(shotStats.ShotDir[0], shotStats.ShotSpeed);
            proj.Initialize(projSettings, hitStats);
            proj.transform.position = shotStats.ShotPos[0];
            proj.transform.rotation = shotStats.Rotation[0];
            proj.Move();
        }
    }
}
