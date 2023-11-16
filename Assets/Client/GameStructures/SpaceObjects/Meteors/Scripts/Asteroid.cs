using CustomTools.Observable;
using System;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;
using Newtonsoft.Json.Linq;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Spaceship;
using SpaceTraveler.GameStructures.Stats;

namespace SpaceTraveler.GameStructures.Meteors
{
    [RequireComponent(typeof(LootRandomizer))]
    public class Asteroid : MonoBehaviour, IAsteroid, IJsonSerializable
    {   
        [SerializeField]
        protected AsteroidType _type;
        [SerializeField]
        private TakeDamageHandler _takeDamageHandler;

        [SerializeField]
        private AsteroidStatsHandler stats;

        private LootRandomizer loot;
        public Action<Asteroid> OnDisableObject { get; set; }

        public event Action<int> OnHealthChangeEvent;
        public event Action<Asteroid> OnDestroyEvent;
        public event Action OnEnableEvent;
        public event Action OnDisableEvent;
        public AsteroidStatsHandler Stats => stats;

        public Observable<int> HealthPoints { get; private set; }
        public AsteroidType Type => _type;
        public Vector3 Position => transform.position;

        public TakeDamageHandler TakeHitHandler => _takeDamageHandler;

        public virtual void Initialize()
        {
            if (_takeDamageHandler == null)
                throw new Exception("TakeDamageHandler is not installed");          

            stats.Initialize(this);
            stats.CalculateValues();

            _takeDamageHandler.Initialize(stats);
            _takeDamageHandler.OnTakeDamageEvent += TakeDamage;

            if (HealthPoints == null)
                HealthPoints = new Observable<int>(Stats.HealthPoints);

            loot = GetComponent<LootRandomizer>();
        }

        public void TakeDamage(DamageAttributes damage)
        {
            HealthPoints.Value -= damage.Value;
            if (HealthPoints.Value <= 0)
                DestroyAsteroid();
        }
        public void Hit(ITakeHit target)
        {
            var hitStats = new HitStats(this, stats.GetShotDamage());
            target.TakeHit(this, hitStats);
        }

        public void SetObjectData(Dictionary<string, object> data)
        {

            Vec2Pos coord = CustomConvert.JObjectToObject<Vec2Pos>((JObject)data["Position"]);
            bool Activity = (bool)data["Activity"];

            gameObject.SetActive(Activity);
            var hp = System.Convert.ToInt32(data["HealthPoints"]);
            HealthPoints = new Observable<int>(hp);
            transform.position = new Vector3(coord.x,coord.y,0);

        }
        public Dictionary<string, object> GetObjectData()
        {
            var data = new Dictionary<string, object>();
            Vec2Pos pos = new Vec2Pos(transform.position.x, transform.position.y);

            data.Add("Position", pos);
            data.Add("HealthPoints", HealthPoints.Value);
            data.Add("Activity", gameObject.activeInHierarchy);


            return data;
        }
        protected virtual void DestroyAsteroid()
        {
        
            OnDestroyEvent?.Invoke(this);

            gameObject.SetActive(false);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var target = collision.GetComponent<ITakeHit>();
        
            if (target != null && target is not Asteroid)
                Hit(target);
        }
        private void OnDisable()
        {
            OnDisableEvent?.Invoke();
            OnDisableObject?.Invoke(this);
        }

    }

    public enum AsteroidType
    {
        Large,
        Medium,
        Small
    }
    public struct Vec2Pos
    {
        public float x { get; private set; }
        public float y { get; private set; }
        public Vec2Pos(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public struct AsteroidState
    {
        public Vec2Pos Position { get; private set; }
        public bool Activity { get; private set; }
        public AsteroidState(Vec2Pos position, bool activity)
        {
            Position = position;
            Activity = activity;
        }
    }
}


