using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Zones;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters.Player
{
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
        public void OnEndAttackTrigger(int attackId)
        {
            var comboElement = _comboElements.Find(element => element.AnimationId == attackId);
            var currentZone = _attackZones.Find(zone => zone.AttacksId.Contains(attackId));
            if(currentZone != null)
            {
                DealHit(currentZone, comboElement);
            }
        }
        public AudioClip GetAudioClip(int attackId)
        {
            var comboElement = _comboElements.Find(element => element.AnimationId == attackId);

            return comboElement.SlashAudio;
        }

        private void DealHit(AttackTriggerZone zone, ComboElement comboElement)
        {
            foreach (ITriggerObject element in zone.InZoneObjects)
            {
                var hitObj = element as ITakeHit;
                if (hitObj != null)
                {
                    hitObj.TakeHit(actor, statsHandler.GetHitStats(comboElement.AddedModifiers));
                }
            }
        }
    }
}

