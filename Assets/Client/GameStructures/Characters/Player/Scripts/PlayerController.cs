using SpaceTraveler.Characters.Controllers;
using SpaceTraveler.GameStructures.Characters;
using SpaceTraveler.GameStructures.Characters.Player;
using System;
using UnityEngine;

namespace SpaceTraveler.Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerCombatController m_combatController;
        [SerializeField]
        private CharacterSurfaceCheckHandler _surfaceCheckHandler;
        [SerializeField]
        private Vector2 climbStartOffset = Vector2.zero;
        [SerializeField]
        private Vector2 climbEndOffset = Vector2.zero;
    
        private PlayerStatsHandler statsHandler;
        private MovementController movementController;

        private Rigidbody2D rb;

        public float YVelocity => rb.velocity.y;
        public PlayerInputHandler InputHandler { get; private set; }
        public PlayerCombatController CombatController => m_combatController;
        public CharacterSurfaceCheckHandler SurfaceCheckHandler => _surfaceCheckHandler;

        #region Events
        public event Action OnEndLandingEvent;
        #endregion

        public Vector2 ClimbStartOffset
        {
            get
            {
                var newVector = new Vector2(Dirrection * climbStartOffset.x, climbStartOffset.y);
                return newVector;
            }
        }
        public Vector2 ClimbEndOffset
        {
            get
            {
                var newVector = new Vector2(Dirrection * climbEndOffset.x, climbEndOffset.y);
                return newVector;
            }
        }
        public bool OnGround => _surfaceCheckHandler.OnGround;
        public int MoveX => InputHandler.MoveX;
        public int MoveY => InputHandler.MoveY;
        public float CurrentVelocityY => movementController.CurrentVelocity.y;
        public float CurrentVelocityX => movementController.CurrentVelocity.x;
        public int Dirrection => movementController.Direction;
        public Rigidbody2D Rigidbody => rb;

        private void Update()
        {
            movementController.UpdateLogic();
        }
        public void Initialize(Player player)
        {
            m_combatController.Initialize(player);
            this.InputHandler = player.InputHandler;
            this.statsHandler = player.StatsHandler;

            rb = GetComponent<Rigidbody2D>();
            movementController = new MovementController(rb);
        }
        public void Move()
        {
            movementController.Move(statsHandler.MoveSpeed);
        }
        public void Jump()
        {
            movementController.Jump(statsHandler.JumpPower);
        }
        public void Flip()
        {
            movementController.Flip();
        }
        public void SetBodyType(RigidbodyType2D bodyType)
        {
            rb.bodyType = bodyType;
        }
        public void TriggeredAttack(int attackId)
        {
            m_combatController.TriggeredAttack(attackId);
        }
        public Vector2 DetermineCornerPosition()
        {
            return SurfaceCheckHandler.DetermineCornerPosition(Dirrection);
        }

        public void SetVelocityZero()
        {
            movementController.SetVelocityZero();
        }
    }
}