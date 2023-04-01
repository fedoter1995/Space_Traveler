using CustomTools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace GameStructures.Meteors
{
    public class Asteroids : IJsonSerializable
    {

        private List<AsteroidPool> pools;

        public event Action<Asteroid> OnDestroyAsteroidEvent;

        public void OnDestroyAsteroid(Asteroid asteroid)
        {
            OnDestroyAsteroidEvent?.Invoke(asteroid);
        }

        public void Initialize()
        {
            if (pools == null)
                FindPools();

            foreach (AsteroidPool pool in pools)
            {
                InitPool(pool);
            }
        }

        private void FindPools()
        {
            var pools = UnityEngine.Object.FindObjectsOfType<AsteroidPool>();

            this.pools = new List<AsteroidPool>(pools);
        }
        private void InitPool(AsteroidPool pool)
        {
            pool.Initialize();
            pool.OnDestroyAsteroidEvent += OnDestroyAsteroid;
        }

        public void SetObjectData(Dictionary<string, object> data)
        {
            FindPools();

            foreach(AsteroidPool pool in pools)
            {
                if(data.ContainsKey(pool.Name))
                {
                    var poolData = CustomConvert.JObjectToDict<string, object>((JObject)data[pool.Name]);
                    pool.SetObjectData(poolData);
                }

            }
        }

        public Dictionary<string, object> GetObjectData()
        {
            var data = new Dictionary<string, object>();

            foreach (AsteroidPool pool in pools)
            {
                data.Add(pool.Name, pool.GetObjectData());
            }

            return data;
        }


    }
}


