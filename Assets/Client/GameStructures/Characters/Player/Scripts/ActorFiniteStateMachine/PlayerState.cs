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
        protected PlayerStateMachine stateMachine;
        protected ActorStatsHandler actorStatsHandler;

        protected float startTime;
        public PlayerState(Player player, PlayerStateMachine stateMachine)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            actorStatsHandler = player.StatsHandler;
        }

        public virtual void Enter()
        {
            DoChecks();
            startTime = Time.time;
        }

        public abstract void Exit();

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
