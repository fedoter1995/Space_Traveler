using SpaceTraveler.Characters.Player.PlayerFiniteStateMachine;
using SpaceTraveler.GameStructures.Characters.Player;
using SpaceTraveler.GameStructures.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Characters.Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerStatsHandler m_statsHandler;
        [SerializeField]
        private PlayerAnimatorController m_animatorController;
        [SerializeField]
        private PlayerController m_controller;
        [SerializeField]
        private PlayerAudioController m_audioController;

        public PlayerAudioController AudioController => m_audioController;
        public PlayerAnimatorController AnimatorController => m_animatorController;
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerStatsHandler StatsHandler => m_statsHandler;
        public PlayerInputHandler InputHandler {  get; private set; }
        public PlayerController Controller => m_controller;

        #region Armanent states
        public PlayerArmedState ArmedState { get; private set; }
        public PlayerUnarmedState UnarmedState { get; private set; }
        #endregion
        #region Movement states
        public PlayerMoveState MoveState { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerInAirState InAirState { get; private set; }
        public PlayerLandingState LandingState { get; private set; }
        public PlayerGrabState GrabState { get; private set; }
        public PlayerLedgeClimbState LedgeClimbState { get; private set; }
        #endregion
        #region Super States
        public PlayerOnLedgeState OnLedgeState { get; private set; }
        #endregion
        #region Attack States
        public PlayerAttackState FirstAttackState { get; private set; }
        public PlayerAttackState SecondAttackState { get; private set; }
        public PlayerAttackState ThirdAttackState { get; private set; }
        #endregion
        #region Attack Transition States
        public PlayerAttackTransition FirstAttackTransition { get; private set; }
        public PlayerAttackTransition SecondAttackTransition { get; private set; }
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
            m_animatorController.Initialize();
            m_statsHandler.Initialize(this);
            InputHandler = GetComponent<PlayerInputHandler>();
            m_controller.Initialize(this);
            StateMachine = new PlayerStateMachine();
            InitStates();
            StateMachine.Initialize(UnarmedState, IdleState);
            InitCombatStates();

        }
        //Create all player states;
        private void InitStates()
        {
            //ArmedUnarmed states
            UnarmedState    = new PlayerUnarmedState(this);
            ArmedState      = new PlayerArmedState(this);

            //Movement states
            IdleState       = new PlayerIdleState(this);
            MoveState       = new PlayerMoveState(this);
            JumpState       = new PlayerJumpState(this);
            InAirState      = new PlayerInAirState(this);
            LandingState    = new PlayerLandingState(this);
            GrabState       = new PlayerGrabState(this);
            LedgeClimbState = new PlayerLedgeClimbState(this);

            //Super states
            OnLedgeState    = new PlayerOnLedgeState(this);


        }
        private void InitCombatStates()
        {

            FirstAttackState = new PlayerAttackState(this, "First_Attack");
            SecondAttackState = new PlayerAttackState(this, "Second_Attack");
            ThirdAttackState = new PlayerAttackState(this, "Third_Attack");

            FirstAttackTransition = new PlayerAttackTransition(this, "First_Attack_Transition");
            SecondAttackTransition = new PlayerAttackTransition(this, "Second_Attack_Transition");

            FirstAttackTransition.SetNextAttackState(SecondAttackState);
            SecondAttackTransition.SetNextAttackState(ThirdAttackState);


            FirstAttackState.SetTransition(FirstAttackTransition);
            SecondAttackState.SetTransition(SecondAttackTransition);    

        }
        #endregion
    }

}
