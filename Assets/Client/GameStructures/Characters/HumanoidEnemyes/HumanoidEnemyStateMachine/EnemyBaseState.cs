using SpaceTraveler.Characters.Player.PlayerFiniteStateMachine;
using SpaceTraveler.Characters.Player;
using SpaceTraveler.GameStructures.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceTraveler.GameStructures.Characters.HumanoidEnemyes;
using SpaceTraveler.GameStructures.Characters.Player;

namespace SpaceTraveler.Characters.HumanoidEnemyes.HumanoidEnemyStateMachine
{
    public class EnemyBaseState
    {
        protected HumanoidEnemy enemy;

        protected bool isActive = false;
        protected string stateName;
        protected float startTime;

        protected EnemyStateMachine stateMachine => enemy.StateMachine;
        protected ActorStatsHandler statsHandler => enemy.StatsHandler;
        protected HumanoidEnemyAnimatorController animatorController => enemy.AnimatorController;
        protected HumanoidEnemyController controller => enemy.Controller;
        public string Name => stateName;
        public EnemyBaseState(HumanoidEnemy enemy)
        {
            this.enemy = enemy;
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
