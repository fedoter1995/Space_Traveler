﻿using CustomTools;
using GameStructures.Gear.Weapons;
using GameStructures.Hits;
using GameStructures.Stats;
using UnityEngine;

namespace GameStructures.Enemy
{
    public class EnemyShootController : MonoBehaviour
    {
        [SerializeField]
        private ShotPreset shotPreset;
        [SerializeField]
        protected Projectile projectile;
        [SerializeField]
        private bool _haveLimit = false;
        [SerializeField]
        private int _projectilesLimit = 5;
        [SerializeField]
        private GameObject projectileStorage = null;

        private Enemy enemy;
        private Pool<Projectile> projectilePool;
        private bool isInitialize = false;


        public EnemyController EnemyController => enemy.Controller;

        public EnemyStatsHandler Stats => enemy.StatsHandler;


        public void Shot(Vector3 dirrection)
        {
            if (isInitialize)
            {
                HitStats hitStats = Stats.GetHitStats();
                ShotStats shotStats = Stats.GetShotStats(dirrection);
                shotPreset.Shot(enemy, shotStats, hitStats, projectilePool);
            }
        }

        public void Initialize(Enemy enemy)
        {

            this.enemy = enemy;

            if (projectileStorage == null)
                projectileStorage = new GameObject($"{enemy.Name}_Projectile_Storage");

            projectilePool = new Pool<Projectile>(projectile, _projectilesLimit, projectileStorage.transform, !_haveLimit);

            this.enemy = enemy;
            isInitialize = true;
        }
    }
}