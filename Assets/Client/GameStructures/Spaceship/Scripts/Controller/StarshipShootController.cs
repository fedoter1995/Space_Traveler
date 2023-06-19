using GameStructures.Hits;
using GameStructures.Spaceship;
using GameStructures.Stats;
using System.Collections;
using UnityEngine;

public class StarshipShootController : MonoBehaviour
{
    private Starship ship;
    private IStarshipInputManager inputManager;
    private Coroutine shootEnumerator = null;
    private bool isInitialize = false;

    private StarshipStatsHandler Stats => (StarshipStatsHandler)ship.StatsHandler;
    private void Update()
    { 
        if(isInitialize)
            if (inputManager.Fire && shootEnumerator == null)
                shootEnumerator = StartCoroutine(ShootRoutine());
    }
    public void Initialize(IStarshipInputManager inputManager, Starship ship)
    {
        isInitialize = true;
        this.ship = ship;
        this.inputManager = inputManager;
    }

    private IEnumerator ShootRoutine()
    {
        HitStats hitStats = Stats.GetHitStats();
        ShotStats shotStats = Stats.GetShotStats();
        ship.Equipment.MainWeapon.Shot(ship, shotStats, hitStats);
        yield return new WaitForSeconds(1/ Stats.RateOfFire);
        shootEnumerator = null;
    }
}
