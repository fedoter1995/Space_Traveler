using System.Collections;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerArmedState : PlayerArmedUnarmedState
    {
        public PlayerArmedState(Player player) : base(player)
        {
            stateName = "Armed_States";
        }
        public override void Enter()
        {
            base.Enter();
        }
    }
}