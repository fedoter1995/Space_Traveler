using System.Collections;
using System.Security.Policy;
using UnityEngine;

namespace SpaceTraveler.Characters.Controllers
{
    public class MovementController
    {
        private Rigidbody2D rigidbody;
        private Vector2 auxiliaryVector;
        
        
        public int Direction { get; private set; } = 1;
        public bool CanSetVelocity { get; set; } = true;
        public Vector2 CurrentVelocity { get; private set; }



        public MovementController(Rigidbody2D rigidbody)
        {
            this.rigidbody = rigidbody;
        }
        public void Move(float velocity)
        {
            Debug.Log(velocity);
            Debug.Log(Direction);
            auxiliaryVector.Set(velocity * Direction, CurrentVelocity.y);
            SetFinalVelocity();
        }
        public void SetVelocityZero()
        {
            
            auxiliaryVector = Vector2.zero;
            SetFinalVelocity();
        }
        public void Jump(float jumpVector)
        {
            auxiliaryVector.Set(CurrentVelocity.x, jumpVector);
            SetFinalVelocity();
        }
        public void Flip()
        {
            Direction *= -1;
            rigidbody.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        public void UpdateLogic()
        {
            CurrentVelocity = rigidbody.velocity;
        }
        private void SetFinalVelocity()
        {
            if (CanSetVelocity)
            {
                rigidbody.velocity = auxiliaryVector;
                CurrentVelocity = auxiliaryVector;
            }
        }
    }
}