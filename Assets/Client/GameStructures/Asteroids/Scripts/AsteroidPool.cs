using CustomTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AsteroidPool : MonoBehaviour
{
    [SerializeField]
    private Asteroid _asteroidPrefab;
    [SerializeField]
    private int _poolSize = 0;
    [SerializeField]
    private bool _autoExpand = false;

    protected Pool<Asteroid> asteroidPool;

    public event Action<Asteroid> OnCreateAsteroidEvent;
    public event Action<Asteroid> OnDestroyAsteroidEvent;
    public Asteroid Prefab => _asteroidPrefab;
    public void Initialize()
    {
        asteroidPool = new Pool<Asteroid>(_asteroidPrefab, _poolSize, transform, _autoExpand);
        asteroidPool.OnCreateNewObjectEvent += OnCreateAsteroid;
        foreach(Asteroid asteroid in asteroidPool.objectsPool)
        {
            asteroid.OnDestroyEvent += OnDestroyAsteroid;
        }
    }
    public void Initialize(Transform container)
    {
        asteroidPool = new Pool<Asteroid>(_asteroidPrefab, _poolSize, container, _autoExpand);
        asteroidPool.OnCreateNewObjectEvent += OnCreateAsteroid;
        foreach (Asteroid asteroid in asteroidPool.objectsPool)
        { 
            asteroid.OnDestroyEvent += OnDestroyAsteroid;
        }
    }
    public void OnCreateAsteroid(Asteroid asteroid)
    {
        OnCreateAsteroidEvent?.Invoke(asteroid);
    }
    public void OnDestroyAsteroid(Asteroid asteroid)
    {
        OnDestroyAsteroidEvent?.Invoke(asteroid);
    }
    public List<Asteroid> GetActiveAsteroids()
    {
        return asteroidPool.ActiveObjects;
    }
    public Asteroid CreateAsteroid(Vector2 position)
    {
        var asteroid = asteroidPool.GetFreeObject();
        asteroid.Initialize();
        asteroid.transform.position = position;
        return asteroid;
    }
    public Asteroid CreateAsteroid(Vector2 position, Vector2 dirrection)
    {
        var asteroid = asteroidPool.GetFreeObject();
        asteroid.Initialize();
        asteroid.transform.position = position;
        return asteroid;
    }
}
