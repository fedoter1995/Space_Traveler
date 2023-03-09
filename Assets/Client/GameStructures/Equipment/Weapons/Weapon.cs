using CustomTools;
using GameStructures.Stats;
using Stats;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace GameStructures.Equipment.Weapons
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
                CreateProjectileStorage();

            projectilePool = new Pool<Projectile>(projectile, _projectilesLimit, projectileStorage.transform, !_haveLimit);
        }

        public virtual void Shot(ShotStats shotStats, HitStats hitStats)
        {
            _shootPreset.Shot(shotStats, hitStats, projectilePool);
        }
        public override List<StatModifier> GetAllModifiers()
        {
            var modifiers = new List<StatModifier>(_modifiers);
            modifiers.AddRange(projectile.GetAllModifiers());
            return modifiers;
        }
        protected void CreateProjectileStorage()
        {
            projectileStorage = new GameObject($"{Name}_Projectile_Storage");
        }
        public override DescriptionData GetDescriptionData()
        {
            var footer = new List<string>();

            foreach (StatModifier modifier in _modifiers)
                footer.AddRange(modifier.GetDescriptionData());

            var data = new DescriptionData(Description, Name, footer, Icon);
            return data;
        }
    }
}


