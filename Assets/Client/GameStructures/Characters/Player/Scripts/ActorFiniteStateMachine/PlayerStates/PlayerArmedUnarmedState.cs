using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerArmedUnarmedState : SuperState
    {
        protected int intArmed = Animator.StringToHash("Armed");
        public PlayerArmedUnarmedState(Player player) : base(player)
        {
        }

    }
}