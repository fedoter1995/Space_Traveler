using CustomTools;
using GameStructures.Hits;
using GameStructures.Stats;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace GameStructures.Gear.Weapons
{
    [CreateAssetMenu(menuName = "Item/Equipment/Weapon/new_Weapon")]
    public class Weapon : Equipment
    {
        [SerializeField]
        protected Projectile projectile;
        [SerializeField]
        private ShotPreset _shootPreset;

        private bool _haveLimit = false;
        private int _projectilesLimit = 5;
        private Pool<Projectile> projectilePool;
        private GameObject projectileStorage = null;

        public override void InitEquipment()
        {
            if (projectileStorage == null)
                projectileStorage = new GameObject($"{Name}_Projectile_Storage");

            projectilePool = new Pool<Projectile>(projectile, _projectilesLimit, projectileStorage.transform, !_haveLimit);
        }

        public virtual void Shot(object sender, ShotStats shotStats, HitStats hitStats)
        {
            _shootPreset.Shot(sender, shotStats, hitStats, projectilePool);
        }
        public override List<StatModifier> GetAllModifiers()
        {
            var modifiers = new List<StatModifier>(_modifiers);
            modifiers.AddRange(projectile.GetAllModifiers());
            return modifiers;
        }

    }
}


