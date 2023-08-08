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
    [CreateAssetMenu(menuName = "Presets/Triple_Shotting")]
    public class TripleShotting : ShotPreset
    {

        public override void Shot(object sender, ShotStats shotStats, HitStats hitStats, Pool<Projectile> projectilePool)
        {

            var proj = projectilePool.GetFreeObject();
            var proj_1 = projectilePool.GetFreeObject();
            var proj_2 = projectilePool.GetFreeObject();

            var projSettings = new ProjSettings(shotStats.ShotDir, shotStats.ShotSpeed);
            var projSettings1 = new ProjSettings(shotStats.ShotDir, shotStats.ShotSpeed);
            var projSettings2 = new ProjSettings(shotStats.ShotDir, shotStats.ShotSpeed);

            var hitStats1 = new HitStats(hitStats);
            var hitStats2 = new HitStats(hitStats);
            var hitStats3 = new HitStats(hitStats);


            proj.Initialize(sender, projSettings, hitStats1);
            proj_1.Initialize(sender, projSettings1, hitStats2);
            proj_2.Initialize(sender, projSettings2, hitStats3);

            proj.transform.position = shotStats.ShotPos[0];
            proj_1.transform.position = shotStats.ShotPos[1];
            proj_2.transform.position = shotStats.ShotPos[2];

            proj.transform.rotation = shotStats.Rotation[0];
            proj_1.transform.rotation = shotStats.Rotation[1];
            proj_2.transform.rotation = shotStats.Rotation[2];

            proj.transform.up = shotStats.ShotDir.normalized;
            proj_1.transform.up = shotStats.ShotDir.normalized;
            proj_2.transform.up = shotStats.ShotDir.normalized;

            proj.Move();
            proj_1.Move();
            proj_2.Move();

        }
    }
}
