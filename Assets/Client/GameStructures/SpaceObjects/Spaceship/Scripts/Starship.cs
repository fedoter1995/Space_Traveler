using System;
using UnityEngine;
using CustomTools.Observable;
using CustomTools;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SpaceTraveler.GameStructures.Workshop;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.ItemCollections;
using SpaceTraveler.GameStructures.Gear.Spaceship;
using SpaceTraveler.GameStructures.Gear;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Items;
using SpaceTraveler.GameStructures.Zones;
using SpaceTraveler.Scripts;

namespace SpaceTraveler.GameStructures.Spaceship
{
    [RequireComponent(typeof(StarshipController),typeof(StarshipCameraController))]
    public class Starship : MonoBehaviour, IStarship
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private Inventory _inventory = new Inventory();
        [SerializeField]
        private SpaceshipModuleHandler _equipment = new SpaceshipModuleHandler();
        [SerializeField]
        private StarshipStatsHandler _stats;
        [SerializeField]
        private ProtectiveComponentsHandler _protectiveComponentsHandler;
        [SerializeField]
        private WorkshopSettings _workshopSettings;
        


        private StarshipController shipController;
        private StarshipShootController shootController;

        private List<IJsonSerializable> serializableObjects;
    
        #region Events
        public event Action ShootEvent;
        public event Action OnTakeHitEvent;
        public event Action<object,DamageAttributes> OnTakeDamageEvent;
        #endregion

        public string Name => _name;
        public Observable<int> HealthPoints { get; private set; }
        public StarshipController Controller => shipController;
        public IEqupmentHandler Equipment => _equipment;
        public Inventory Inventory => _inventory;
        public StarshipStatsHandler StatsHandler => _stats;
        public WorkshopSettings WorkshopSettings => _workshopSettings;
        public Vector3 Position => transform.position;
        public TriggerObjectType Type => TriggerObjectType.Player;



        public void Initialize()
        {
            var inventory = Architecture.Game.GetInteractor<InventoryInteractor>().collection;

            var manager = new KeyboardStarshipInputManager();
            _inventory = inventory;
            _stats.Initialize(this);
            _workshopSettings.Initialize(this);
            shootController.Initialize(manager, this);
            shipController.Initialize(manager, this);

            _protectiveComponentsHandler.Initialize(StatsHandler);

            _protectiveComponentsHandler.OnTakeDamageEvent += TakeDamage;

            HealthPoints = new Observable<int>((int)_stats.HealthPoints);
        }
        public void TakeDamage(object sender, DamageAttributes damage)
        {
            TakeDamageMessage damageMessage = new TakeDamageMessage(sender, gameObject, damage);

            HealthPoints.Value -= damage.Value;
            OnTakeDamageEvent?.Invoke(this, damage);
            
            if (HealthPoints.Value <= 0)
                Debug.Log("Game Over");
        }
        public void ShootSound(AudioClip clip)
        {
            var audio = GameObject.FindWithTag("Sounds").GetComponent<AudioSource>();
            audio.clip = clip;
            audio.Play();
        }
        public void SetObjectData(Dictionary<string, object> data)
        {
            GetComponents();

            if (data != null)
            {
                foreach (IJsonSerializable obj in serializableObjects)
                {
                    var objData = CustomConvert.JObjectToDict<string, object>((JObject)data[obj.ToString()]);
                    obj.SetObjectData(objData);
                }
            }
            else
                _equipment.Initialize();
        }
        public Dictionary<string, object> GetObjectData()
        {
            var data = new Dictionary<string, object>();

            foreach (IJsonSerializable obj in serializableObjects)
            {
                data.Add(obj.ToString(), obj.GetObjectData());
            }

            return data;
        }
        public override string ToString()
        {
            return _name;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var obj = collision.GetComponent<ItemObject>();
            if (obj != null)
            {
                var notIncludedAmount = 0;
                
                if (_inventory.TryToAddToCollection(obj, obj.ItemSlot.CurrentItem, obj.ItemSlot.Amount,out notIncludedAmount))
                {
                    Debug.LogError("Not implemented");
                }

            }
        }
        private void GetComponents()
        {
            serializableObjects = new List<IJsonSerializable>();

            serializableObjects.Add(_stats);
            serializableObjects.Add(_equipment);
            serializableObjects.Add(_workshopSettings);

            shipController = gameObject.GetComponent<StarshipController>();
            shootController = gameObject.GetComponent<StarshipShootController>();

        }

        public void TakeHit(object sender, HitStats hitStats)
        {
            _protectiveComponentsHandler.TakeHit(sender, hitStats);
        }
    }
}


