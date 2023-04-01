using CustomTools.Observable;
using System;
using System.Collections.Generic;
using UnityEngine;
using CustomTools;
using System.Runtime.Serialization;
using GameStructures.Stats;
using Newtonsoft.Json.Linq;
using GameStructures.Hits;

namespace GameStructures.Meteors
{
    [RequireComponent(typeof(LootRandomizer))]
    public class Asteroid : MonoBehaviour, IAsteroid, IJsonSerializable
    {   
        [SerializeField]
        protected AsteroidType _type;

        [SerializeField]
        private AsteroidStatsHandler stats = new AsteroidStatsHandler();

        private LootRandomizer loot;


        public AsteroidStatsHandler Stats => stats;

        public event Action<object,DamageType,DamageValue> OnTakeDamageEvent;
        public event Action<int> OnHealthChangeEvent;
        public event Action<Asteroid> OnDisableEvent;
        public event Action<Asteroid> OnDestroyEvent;
        public event Action<HitStats> OnTakeHitEvent;

        public Observable<int> HealthPoints { get; private set; }

        public AsteroidType Type => _type;
        public int PointPrice => stats.PointPrice;

        public virtual void Initialize()
        {
            stats.Initialize();
            stats.CalculateValues();
            if (HealthPoints == null)
                InitObservable(Stats.HealthPoints);

            loot = GetComponent<LootRandomizer>();
        }
        public virtual void DestroyAsteroid(object sender)
        {
        
            OnDestroyEvent?.Invoke(this);

            var spaceship = sender as Spaceship;

            if(spaceship != null)
                loot.DropLoot(this, spaceship.Inventory);

            gameObject.SetActive(false);
        }
        public void TakeDamage(object sender, HitDamage damage)
        {
            foreach (KeyValuePair<DamageType, DamageValue> entry in damage.DamageTypeValueDict)
            {
                Message damageMessage = new Message(this, entry.Key, entry.Value);

                HealthPoints.Value -= entry.Value.intNumber;
                OnTakeDamageEvent?.Invoke(this, entry.Key, entry.Value);
            }

            if (HealthPoints.Value <= 0)
                DestroyAsteroid(sender);
        }
        public void TakeHit(object sender, Hit hit)
        {
            var takenDamage = hit.GetHitDamage(Stats.Resistances);
            TakeDamage(sender, takenDamage);
        }
        public void Hit(ITakeHit target)
        {
            var hitStats = new HitStats(stats.GetShotDamage());
            var hit = new Hit(hitStats);
            target.TakeHit(this, hit);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var target = collision.GetComponent<ITakeHit>();
        
            if (target != null && target is not Asteroid)
                Hit(target);
        }
        private void OnDisable()
        {
            OnDisableEvent?.Invoke(this);
        }
        private void InitObservable(int value)
        {
            HealthPoints = new Observable<int>(value);
        }
        public void SetObjectData(Dictionary<string, object> data)
        {

            Vec2Pos coord = CustomConvert.JObjectToObject<Vec2Pos>((JObject)data["Position"]);
            bool Activity = (bool)data["Activity"];

            gameObject.SetActive(Activity);
            var hp = System.Convert.ToInt32(data["HealthPoints"]);
            InitObservable(hp);
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
    }
    [DataContract]
    public enum AsteroidType
    {
        Large,
        Medium,
        Small
    }
    struct Vec2Pos
    {
        public float x { get; private set; }
        public float y { get; private set; }
        public Vec2Pos(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}


