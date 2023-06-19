using Architecture;
using GameStructures.Hits;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ActorController : MonoBehaviour
    {
        [SerializeField]
        private List<ComboElement> _attackRef = new List<ComboElement>();


        private bool isInitialize = false;
        private IActorInputManager inputManager;
        private float rayDistance = 0.1f;
        private Actor actor;
        private Rigidbody2D rb;
        private ActorStatsHandler Stats => (ActorStatsHandler)actor.StatsHandler;

        private Coroutine CheckGroundEnumerator = null;
        private int layer;
        private bool isGround = true;
        private Vector2 moveVector = Vector2.zero;

        public Queue<ComboElement> AttackQueue = new Queue<ComboElement>();

        #region Events
        public event Action OnJumpEvent;
        public event Action OnLandingEvent;
        public event Action<AddedModifiers> OnDealDamageEvent;
        public event Action OnBeginComboEvent;
        #endregion

        public bool IsMoveing
        { 
            get 
            { return moveVector != Vector2.zero; } 
        }
        public bool IsRunning
        {
            get
            { 
                return inputManager.IsRun && IsMoveing;
            }
        }
        public bool IsAttack { get; set; } = false;
        private void Awake()
        {
            layer = LayerMask.GetMask("Ground");
        }
        private void Update()
        {
            CheckInputs();
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
            isInitialize = true;
            rb = GetComponent<Rigidbody2D>();
        }
        public void OnDealDamage()
        {     
            OnDealDamageEvent?.Invoke(AttackQueue.Peek().AddedModifiers);
        }
        private void ActorMovement()
        {
            float x = inputManager.MoveVector.x;
            if (x != 0 && x != transform.localScale.x)
                transform.localScale = new Vector3(inputManager.MoveVector.x, 1, 1);

            transform.Translate(moveVector * Time.fixedDeltaTime, Space.World);

        }
        private void ActorJump()
        {
            OnJumpEvent?.Invoke();
            rb.AddForce(Vector2.up * 300f);
            CheckGroundEnumerator = StartCoroutine(CheckGroundRoutine());
        }
        private void ActorLanding()
        {
            OnLandingEvent?.Invoke();
            StopAllCoroutines();
            CheckGroundEnumerator = null;
        }

        private void CheckInputs()
        {


            if(CheckGroundEnumerator == null)
            {
                if (inputManager.Attack1)
                    OnAttack();

                if (inputManager.Jump)
                    ActorJump();
            }


            if (inputManager.IsMove && inputManager.IsRun)
            {
                moveVector = inputManager.MoveVector.normalized * Stats.MoveSpeed * 2;
                return;
            }

            if (inputManager.IsMove)
            {
                moveVector = inputManager.MoveVector.normalized * Stats.MoveSpeed;
                return;
            }


            moveVector = Vector2.zero;
        }
        private void OnAttack()
        {
            IsAttack = true;

            if (AttackQueue.Count > 0)
            {
                foreach (var item in _attackRef)
                {
                    if (!AttackQueue.Contains(item))
                    {
                        AttackQueue.Enqueue(item);
                        return;
                    }

                }
            }
            else
            {
                AttackQueue.Enqueue(_attackRef[0]);
                OnBeginComboEvent?.Invoke();
            }
        }

        private IEnumerator CheckGroundRoutine()
        {
            yield return new WaitForSeconds(0.1f);

            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, rayDistance, layer);
            while(!hit)
            {
                yield return new WaitForEndOfFrame();
                hit = Physics2D.Raycast(rb.position, Vector2.down, rayDistance, layer);
            }
            ActorLanding();
        }

    }
}