using Architecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;

public class AsteroidsInteractor : Interactor
{
    public Asteroids asteroids;
    public event System.Action<Asteroid> OnDestroyAsteroidEvent;

    public override void OnCreate()
    {
        LoadResources();
        asteroids.OnDestroyAsteroidEvent += OnDestroyAsteroid;
    }

    public override void OnInitialize()
    {
        asteroids.Initialize();
    }

    public override void OnStart()
    {
        asteroids.CreateAsteroid(AsteroidType.Large, new Vector2(0, 5));
    }
    public void OnDestroyAsteroid(Asteroid asteroid)
    {
        OnDestroyAsteroidEvent?.Invoke(asteroid);
    }
    protected void LoadResources()
    {
        var res = MyTools.LoadObjectResource<Asteroids>("Asteroids");

        asteroids = Object.Instantiate(res);
    }

    public override Dictionary<string, object> GetObjectData()
    {
        return null;
    }

    public override void SetObjectData(Dictionary<string, object> obj)
    {
        
    }
}
