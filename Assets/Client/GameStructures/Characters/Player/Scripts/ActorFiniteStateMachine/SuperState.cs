using System.Collections;
using UnityEngine;

namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public abstract class SuperState : PlayerBaseState
    {
        protected SuperState(Player player) : base(player)
        {
        }
    }
}