using SpaceTraveler.Characters.Controllers;
using SpaceTraveler.GameStructures.Characters;
using SpaceTraveler.GameStructures.Characters.Player;
using System;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor
{
    [RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private CharacterSurfaceCheckHandler _surfaceCheckHandler;
        [SerializeField]
        private Vector2 climbStartOffset = Vector2.zero;
        [SerializeField]
        private Vector2 climbEndOffset = Vector2.zero;
        private ActorStatsHandler actorStatsHandler;
        private MovementController movementController;

        private Rigidbody2D rb;

        public float YVelocity => rb.velocity.y;
        public PlayerInputHandler InputHandler { get; private set; }
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

        private void Update()
        {
            movementController.UpdateLogic();
        }
        public void Initialize(ActorStatsHandler actorStatsHandler, PlayerInputHandler playerInputHandler)
        {
            this.InputHandler = playerInputHandler;
            this.actorStatsHandler = actorStatsHandler;
            rb = GetComponent<Rigidbody2D>();
            movementController = new MovementController(rb);
        }
        public void Move()
        {
            movementController.Move(actorStatsHandler.MoveSpeed);
        }
        public void Jump()
        {
            movementController.Jump(actorStatsHandler.JumpPower);
        }
        public void Flip()
        {
            movementController.Flip();
        }
        public void SetBodyType(RigidbodyType2D bodyType)
        {
            rb.bodyType = bodyType;
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