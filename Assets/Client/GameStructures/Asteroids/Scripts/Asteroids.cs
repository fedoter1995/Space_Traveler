using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField]
    private AsteroidPool _largeAsteroids;
    [SerializeField]
    private AsteroidPool _mediumAsteroids;
    [SerializeField]
    private AsteroidPool _smallAsteroids;

    private Dictionary<AsteroidType,AsteroidPool> pools = new Dictionary<AsteroidType, AsteroidPool>();

    public event Action<Asteroid> OnDestroyAsteroidEvent;

    public Asteroid CreateAsteroid(AsteroidType type, Vector2 position, Vector2 dirrection)
    {
        var asteroid = pools[type].CreateAsteroid(position, dirrection);

        return asteroid;
    }
    public Asteroid CreateAsteroid(AsteroidType type, Vector2 position)
    {
        var asteroid = pools[type].CreateAsteroid(position);

        return asteroid;
    }

    public List<Asteroid> GetAsteroids(AsteroidType type)
    {
        var asteroids = pools[type].GetActiveAsteroids();
        
        return asteroids;
    }

    public void OnCreateAsteroid(Asteroid asteroid)
    {
        asteroid.OnDestroyEvent += OnDestroyAsteroid;
    }
    public void OnDestroyAsteroid(Asteroid asteroid)
    {
        OnDestroyAsteroidEvent?.Invoke(asteroid);
    }

    public void Initialize()
    {
        _largeAsteroids.Initialize();
        _largeAsteroids.OnCreateAsteroidEvent += OnCreateAsteroid;
        _largeAsteroids.OnDestroyAsteroidEvent += OnDestroyAsteroid;

        _mediumAsteroids.Initialize();
        _mediumAsteroids.OnCreateAsteroidEvent += OnCreateAsteroid;
        _mediumAsteroids.OnDestroyAsteroidEvent += OnDestroyAsteroid;

        _smallAsteroids.Initialize();
        _smallAsteroids.OnCreateAsteroidEvent += OnCreateAsteroid;
        _smallAsteroids.OnDestroyAsteroidEvent += OnDestroyAsteroid;


        pools.Add(_largeAsteroids.Prefab.Type, _largeAsteroids);
        pools.Add(_mediumAsteroids.Prefab.Type, _mediumAsteroids);
        pools.Add(_smallAsteroids.Prefab.Type, _smallAsteroids);
    }

}

