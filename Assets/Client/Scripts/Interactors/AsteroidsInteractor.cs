using Architecture;
using System.Collections.Generic;
using SpaceTraveler.GameStructures.Meteors;

public class AsteroidsInteractor : Interactor
{
    public Asteroids asteroids;

    public event System.Action<Asteroid> OnDestroyAsteroidEvent;

    public override void OnCreate()
    {
        asteroids = new Asteroids();

        var data = Game.saveController.Load(ToString());

        SetObjectData(data);

        asteroids.Initialize();

        asteroids.OnDestroyAsteroidEvent += OnDestroyAsteroid;
    }

    public void OnDestroyAsteroid(Asteroid asteroid)
    {
        OnDestroyAsteroidEvent?.Invoke(asteroid);
    }

    public override Dictionary<string, object> GetObjectData()
    {
        return asteroids.GetObjectData();
    }
    public override void SetObjectData(Dictionary<string, object> data)
    {
        if(data != null)
            asteroids.SetObjectData(data);
    }
    public override string ToString()
    {
        return "Asteroids";
    }
}
