using System;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }
        public PlayerArmedUnarmedState CurrentArmamentState { get; private set; }
        public SuperState SuperState { get; private set; }

        public void Initialize(PlayerArmedUnarmedState initialSuperState, PlayerState initialState)
        {
            CurrentArmamentState = initialSuperState;
            CurrentState = initialState;
            CurrentArmamentState.Enter();
            CurrentState.Enter();
        }
        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
        public void ChangeArmamentState(PlayerArmedUnarmedState newState)
        {
            CurrentArmamentState.Exit();
            CurrentArmamentState = newState;
            CurrentArmamentState.Enter();
            CurrentState.Enter();
        }
        public void SetSuperState(SuperState newState)
        {
            if (SuperState != null)
                ClearSuperState();

            SuperState = newState;
            SuperState.Enter();

        }
        public void ClearSuperState()
        {
            if (SuperState != null)
            {
                SuperState.Exit();

                SuperState = null;
            }

        }
        public string GetMainStateName()
        {
            if (SuperState != null)
                return SuperState.Name;

            return CurrentArmamentState.Name;
        }
    }
}
