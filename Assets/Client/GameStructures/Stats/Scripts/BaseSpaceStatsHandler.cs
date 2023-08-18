using Architecture;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    [System.Serializable]
    public abstract class BaseSpaceStatsHandler : StatsHandler
    {

        #region Const
        private const string HEALTH_POINTS = "Max_Health_Points";
        private const string MOVE_SPEED = "Max_Movement_Speed";
        private const string ACCELERATION = "Acceleration";
        private const string DECELERATION = "Deceleration";
        private const string SWING_SPEED = "Swing_Speed";
        private const string SWING_SPEEDUP = "Swing_Speedup";
        private const string SWING_SLOWDOWN = "Swing_Slowdown";
        #endregion

        [SerializeField, Header("Standart Stats")]
        protected List<Stat> _stats;

        #region Base Agent Stats
            
        public float HealthPoints { get; private set; }
        public float MoveSpeed { get; private set; }
        public float Acceleration { get; private set; }
        public float Deceleration { get; private set; }
        public float SwingSpeed { get; private set; }
        public float SwingSpeedup { get; private set; }
        public float SwingSlowdown { get; private set; }


        #endregion

        public override event Action OnCalculateValuesEvent;

        public override void Initialize(object sender)
        {
            InitializeStats(_stats);
            base.Initialize(sender);
        }
        public override void CalculateValues()
        {
            if (_environment == null)
            {
                _environment = Game.DefaultEnvironment;
            }

            CalculateValuesInList(_stats);
            OnValuesCalculated();
        }

        public override BaseStat GetStat(string statName)
        {
            var findedStat = _stats.Find(stat => stat.Name == statName);
            return findedStat;
        }

        protected override void OnValuesCalculated()
        {
            HealthPoints = GetStat(HEALTH_POINTS).Value;
            MoveSpeed = GetStat(MOVE_SPEED).Value;
            SwingSpeed = GetStat(SWING_SPEED).Value;
            Acceleration = GetStat(ACCELERATION).Value;
            Deceleration = GetStat(DECELERATION).Value;
            SwingSpeedup = GetStat(SWING_SPEEDUP).Value;
            SwingSlowdown = GetStat(SWING_SLOWDOWN).Value;
            OnCalculateValuesEvent?.Invoke();
        }
    }
}
