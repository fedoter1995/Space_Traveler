using SpaceTraveler.GameStructures.Characters;
using System.Collections;
using UnityEngine;

namespace Assets.Client.Characters.Player
{
    [RequireComponent(typeof(CharacterAnimationEventsHandler))]
    public class PlayerAnimatorController : AnimatorController
    {
        public CharacterAnimationEventsHandler EventsHandler { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            EventsHandler = GetComponent<CharacterAnimationEventsHandler>();
        }
    }
}