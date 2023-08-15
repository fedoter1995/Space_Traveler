using GameStructures.Spaceship;
using SpaceTraveler.GameStructures.Gear.Spaceship;
using SpaceTraveler.GameStructures.Stats;
using System.Collections;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Spaceship
{
    public class StarshipShootController : MonoBehaviour
    {
        private Starship ship;
        private IStarshipInputManager inputManager;
        private Coroutine shootEnumerator = null;
        private bool isInitialize = false;

        private SpaceshipModuleHandler spaceshipModuleHandler;
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
            spaceshipModuleHandler = ship.Equipment as SpaceshipModuleHandler;
            this.inputManager = inputManager;
        }

        private IEnumerator ShootRoutine()
        {
            HitStats hitStats = Stats.GetHitStats();
            ShotStats shotStats = Stats.GetShotStats();
            spaceshipModuleHandler.MainWeapon.Shot(ship, shotStats, hitStats);
            yield return new WaitForSeconds(1/ Stats.RateOfFire);
            shootEnumerator = null;
        }
    }
}

