using System.Collections;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerArmedState : PlayerArmedUnarmedState
    {
        public PlayerArmedState(Player player) : base(player)
        {
        }
        public override void Enter()
        {
            base.Enter();
            animatorController.SetBool(intArmed, true);
        }
    }
}