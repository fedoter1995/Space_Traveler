using System;
using UnityEngine;
using UnityEngine.Events;
using CustomTools.Observable;
using Stats;
using CustomTools;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

[RequireComponent(typeof(SpaceShipController),typeof(ShipStatsHandler),typeof(EquipmentHandler))]
public class Spaceship : MonoBehaviour, ITakeDamage, IJsonSerializable
{     
    private SpaceShipController shipController;
    private ShootController shootController;
    private EquipmentHandler equipment;
    private ShipStatsHandler stats;
    private Inventory inventory;
    private List<IJsonSerializable> serializableObjects;
    
    #region Events
    public event Action ShootEvent;
    public event Action<float> OnTakeDamageEvent;
    #endregion

    public Observable<int> HealthPoints { get; private set; }
    public SpaceShipController Controller => shipController;
    public ShipStatsHandler Stats => stats;
    public EquipmentHandler Equipment => equipment;

    private float offset = -90;
    private float move = 0;
    private float timeLeft = 0;
    private Vector2 dirrection;



    #region Ship Movement

    private void SpaceShipRotation_MouseTracking()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);
        var _tempEulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle + offset : -angle + offset);
        var target = Quaternion.Euler(_tempEulerAngles);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * stats.SwingSpeed);

    }

    #endregion

    public void Initialize()
    {
        var manager = new KeyboardInputManager();
        equipment.EquipmentInitialize();
        shootController.Initialize(manager, this);
        shipController.Initialize(manager, this);
        stats.Initialize();
        HealthPoints = new Observable<int>((int)Stats.HealthPoints);
    }


    #region OnTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    #endregion

    public void TakeDamage(ShotDamage damage)
    {
        var damageDone = TakeDamageHandler.CalculateDamage(damage, new System.Collections.Generic.List<Resistance>());
        HealthPoints.Value -= damageDone;
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

        stats = MyTools.GetComponent<ShipStatsHandler>(gameObject);
        shipController = MyTools.GetComponent<SpaceShipController>(gameObject);
        shootController = MyTools.GetComponent<ShootController>(gameObject);
        equipment = MyTools.GetComponent<EquipmentHandler>(gameObject);
        inventory = MyTools.GetComponent<Inventory>(gameObject);
        
        serializableObjects.Add(equipment);
        serializableObjects.Add(inventory);
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
                else
                {
                    obj.SetObjectData(null);
                }

            }
        }
        Initialize();
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

}

