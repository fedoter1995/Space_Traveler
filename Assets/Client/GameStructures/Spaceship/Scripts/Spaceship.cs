using System;
using UnityEngine;
using CustomTools.Observable;
using CustomTools;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using GameStructures.Hit;
using GameStructures.Equipment;
using GameStructures.Stats;

[RequireComponent(typeof(SpaceShipController))]
public class Spaceship : MonoBehaviour, ITakeHit, IJsonSerializable, IHaveStatsHandler
{
    [SerializeField]
    private Inventory _inventory = new Inventory();
    [SerializeField]
    private EquipmentHandler _equipment = new EquipmentHandler();
    [SerializeField]
    private ShipStatsHandler _stats = new ShipStatsHandler();

    private SpaceShipController shipController;
    private ShootController shootController;

    private List<IJsonSerializable> serializableObjects;
    
    #region Events
    public event Action ShootEvent;
    public event Action<HitStats> OnTakeHitEvent;
    public event Action<object,DamageType,DamageValue> OnTakeDamageEvent;
    #endregion

    public Observable<int> HealthPoints { get; private set; }
    public SpaceShipController Controller => shipController;
    //public ShipStatsHandler Stats => _stats;
    public EquipmentHandler Equipment => _equipment;
    public Inventory Inventory => _inventory;
    public StatsHandler Handler => _stats;

    private float offset = -90;
    private float move = 0;
    private float timeLeft = 0;
    private Vector2 dirrection;




    public void Initialize()
    {
        var inventory = Architecture.Game.GetInteractor<InventoryInteractor>().collection;
        var equipment = Architecture.Game.GetInteractor<EquipmentInteractor>().equipment;
        var statsHandler = Architecture.Game.GetInteractor<SpaceshipStatsHandlerInteractor>().statsHandler;
        var manager = new KeyboardInputManager();
        _inventory = inventory;
        _equipment = equipment;
        _stats = statsHandler;

        _equipment.EquipmentInitialize();
        _stats.Initialize();
        shootController.Initialize(manager, this);
        shipController.Initialize(manager, this);
        HealthPoints = new Observable<int>((int)_stats.HealthPoints);
    }


    #region OnTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.GetComponent<ItemObject>();
        if (obj != null)
            _inventory.TryToAddToCollection(obj, obj.ItemSlot.CurrentItem, obj.ItemSlot.Amount);
    }
    #endregion

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

    private void GetComponents()
    {
        serializableObjects = new List<IJsonSerializable>();

        shipController = MyTools.GetComponent<SpaceShipController>(gameObject);
        shootController = MyTools.GetComponent<ShootController>(gameObject);

    }
    public void SetObjectData(Dictionary<string, object> data)
    {
        GetComponents();
        if (data != null)
        {
            foreach (IJsonSerializable obj in serializableObjects)
            {
                string objKey = obj.ToString();
                if (data.ContainsKey(objKey))
                {
                    JObject jobj = (JObject)data[objKey];
                    var newData = jobj.ToObject<Dictionary<string, object>>();
                    obj.SetObjectData(newData);
                }
            }
        }
    }

    public Dictionary<string, object> GetObjectData()
    {
        var dict = new Dictionary<string, object>();
        foreach(IJsonSerializable obj in serializableObjects)
        {
            dict.Add(obj.ToString(), obj.GetObjectData());
        }

        return dict;
    }

    public void TakeHit(Hit hit)
    {
        var dmg = hit.GetHitDamage(_stats.Resistances);
        TakeDamage(dmg);
    }
}

