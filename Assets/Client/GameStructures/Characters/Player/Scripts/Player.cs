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
        private Animator _animatorController;
        [SerializeField]
        private PlayerController _controller;


        public Animator AnimatorController => _animatorController;
        public PlayerStateMachine StateMachine { get; private set; }
        public ActorStatsHandler StatsHandler => _statsHandler;
        public PlayerInputHandler InputHandler {  get; private set; }
        public PlayerController Controller => _controller;

        public Vector2 CurrentVelocity { get; private set; }

        #region Player states
        public PlayerMoveState UnarmedMoveState { get; private set; }
        public PlayerIdleState UnarmedIdleState { get; private set; }
        #endregion
        private void Awake()
        {
            Initialize();
        }
        public void Initialize()
        {
            InputHandler = GetComponent<PlayerInputHandler>();
            StateMachine = new PlayerStateMachine();
            _statsHandler.Initialize(this);
            _controller.Initialize(_statsHandler);
            InitStates();
            StateMachine.Initialize(UnarmedIdleState);

        }

        private void Update()
        {
            StateMachine.CurrentState.UpdateLogick();
        }
        private void FixedUpdate()
        {
            StateMachine.CurrentState.UpdatePhysics();
        }
        private void InitStates()
        {
            UnarmedIdleState = new PlayerIdleState(this, StateMachine);
            UnarmedMoveState = new PlayerMoveState(this, StateMachine);
        }
        public void Move(float moveFloat)
        {
            var moveVector = new Vector2(moveFloat, 0);
            if (moveFloat != 0 && moveFloat != transform.localScale.x)
                transform.localScale = new Vector3(moveFloat, 1, 1);

            transform.Translate(moveVector * Time.fixedDeltaTime, Space.World);
        }
    }

}
