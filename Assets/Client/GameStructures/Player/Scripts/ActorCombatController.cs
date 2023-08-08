using GameStructures.Hits;
using GameStructures.Player;
using GameStructures.Zones;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class ActorCombatController : MonoBehaviour
{
    [SerializeField]
    private List<ComboElement> _comboElements = new List<ComboElement>();
    [SerializeField]
    private List<AttackTriggerZone> _attackZones;

    private Actor actor;
    private ActorStatsHandler statsHandler;

    public void Initialize(Actor actor, ActorStatsHandler statsHandler)
    {
        this.actor = actor; 
        this.statsHandler = statsHandler;
    }
    public void OnAttackTrigger(int attackId)
    {
        var comboElement = _comboElements.Find(element => element.AnimationId == attackId);
        var currentZone = _attackZones.Find(zone => zone.AttacksId.Contains(attackId));
        if(currentZone != null)
        {
            DealHit(currentZone, comboElement);
        }

    }

    private void DealHit(AttackTriggerZone zone, ComboElement comboElement)
    {
        foreach (ITriggerObject element in zone.InZoneObjects)
        {
            var hitObj = element as ITakeHit;
            
            if(hitObj != null)
            {
                var hit = new Hit(statsHandler.GetHitStats());
                hitObj.TakeHit(actor, hit);
            }
        }
    }
}
