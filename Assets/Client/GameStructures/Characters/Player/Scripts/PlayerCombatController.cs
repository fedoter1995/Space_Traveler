using SpaceTraveler.GameStructures.Characters;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Zones;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Characters.Player
{
    public class PlayerCombatController : MonoBehaviour
    {
        [SerializeField]
        private List<ComboElement> _comboElements = new List<ComboElement>();
        [SerializeField]
        private List<AttackTriggerZone> _attackZones;

        private Player player;
        private PlayerStatsHandler statsHandler;

        public void Initialize(Player player)
        {
            this.player = player;
            this.statsHandler = player.StatsHandler;
        }
        public void TriggeredAttack(int attackId)
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
                    hitObj.TakeHit(player, statsHandler.GetHitStats(comboElement.AddedModifiers));
                }
            }
        }
    }
}
