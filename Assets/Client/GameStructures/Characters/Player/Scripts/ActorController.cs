using SpaceTraveler.Audio;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.Scripts;
using System;
using System.Drawing.Text;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D),typeof(ActorCombatController))]
    public class ActorController : MonoBehaviour
    {
        [SerializeField]
        private ActorCombatController _actorCombatController;
        [SerializeField]
        private CharacteGroundCheckHandler _groundCheckHandler;



        private bool isInitialize = false;

        private Rigidbody2D rb;

        Actor actor;
        private IActorInputManager inputManager;
        private ActorStatsHandler stats => actor.StatsHandler;

        private bool isMove = false;
        private ActorStance stance = ActorStance.Unarmed;
        private BlockState blockState = BlockState.BlockDisable;
        private Vector2 moveVector = Vector2.zero;



        public ActorCombatController CombatController => _actorCombatController;
        public CharacteGroundCheckHandler GroundCheckHandler => _groundCheckHandler;

        #region Events
        //Movement events
        public event Action<bool> OnMoveStateChangeEvent;
        public event Action JumpEvent;
        //Combat events
        public event Action<AddedModifiers> DealDamageEvent;
        public event Action Attack1Event;
        public event Action Attack2Event;
        public event Action<BlockState> BlockStateChangeEvent;
        public event Action<ActorStance> ChangeStanceEvent;
        #endregion

        public bool IsMove
        { 
            get 
            { 
                return isMove;
            }
            set
            {
                isMove = value;
                OnMoveStateChangeEvent.Invoke(isMove);
            }
        }
        public bool IsRunning
        {
            get
            { 
                return inputManager.IsRun && IsMove;
            }
        }
        public bool IsAttack { get; set; } = false;
        public bool OnGround => _groundCheckHandler.OnGround;

        private void Update()
        {
            if(isInitialize)
            {
                CheckInputs();
            }

        }
           
        private void FixedUpdate()
        {
            if (isInitialize && inputManager.IsMove && !IsAttack)
                ActorMovement();
        }
        public void Initialize(IActorInputManager manager, Actor actor)
        {
            this.actor = actor;
            _actorCombatController.Initialize(actor);


            inputManager = manager;
            rb = GetComponent<Rigidbody2D>();
            isInitialize = true;

        }

        public void OnEndAttackTriggered(int attackId)
        {
            _actorCombatController.OnEndAttackTrigger(attackId);
        }
        public AudioClip GetSlashAudioClip(int attackId)
        {
            return _actorCombatController.GetAudioClip(attackId);
        }
        private void ActorMovement()
        {
            float x = inputManager.MoveVector.x;
            if (x != 0 && x != transform.localScale.x)
                transform.localScale = new Vector3(inputManager.MoveVector.normalized.x, 1, 1);

            transform.Translate(moveVector * Time.fixedDeltaTime, Space.World);

        }
        private void ActorJump()
        {

            if (!OnGround)
                return;

            JumpEvent?.Invoke();
            var jumpVector = Vector2.up * 300f + inputManager.MoveVector * 150f;
            rb.AddForce(jumpVector);
        }
        private void CheckInputs()
        {

            if(inputManager.IsMove != IsMove)
                IsMove = inputManager.IsMove;


            if (Input.GetKeyDown(inputManager.ChangeStanceButton))
            {
                ChancgeStance();
            }

            if (OnGround)
            {
                if (Input.GetKeyDown(inputManager.AttackButton1))
                    OnAttack1Button();
                if (Input.GetKeyDown(inputManager.AttackButton2))
                    OnAttack2Button();

                if (Input.GetKey(inputManager.BlockButton))
                    OnBlockStateChange(BlockState.BlockEnable);
                if (Input.GetKeyUp(inputManager.BlockButton))
                    OnBlockStateChange(BlockState.BlockDisable);

                if (inputManager.Jump)
                    ActorJump();
                if(blockState == BlockState.BlockDisable)
                {
                    if (IsMove)
                    {
                        moveVector = inputManager.MoveVector * stats.MoveSpeed;
                        return;
                    }
                }


            }

            moveVector = Vector2.zero;
        }
        private void OnAttack1Button()
        {
            if (stance == ActorStance.Unarmed)
                ChancgeStance();

            Attack1Event?.Invoke();
        }
        private void OnAttack2Button()
        {
            if (stance == ActorStance.Unarmed)
                ChancgeStance();

            Attack2Event?.Invoke();
        }
        private void OnBlockStateChange(BlockState blockState)
        {
            if (stance == ActorStance.Unarmed)           
                ChancgeStance();
            
            if (this.blockState == blockState)
                return;


            this.blockState = blockState;
            BlockStateChangeEvent?.Invoke(this.blockState);
        }
        private void ChancgeStance()
        {
            if (stance == ActorStance.Unarmed)
                stance = ActorStance.Armed;
            else if (stance == ActorStance.Armed)
                stance = ActorStance.Unarmed;

            ChangeStanceEvent.Invoke(stance);
        }
        
    }
    public enum ActorStance
    {
        Unarmed = 0,
        Armed = 1,

    }
    public enum BlockState
    {
        BlockEnable,
        BlockDisable

    }

}