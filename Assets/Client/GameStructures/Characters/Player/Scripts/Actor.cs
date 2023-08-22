using SpaceTraveler.Scripts;
using CustomTools.Observable;
using SpaceTraveler.GameStructures.Gear;
using SpaceTraveler.GameStructures.ItemCollections;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Workshop;
using SpaceTraveler.GameStructures.Zones;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters.Player
{
    [RequireComponent(typeof(ActorController), typeof(ActorCombatController))]
    public class Actor : MonoBehaviour, IPlayerObject
    {

        [SerializeField]
        private string _name;
        [SerializeField]
        private Inventory _inventory = new Inventory();
        [SerializeField]
        private ActorEquipmentHandler _equipment = new ActorEquipmentHandler();
        [SerializeField]
        private ActorStatsHandler _stats;
        [SerializeField]
        private ProtectiveComponentsHandler _protectiveComponentsHandler;
        [SerializeField]
        private ActorAnimatorController _animatorController;


        [SerializeField]
        private WorkshopSettings _workshopSettings;


        private ActorController actorController;

        private List<IJsonSerializable> serializableObjects;

        #region Events
        public event Action ShootEvent;
        public event Action<HitStats> OnTakeHitEvent;
        public event Action<object, DamageAttributes> OnTakeDamageEvent;
        #endregion

        public Observable<int> HealthPoints { get; private set; }
        public ActorController Controller => actorController;
        public IEqupmentHandler Equipment => _equipment;
        public Inventory Inventory => _inventory;
        public ActorStatsHandler StatsHandler => _stats;
        public Vector3 Position => transform.position;
        public TriggerObjectType Type => TriggerObjectType.Player;

        public WorkshopSettings WorkshopSettings => _workshopSettings;

        private void Awake()
        {
            actorController = GetComponent<ActorController>();
            
            StatsHandler.Initialize(this);

            actorController.Initialize(new KeyboardActorInputManager(), this);
            _animatorController.Initialize(Controller);


            _protectiveComponentsHandler.Initialize(StatsHandler);
            _protectiveComponentsHandler.OnTakeDamageEvent += OnTakeDamage;
        }
        private void OnTakeDamage(object sender, DamageAttributes damageStats)
        {
            _animatorController.TakeDamageAnimation(damageStats);
        }


    }
}