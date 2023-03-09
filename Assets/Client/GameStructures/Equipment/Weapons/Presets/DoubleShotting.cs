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
    [CreateAssetMenu(menuName = "Presets/Double_Shotting")]
    public class DoubleShotting : ShotPreset
    {
        [SerializeField]
        private Vector2 _delta;

        public override void Shot(ShotStats shotStats, HitStats hitStats, Pool<Projectile> projectilePool)
        {

            var proj = projectilePool.GetFreeObject();
            var proj_1 = projectilePool.GetFreeObject();

            var projSettings = new ProjSettings(shotStats.ShotDir[1], shotStats.ShotSpeed);
            var projSettings1 = new ProjSettings(shotStats.ShotDir[2], shotStats.ShotSpeed);

            proj.Initialize(projSettings, hitStats);
            proj_1.Initialize(projSettings1, hitStats);

            proj.transform.position = shotStats.ShotPos[1] - _delta;
            proj_1.transform.position = shotStats.ShotPos[2] - _delta;

            proj.transform.rotation = shotStats.Rotation[1];
            proj_1.transform.rotation = shotStats.Rotation[2];

            proj.Move();
            proj_1.Move();

        }
    }
}
