using Architecture;
using System;
using System.Collections.Generic;
using UnityEngine;

class GameStatisticsInteractor : Interactor
{

    public GameStatistics statistics;

    private SpaceshipInteractor playerInteractor;
    private AsteroidsInteractor asteroidsInteractor;

    public override void OnCreate()
    {
        statistics = new GameStatistics();
        GetInteractors();
    }

    public override void OnInitialize()
    {
        var objectData = Game.saveController.Load(statistics.ToString());
        statistics.SetObjectData(objectData);
        asteroidsInteractor.asteroids.OnDestroyAsteroidEvent += statistics.OnDestroyAsteroid;
    }

    public void GetInteractors()
    {
        playerInteractor = Game.GetInteractor<SpaceshipInteractor>();
        asteroidsInteractor = Game.GetInteractor<AsteroidsInteractor>();
    }

    public override Dictionary<string, object> GetObjectData()
    {
        return statistics.GetObjectData();
    }

    public override void SetObjectData(Dictionary<string, object> obj)
    {
        if(obj != null)
            statistics.SetObjectData(obj);
    }
    public override string ToString()
    {
        return "Game Statistics";
    }
}
