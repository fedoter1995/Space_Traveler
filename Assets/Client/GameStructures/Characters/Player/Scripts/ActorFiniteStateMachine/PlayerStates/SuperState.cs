using System.Collections;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public abstract class SuperState : PlayerState
    {
        public SuperState(Player player) : base(player)
        {
        }
    }
}