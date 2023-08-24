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


        public Vector2 CurrentVelocity {  get; private set; }   

        public void Initialize(ActorStatsHandler actorStatsHandler)
        {
            this.actorStatsHandler = actorStatsHandler;
        }

        public void Move(int dirrection)
        {
            Debug.Log(actorStatsHandler.MoveSpeed);
            var moveVector = new Vector2(dirrection * actorStatsHandler.MoveSpeed, 0);
            Debug.Log(moveVector);
            transform.Translate(moveVector * Time.fixedDeltaTime, Space.World);
        }
        public void Flip(int dirrection)
        {
            if (dirrection != 0 && dirrection != transform.localScale.x)
                transform.localScale = new Vector3(dirrection, 1, 1);
        }
    }
}