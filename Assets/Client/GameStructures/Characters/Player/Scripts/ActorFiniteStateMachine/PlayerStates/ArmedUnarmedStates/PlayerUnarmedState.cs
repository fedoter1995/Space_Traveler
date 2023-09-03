using SpaceTraveler.Characters.Actor.ActorFiniteStateMachine;
using System.Collections;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerUnarmedState : PlayerArmedUnarmedState
    {
        public PlayerUnarmedState(Player player) : base(player)
        {
        }
        public override void Enter()
        {
            base.Enter();
            animatorController.SetBool(intArmed, false);
        }
    }
}