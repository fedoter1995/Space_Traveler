using System.Collections.Generic;
using UnityEngine;
using CustomTools;
using GameStructures.Spaceship;

public class ProjectileStorage : MonoBehaviour
{
    [SerializeField] private int poolSize = 4;
	[SerializeField] private bool autoExpand = false;
	[SerializeField] private Projectile prefab;
    [SerializeField] private Transform shotPoint;


    private Starship _spaceShip;
    private GameObject _container;
    private Pool<Projectile> projectilePool;

    private void Start()
    {
        _spaceShip = GameObject.FindWithTag("Player").GetComponent<Starship>();
        _container = GameObject.FindWithTag("ProjectilesPool");
        projectilePool = new Pool<Projectile>(prefab, poolSize, _container.transform, autoExpand);

        _spaceShip.ShootEvent += Shoot;
    }


    public void Shoot()
    {
        var projectile = projectilePool.GetFreeObject();
        projectile.transform.position = shotPoint.position;
        projectile.transform.rotation = shotPoint.rotation;
    }

}
