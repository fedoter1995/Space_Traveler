using Assets.Client.Characters.Player;
using SpaceTraveler.Characters.Actor.ActorFiniteStateMachine;
using SpaceTraveler.GameStructures.Characters.Player;
using SpaceTraveler.GameStructures.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private ActorStatsHandler _statsHandler;
        [SerializeField]
        private PlayerAnimatorController _animatorController;
        [SerializeField]
        private PlayerController _controller;

        private List<PlayerState> groundCheckExceptions;

        public PlayerAnimatorController AnimatorController => _animatorController;
        public PlayerStateMachine StateMachine { get; private set; }
        public ActorStatsHandler StatsHandler => _statsHandler;
        public PlayerInputHandler InputHandler {  get; private set; }
        public PlayerController Controller => _controller;

        #region Player states
        public PlayerArmedState ArmedState { get; private set; }
        public PlayerUnarmedState UnarmedState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerInAirState InAirState { get; private set; }
        public PlayerLandingState LandingState { get; private set; }
        public PlayerOnLedgeState LadgeClimbState { get; private set; }
        #endregion
        #region Unity Methods
        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            StateMachine.CurrentState.UpdateLogick();
        }
        private void FixedUpdate()
        {
            StateMachine.CurrentState.UpdatePhysics();
        }
        #endregion

        #region Init Methods
        public void Initialize()
        {
            _animatorController.Initialize();
            _statsHandler.Initialize(this);
            InputHandler = GetComponent<PlayerInputHandler>();
            _controller.Initialize(_statsHandler, InputHandler);
            StateMachine = new PlayerStateMachine();
            InitStates();
            StateMachine.Initialize(UnarmedState,IdleState);
            
        }
        private void InitStates()
        {
            UnarmedState = new PlayerUnarmedState(this);
            ArmedState = new PlayerArmedState(this);

            IdleState = new PlayerIdleState(this);
            MoveState = new PlayerMoveState(this);
            JumpState = new PlayerJumpState(this);
            InAirState = new PlayerInAirState(this);
            LandingState = new PlayerLandingState(this);
            LadgeClimbState = new PlayerOnLedgeState(this);


            groundCheckExceptions = new List<PlayerState> { JumpState, InAirState, LadgeClimbState };

        }
        #endregion
    }

}
