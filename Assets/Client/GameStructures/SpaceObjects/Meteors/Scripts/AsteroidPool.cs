using CustomTools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceTraveler.GameStructures.Meteors
{
    public class AsteroidPool : MonoBehaviour, IJsonSerializable
    {

        protected List<Asteroid> asteroids;

        public event Action<Asteroid> OnCreateAsteroidEvent;
        public event Action<Asteroid> OnDestroyAsteroidEvent;


        public string Name => ToString();
        public void Initialize()
        {
            if (asteroids == null)
                FindAsteroids();

            foreach (Asteroid asteroid in asteroids)
            {
                InitAsteroid(asteroid);
            }
        }
        public void OnDestroyAsteroid(Asteroid asteroid)
        {
            OnDestroyAsteroidEvent?.Invoke(asteroid);
        }
        public List<Asteroid> GetActiveAsteroids()
        {
            var activeObjects = new List<Asteroid>();

            foreach(Asteroid asteroid in asteroids)
            {
                if(asteroid.gameObject.activeInHierarchy)
                {
                    activeObjects.Add(asteroid);
                }
            }
            return activeObjects;
        }
        public void SetObjectData(Dictionary<string, object> data)
        {
            FindAsteroids();

            var asteroidsData = CustomConvert.JObjectToDict<int, Dictionary<string, object>>((JObject)data["Asteroids"]);

            foreach (KeyValuePair<int, Dictionary<string, object>> entry in asteroidsData)
            {
                if(entry.Key < asteroids.Count)
                    asteroids[entry.Key].SetObjectData(entry.Value);
            }
        }
        public Dictionary<string, object> GetObjectData()
        {
            var data = new Dictionary<string, object>();

            var asteroidsData = new Dictionary<int, Dictionary<string, object>>();

            for(int i = 0; i < asteroids.Count; i++)
            {
                asteroidsData.Add(i, asteroids[i].GetObjectData());
            }

            data.Add("Asteroids", asteroidsData);

            return data;
        }
        private void InitAsteroid(Asteroid asteroid)
        {
            asteroid.Initialize();
            asteroid.OnDestroyEvent += OnDestroyAsteroid;
        }
        private void FindAsteroids()
        {
            var childAsteroids = GetComponentsInChildren<Asteroid>();

            asteroids = new List<Asteroid>(childAsteroids);
        }
    }
}
