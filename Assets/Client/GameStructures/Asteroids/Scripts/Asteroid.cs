using CustomTools.Observable;
using System;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using CustomTools;
using System.Runtime.Serialization;
using GameStructures.Asteroids;
using GameStructures.Hit;

public class Asteroid : MonoBehaviour, IAsteroid, IGivePoints,IPoolsObject<Asteroid>
{   
    [SerializeField]
    protected Vector3 _direction;
    [SerializeField]
    protected List<LootSlot> _loot;
    [SerializeField]
    protected AsteroidType _type;

    [SerializeField]
    private AsteroidStatsHandler stats = new AsteroidStatsHandler();
    public AsteroidStatsHandler Stats => stats;

    public event Action<object,DamageType,DamageValue> OnTakeDamageEvent;
    public event Action<int> OnHealthChangeEvent;
    public event Action<Asteroid> OnDisableEvent;
    public event Action<Asteroid> OnDestroyEvent;
    public event Action<HitStats> OnTakeHitEvent;

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
        var target = collision.GetComponent<ITakeHit>();
        
        if (target != null && target is not Asteroid)
            Hit(target);
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
    public void TakeDamage(HitDamage damage)
    {
        foreach (KeyValuePair<DamageType, DamageValue> entry in damage.DamageTypeValueDict)
        {
            Message damageMessage = new Message(this, entry.Key, entry.Value);

            HealthPoints.Value -= entry.Value.intNumber;
            OnTakeDamageEvent?.Invoke(this, entry.Key, entry.Value);
        }

        if (HealthPoints.Value <= 0)
            DestroyAsteroid();
    }

    public void TakeHit(Hit hit)
    {
        var takenDamage = hit.GetHitDamage(Stats.Resistances);
        TakeDamage(takenDamage);
    }

    public void Hit(ITakeHit target)
    {
        var hitStats = new HitStats(stats.GetShotDamage());
        var hit = new Hit(hitStats);
        target.TakeHit(hit);
    }
}
[DataContract]
public enum AsteroidType
{
    Large,
    Medium,
    Small
}

