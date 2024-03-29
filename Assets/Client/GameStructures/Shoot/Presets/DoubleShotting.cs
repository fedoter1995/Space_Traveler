﻿using CustomTools;
using SpaceTraveler.GameStructures.Gear.Weapons;
using SpaceTraveler.GameStructures.Projectiles;
using SpaceTraveler.GameStructures.Stats;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Gear.Weapons
{
    [CreateAssetMenu(menuName = "Presets/Double_Shotting")]
    public class DoubleShotting : ShotPreset
    {
        [SerializeField]
        private Vector2 _delta;

        public override void Shot(object sender, ShotStats shotStats, HitStats hitStats, Pool<Projectile> projectilePool)
        {

            var proj = projectilePool.GetFreeObject();
            var proj_1 = projectilePool.GetFreeObject();

            var projSettings = new ProjSettings(shotStats.ShotDir, shotStats.ShotSpeed);
            var projSettings1 = new ProjSettings(shotStats.ShotDir, shotStats.ShotSpeed);

            proj.Initialize(sender, projSettings, hitStats);
            proj_1.Initialize(sender, projSettings1, hitStats);

            proj.transform.position = shotStats.ShotPos[1] - _delta;
            proj_1.transform.position = shotStats.ShotPos[2] - _delta;

            proj.transform.rotation = shotStats.Rotation[1];
            proj_1.transform.rotation = shotStats.Rotation[2];


            proj.transform.up = shotStats.ShotDir.normalized;
            proj_1.transform.up = shotStats.ShotDir.normalized;

            proj.Move();
            proj_1.Move();

        }
    }
}
