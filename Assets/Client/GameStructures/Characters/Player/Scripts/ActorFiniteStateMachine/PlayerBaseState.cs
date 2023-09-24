using SpaceTraveler.GameStructures.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public abstract class PlayerBaseState
    {
        protected Player player;

        protected bool isActive = false;
        protected string stateName;
        protected float startTime;

        protected PlayerStateMachine stateMachine => player.StateMachine;
        protected PlayerStatsHandler playerStatsHandler => player.StatsHandler;
        protected PlayerAnimatorController playerAnimatorController => player.AnimatorController;
        protected PlayerController playerController => player.Controller;
        protected CharacterSurfaceCheckHandler surfaceCheckHandler => playerController.SurfaceCheckHandler;
        public string Name => stateName;
        public PlayerBaseState(Player player)
        {
            this.player = player;
        }
        public virtual void Enter()
        {
            Debug.Log($"Enter in {this}");
            isActive = true;
            startTime = Time.time;
        }

        public virtual void Exit()
        {
            isActive = false;
        }
    }
}
