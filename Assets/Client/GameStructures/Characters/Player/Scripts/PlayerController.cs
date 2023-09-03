using SpaceTraveler.Characters.Actor.ActorFiniteStateMachine;
using SpaceTraveler.GameStructures.Characters;
using SpaceTraveler.GameStructures.Characters.Player;
using System;
using System.Collections;
using System.Collections.Generic;
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
        public bool Landing { get; private set; } = false;
        public int Dirrection { get; private set; } = 1;
        public void Initialize(ActorStatsHandler actorStatsHandler, PlayerInputHandler playerInputHandler)
        {
            this.InputHandler = playerInputHandler;
            this.actorStatsHandler = actorStatsHandler;
            rb = GetComponent<Rigidbody2D>();
        }

        public void Move()
        {
            var moveVector = new Vector2(MoveX * actorStatsHandler.MoveSpeed, 0);
            transform.Translate(moveVector * Time.fixedDeltaTime, Space.World);
        }
        public void Flip()
        {
            if (MoveX != 0 && MoveX != Dirrection)
            {
                Dirrection = MoveX;
                transform.localScale = new Vector3(Dirrection, 1, 1);
            }
        }
        public void Jump(int dirrection)
        {
            var jumpDirrection = new Vector2(dirrection * actorStatsHandler.JumpPower, actorStatsHandler.JumpPower);
            rb.AddForce(jumpDirrection);
        }
        public void EndLanding()
        {
            Landing = false;
        }
        public void SetBodyType(RigidbodyType2D bodyType)
        {
                rb.bodyType = bodyType;
        }

        public Vector2 DetermineCornerPosition()
        {
            return SurfaceCheckHandler.DetermineCornerPosition(Dirrection);
        }
    }
}