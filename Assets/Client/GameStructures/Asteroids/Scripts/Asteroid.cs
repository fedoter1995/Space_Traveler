using CustomTools.Observable;
using System;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using CustomTools;
using System.Runtime.Serialization;

[RequireComponent(typeof(AsteroidStatsHandler))]
public class Asteroid : MonoBehaviour, ITakeDamage, IGivePoints,IPoolsObject<Asteroid>
{   
    [SerializeField]
    protected Vector3 _direction;
    [SerializeField]
    protected List<LootSlot> _loot;
    [SerializeField]
    protected AsteroidType _type;


    private AsteroidStatsHandler stats;
    public AsteroidStatsHandler Stats => stats;

    public event Action<float> OnTakeDamageEvent;
    public event Action<float> OnHealthChangeEvent;
    public event Action<Asteroid> OnDisableEvent;
    public event Action<Asteroid> OnDestroyEvent;
    public float CurrentSpeed { get; private set; }

    public Observable<int> HealthPoints { get; private set; }

    public AsteroidType Type => _type;
    public int PointPrice => stats.PointPrice;

    public virtual void Start()
    {
        Initialize();
    }

    public virtual void FixedUpdate()
    {
        //AsteroidMove();
    }
    public virtual void Initialize()
    {
        stats = GetComponent<AsteroidStatsHandler>();
        stats.Initialize();
        stats.CalculateValues();
        HealthPoints = new Observable<int>(Stats.HealthPoints);
        CurrentSpeed = UnityEngine.Random.Range(stats.MinSpeed, stats.MaxSpeed);
        AsteroidRandomRotation();
        AsteroidRandomDirrection();
    }
    protected void AsteroidRandomRotation()
    {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, UnityEngine.Random.value * 360.0f);
    }
    protected void AsteroidRandomDirrection()
    {
        _direction = new Vector3(UnityEngine.Random.Range(-180.0f, 180.0f), UnityEngine.Random.Range(-180.0f, 180.0f), 0.0f);
    }
    private void AsteroidMove()
    {
        transform.Translate(_direction.normalized * Time.fixedDeltaTime * CurrentSpeed, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.GetComponent<ITakeDamage>();
        
        if (target != null && target is not Asteroid)
            DealDamage(target);         
    }
    public Vector3 GetDirection()
    {
        return _direction;
    }
    public virtual void DestroyAsteroid()
    {
        LootRandomizer.DropLoot(_loot, transform.position);
        OnDisableEvent?.Invoke(this);
        OnDestroyEvent?.Invoke(this);
        gameObject.SetActive(false);
    }
    public void Explosion(AudioClip clip)
    {
        
    }
    public void TakeDamage(ShotDamage damage)
    {
        var takenDamage = TakeDamageHandler.CalculateDamage(damage, stats.Resistances);
        HealthPoints.Value -= takenDamage;
        OnTakeDamageEvent?.Invoke(takenDamage);

        if (HealthPoints.Value <= 0)
            DestroyAsteroid();
    }
    private void DealDamage(ITakeDamage target)
    {
        target.TakeDamage(stats.GetShotDamage());
        DestroyAsteroid();
    }
}
[DataContract]
public enum AsteroidType
{
    Large,
    Medium,
    Small
}

