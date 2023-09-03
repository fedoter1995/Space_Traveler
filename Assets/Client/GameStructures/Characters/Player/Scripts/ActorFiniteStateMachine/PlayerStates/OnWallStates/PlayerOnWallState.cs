using System.Collections;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerOnWallState : PlayerState
    {
        public PlayerOnWallState(Player player) : base(player)
        {
        }
        public override void Enter()
        {
            base.Enter();
            stateMachine.ChangeSuperState(player.UnarmedState);
        }
    }
}