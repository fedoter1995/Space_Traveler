using Assets.Client.Characters.Player;
using SpaceTraveler.GameStructures.Characters.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public abstract class PlayerState
    {
        protected Player player;

        protected bool isActive = false;
        protected float startTime;
        protected PlayerStateMachine stateMachine => player.StateMachine;
        protected ActorStatsHandler actorStatsHandler => player.StatsHandler;
        protected PlayerAnimatorController animatorController => player.AnimatorController;
        protected PlayerController playerController => player.Controller;
        public PlayerState(Player player)
        {
            this.player = player;
        }

        public virtual void Enter()
        {
            DoChecks();
            Debug.Log($"Enter in {this}");
            isActive = true;
            startTime = Time.time;
        }

        public virtual void Exit()
        {
            isActive = false;
        }

        public virtual void UpdateLogick()
        {

        }
        public virtual void UpdatePhysics()
        {
            DoChecks();
        }
        public virtual void DoChecks()
        {
        }
    }
}
