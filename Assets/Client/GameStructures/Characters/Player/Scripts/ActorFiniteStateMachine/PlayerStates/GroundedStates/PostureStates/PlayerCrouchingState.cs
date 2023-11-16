using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public class PlayerCrouchingState : PlayerPostureState
    {
        public PlayerCrouchingState(Player player) : base(player)
        {
            stateName = "Crouching";
        }
        public override void Enter()
        {
            base.Enter();
            stateMachine.ChangeState(player.CrouchState);
        }
    }
}
