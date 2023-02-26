using GameStructures.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    private Spaceship ship;
    private IInputManager inputManager;
    private Coroutine shootEnumerator = null;
    private bool isInitialize = false;

    private void Update()
    { 
        if(isInitialize)
            if (inputManager.Fire && shootEnumerator == null)
                shootEnumerator = StartCoroutine(ShootRoutine());
    }
    public void Initialize(IInputManager inputManager, Spaceship ship)
    {
        isInitialize = true;
        this.ship = ship;
        this.inputManager = inputManager;
    }

    private IEnumerator ShootRoutine()
    {
        HitStats hitStats = ship.Stats.GetHitStats();
        ShotStats shotStats = ship.Stats.GetShotStats();
        ship.Equipment.MainWeapon.Shot(shotStats,hitStats);
        yield return new WaitForSeconds(1/ship.Stats.RateOfFire);
        shootEnumerator = null;
    }
}
