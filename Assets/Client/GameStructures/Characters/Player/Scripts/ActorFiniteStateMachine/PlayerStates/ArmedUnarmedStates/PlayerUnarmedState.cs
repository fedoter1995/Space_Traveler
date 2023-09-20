using SpaceTraveler.Characters.Actor.ActorFiniteStateMachine;
using System.Collections;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerUnarmedState : PlayerArmedUnarmedState
    {
        public PlayerUnarmedState(Player player) : base(player)
        {
            stateName = "Unarmed_States";
        }
        public override void Enter()
        {
            base.Enter();
        }
    }
}