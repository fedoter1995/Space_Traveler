using CustomTools;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Equipment/Weapon/new_Kinewtic_Weapon")]
public class KineticWeapons : Weapon
{
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

    public override void Shot(ShotStats stats)
    {
        var proj = projectilePool.GetFreeObject();
        proj.Initialize(stats);
        proj.transform.position = stats.ShotPos;
        proj.transform.rotation = stats.Rotation;
        proj.Move();
    }

    private void CreateProjectileStorage()
    {
        projectileStorage = new GameObject($"{Name}_Projectile_Storage");
    }


}

