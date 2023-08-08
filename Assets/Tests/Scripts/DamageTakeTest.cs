using GameStructures.Hits;
using GameStructures.Stats;
using GameStructures.Zones;
using System;
using Tests;
using UnityEngine;

public class DamageTakeTest : MonoBehaviour, ITakeHit, IHaveTakeHitHandler
{
    [SerializeField]
    private TestResistanceseStatsHandler statsHandler;

    public Vector3 Position => transform.position;

    public IHaveTakeHitHandler Obj => this;

    public TakeDamageHandler TakeHitHandler => throw new NotImplementedException();

    public TriggerObjectType Type => throw new NotImplementedException();

    TakeHitHandler IHaveTakeHitHandler.TakeHitHandler => throw new NotImplementedException();

    public event Action OnTakeHitEvent;

    private void Awake()
    {
        statsHandler.Initialize();
    }

    public void TakeDamage(HitDamage damage)
    {
        foreach (DamageTypeValue dmg in damage.DamageTypeValues)
        {
            TakeDamageMessage damageMessage = new TakeDamageMessage(this, dmg);

            Debug.Log(damageMessage);
        }
    }

    public void TakeHit(object sender, Hit hit)
    {
        var takenDamage = hit.GetHitDamage();
        TakeDamage(takenDamage);
    }
}
