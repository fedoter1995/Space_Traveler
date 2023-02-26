using CustomTools;
using GameStructures.Equipment.Weapons;
using GameStructures.Stats;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Equipment/Weapon/new_Kinewtic_Weapon")]
public class KineticWeapons : Weapon
{
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

    public override void Shot(ShotStats shotStats, HitStats hitStats)
    {
        _shootPreset.Shot(shotStats,hitStats, projectilePool);
    }

    private void CreateProjectileStorage()
    {
        projectileStorage = new GameObject($"{Name}_Projectile_Storage");
    }


}

