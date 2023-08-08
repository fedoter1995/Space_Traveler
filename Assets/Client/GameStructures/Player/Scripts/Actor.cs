using Assets.Client.GameStructures.Player.Scripts;
using CustomTools.Observable;
using GameStructures.Garage.Workshop;
using GameStructures.Gear;
using GameStructures.Hits;
using GameStructures.Stats;
using GameStructures.Zones;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Player
{
    public class Actor : MonoBehaviour
    {

        [SerializeField]
        private string _name;
        [SerializeField]
        private Inventory _inventory = new Inventory();
        [SerializeField]
        private SpaceshipModuleHandler _equipment = new SpaceshipModuleHandler();
        [SerializeField]
        private ActorStatsHandler _stats;
        [SerializeField]
        private TakeDamageHandler _takeDamageHandler;
        [SerializeField]
        private WorkshopSettings _workshopSettings;
        [SerializeField]
        private EnemyTriggerZone _triggerZone;


        private ActorController actorController;
        private ActorCombatController combatController;
        private ActorAnimatorController animatorController;

        private List<IJsonSerializable> serializableObjects;

        #region Events
        public event Action ShootEvent;
        public event Action<HitStats> OnTakeHitEvent;
        public event Action<object, DamageTypeValue> OnTakeDamageEvent;
        #endregion

        public Observable<int> HealthPoints { get; private set; }
        public ActorController Controller => actorController;
        public SpaceshipModuleHandler Equipment => _equipment;
        public Inventory Inventory => _inventory;
        public ActorStatsHandler StatsHandler => _stats;
        public Vector3 Position => transform.position;
        public TriggerObjectType Type => TriggerObjectType.Player;

        public TakeDamageHandler TakeHitHandler => _takeDamageHandler;


        
        private void Awake()
        {
            actorController = GetComponent<ActorController>();
            animatorController = GetComponentInChildren<ActorAnimatorController>();
            combatController = GetComponentInChildren<ActorCombatController>();

            actorController.Initialize(new KeyboardActorInputManager(), this);
            animatorController.Initialize(Controller);
            combatController.Initialize(this, _stats);
            StatsHandler.Initialize();
        }


    }
}