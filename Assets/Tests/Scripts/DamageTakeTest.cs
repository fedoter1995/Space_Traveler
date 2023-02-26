using GameStructures.Hit;
using Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using Tests;
using UnityEngine;

public class DamageTakeTest : MonoBehaviour, ITakeHit
{
    [SerializeField]
    private TestResistanceseStatsHandler statsHandler;
    public event Action<HitStats> OnTakeHitEvent;

    private void Awake()
    {

        statsHandler.Initialize();
    }

    public void TakeDamage(HitDamage damage)
    {
        foreach (KeyValuePair<DamageType, DamageValue> entry in damage.DamageTypeValueDict)
        {
            
            Message damageMessage = new Message(this, entry.Key, entry.Value);

            Debug.Log(damageMessage);
        }
    }

    public void TakeHit(Hit hit)
    {
        var takenDamage = hit.GetHitDamage(statsHandler.Resistances);
        TakeDamage(takenDamage);
    }
}
