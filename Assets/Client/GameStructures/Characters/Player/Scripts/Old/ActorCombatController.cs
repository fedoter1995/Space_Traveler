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

        public void Initialize(Actor actor)
        {
            this.actor = actor; 
            this.statsHandler = actor.StatsHandler;
        }
        public void OnEndAttackTrigger(int attackId)
        {
            var comboElement = _comboElements.Find(element => element.AnimationId == attackId);
            if (comboElement == null)
                Debug.LogError($"No element with id ({attackId}) found");
            else
            {
                var currentZone = _attackZones.Find(zone => zone.AttacksId.Contains(attackId));

                if (currentZone == null)
                    Debug.LogError($"There is no suitable zone for {comboElement}");
                else
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

