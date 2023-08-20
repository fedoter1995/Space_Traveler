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
        private Transform _groundCheckObj;

        private bool isInitialize = false;
        private IActorInputManager inputManager;
        private float rayDistance = 0.07f;
        private Rigidbody2D rb;
        private ActorCombatController combatController => actor.CombatController; 
        private ActorStatsHandler Stats => actor.StatsHandler;
        private Actor actor; 
        private Coroutine CheckGroundEnumerator = null;
        private int layer;

        private bool onGround = true;
        private bool isMove = false;
        private bool isFall = false;
        private ActorStance stance = ActorStance.Unarmed;
        private BlockState blockState = BlockState.BlockDisable;
        private Vector2 moveVector = Vector2.zero;
        private int currentGroundLayer;


        #region Events
        public event Action<GroundSettings> GroundTypeChangeEvent;
        public event Action<bool> OnGroundStateChangeEvent;
        public event Action<bool> OnMoveStateChangeEvent;
        public event Action JumpEvent;
        public event Action LandingEvent;
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
        public bool OnGround
        {
            get { return onGround; }
            set 
            {
                if(onGround != value && value)
                {
                    ActorLanding();
                }
                
                onGround = value;
                OnGroundStateChangeEvent.Invoke(onGround);
            }
        }

        private void Update()
        {
            if(isInitialize)
            {
                CheckGround();

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
            inputManager = manager;
            layer = LayerMask.GetMask("Wood_Ground","Metal_Ground");
            rb = GetComponent<Rigidbody2D>();
            isInitialize = true;

        }
        public void OnAttackTriggered(int attackId)
        {
            combatController.OnAttackTrigger(attackId);
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
        private void ActorLanding ()
        {
            LandingEvent?.Invoke();
            StopAllCoroutines();
            CheckGroundEnumerator = null;
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
                        moveVector = inputManager.MoveVector * Stats.MoveSpeed;
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

        private void CheckGround()
        {
            //var hit = Physics2D.Raycast(rb.position, Vector2.down, rayDistance, layer);
            var hit = Physics2D.OverlapCircle(_groundCheckObj.position, rayDistance, layer);
            
            if(hit)
            {
                if(currentGroundLayer != hit.gameObject.layer)
                {
                    currentGroundLayer = hit.gameObject.layer;
                    GroundTypeChangeEvent?.Invoke(hit.GetComponent<GroundSettings>());
                }
            }


            if (hit != onGround)
            {
                OnGround = hit;
            }
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