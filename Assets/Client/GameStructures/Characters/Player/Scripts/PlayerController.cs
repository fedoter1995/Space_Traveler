using SpaceTraveler.Characters.Actor.ActorFiniteStateMachine;
using SpaceTraveler.GameStructures.Characters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceTraveler.Characters.Actor
{
    public class PlayerController : MonoBehaviour
    {

        protected ActorStatsHandler actorStatsHandler;
        private Rigidbody2D rb;
        private Vector2 workspace;


        public Vector2 CurrentVelocity {  get; private set; }   

        public void Initialize(ActorStatsHandler actorStatsHandler)
        {
            this.actorStatsHandler = actorStatsHandler;
            rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            CurrentVelocity = rb.velocity;
        }
        public void Move(float dirrection)
        {
            if (dirrection != 0 && dirrection != transform.localScale.x)
                transform.localScale = new Vector3(dirrection, 1, 1);

            workspace.Set(dirrection * actorStatsHandler.MoveSpeed, CurrentVelocity.y);
            rb.velocity = workspace;
            CurrentVelocity = rb.velocity;
        }
    }
}