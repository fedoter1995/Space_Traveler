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
    [CreateAssetMenu(menuName = "Presets/Triple_Shotting")]
    public class TripleShotting : ShotPreset
    {

        public override void Shot(ShotStats shotStats, HitStats hitStats, Pool<Projectile> projectilePool)
        {

            var proj = projectilePool.GetFreeObject();
            var proj_1 = projectilePool.GetFreeObject();
            var proj_2 = projectilePool.GetFreeObject();

            var projSettings = new ProjSettings(shotStats.ShotDir[0], shotStats.ShotSpeed);
            var projSettings1 = new ProjSettings(shotStats.ShotDir[1], shotStats.ShotSpeed);
            var projSettings2 = new ProjSettings(shotStats.ShotDir[2], shotStats.ShotSpeed);

            var hitStats1 = new HitStats(hitStats);
            var hitStats2 = new HitStats(hitStats);
            var hitStats3 = new HitStats(hitStats);


            proj.Initialize(projSettings, hitStats1);
            proj_1.Initialize(projSettings1, hitStats2);
            proj_2.Initialize(projSettings2, hitStats3);

            proj.transform.position = shotStats.ShotPos[0];
            proj_1.transform.position = shotStats.ShotPos[1];
            proj_2.transform.position = shotStats.ShotPos[2];

            proj.transform.rotation = shotStats.Rotation[0];
            proj_1.transform.rotation = shotStats.Rotation[1];
            proj_2.transform.rotation = shotStats.Rotation[2];

            proj.Move();
            proj_1.Move();
            proj_2.Move();

        }
    }
}
