using System;
using UnityEngine;
using CustomTools.Observable;
using CustomTools;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using GameStructures.Hit;
using GameStructures.Gear;
using GameStructures.Stats;
using GameStructures.Garage.Workshop;

[RequireComponent(typeof(SpaceShipController),typeof(SpaceshipCameraController))]
public class Spaceship : MonoBehaviour, ITakeHit, IJsonSerializable, IHaveStatsHandler
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private Inventory _inventory = new Inventory();
    [SerializeField]
    private EquipmentHandler _equipment = new EquipmentHandler();
    [SerializeField]
    private ShipStatsHandler _stats = new ShipStatsHandler();


    [SerializeField]
    private WorkshopSettings _workshopSettings;

    private SpaceShipController shipController;
    private ShootController shootController;

    private List<IJsonSerializable> serializableObjects;
    
    #region Events
    public event Action ShootEvent;
    public event Action<HitStats> OnTakeHitEvent;
    public event Action<object,DamageType,DamageValue> OnTakeDamageEvent;
    #endregion

    public string Name => _name;
    public Observable<int> HealthPoints { get; private set; }
    public SpaceShipController Controller => shipController;
    //public ShipStatsHandler Stats => _stats;
    public EquipmentHandler Equipment => _equipment;
    public Inventory Inventory => _inventory;
    public StatsHandler StatsHandler => _stats;
    public WorkshopSettings WorkshopSettings => _workshopSettings;


    private float offset = -90;
    private float move = 0;
    private float timeLeft = 0;
    private Vector2 dirrection;




    public void Initialize()
    {
        var inventory = Architecture.Game.GetInteractor<InventoryInteractor>().collection;

        var manager = new KeyboardInputManager();
        _inventory = inventory;

        _equipment.Initialize();
        _stats.Initialize();
        _workshopSettings.Initialize(this);
        shootController.Initialize(manager, this);
        shipController.Initialize(manager, this);
        HealthPoints = new Observable<int>((int)_stats.HealthPoints);
    }
    public void TakeDamage(HitDamage damage)
    {
        foreach (KeyValuePair<DamageType, DamageValue> entry in damage.DamageTypeValueDict)
        {
            Debug.Log(entry.Key);
            Debug.Log(entry.Value);
            HealthPoints.Value -= entry.Value.intNumber;
            OnTakeDamageEvent?.Invoke(this, entry.Key, entry.Value);
        }

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
            foreach(IJsonSerializable obj in serializableObjects)
            {
                var objData = MyTools.JObjectToDict<string, object>((JObject)data[obj.ToString()]);
                obj.SetObjectData(objData);
            }
        }
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
    public void TakeHit(Hit hit)
    {
        var dmg = hit.GetHitDamage(_stats.Resistances);
        TakeDamage(dmg);
    }
    public override string ToString()
    {
        return _name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.GetComponent<ItemObject>();
        if (obj != null)
            _inventory.TryToAddToCollection(obj, obj.ItemSlot.CurrentItem, obj.ItemSlot.Amount);
    }
    private void GetComponents()
    {
        serializableObjects = new List<IJsonSerializable>();

        serializableObjects.Add(_stats);
        serializableObjects.Add(_equipment);
        serializableObjects.Add(_workshopSettings);

        shipController = gameObject.GetComponent<SpaceShipController>();
        shootController = gameObject.GetComponent<ShootController>();

    }

}

