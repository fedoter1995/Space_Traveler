using System;
using UnityEngine;
namespace SpaceTraveler.GameStructures.Characters
{
    public class CharacterAnimationEventsHandler : MonoBehaviour
    {
        #region Events
        public event Action EndGrabEvent;
        public event Action<int> EndAttackEvent;
        public event Action<int> DamageTriggeredEvent;
        public event Action<int> BeginAttackEvent;
        public event Action EndTransitionEvent;
        public event Action EndJumpEvent;
        public event Action EndLandingEvent;
        public event Action EndClimbEvent;
        public event Action StepEvent;
        #endregion
        
        public void OnEndAttack(int attackId) => EndAttackEvent?.Invoke(attackId);
        public void OnDamageTriggered(int attackId) => DamageTriggeredEvent?.Invoke(attackId);
        public void OnBeginAttack(int attackId) => BeginAttackEvent?.Invoke(attackId);
        public void OnEndTransition() => EndTransitionEvent?.Invoke();


        public void OnStep() => StepEvent?.Invoke();
        public void OnEndJump() => EndJumpEvent?.Invoke();
        public void OnEndLanding() => EndLandingEvent?.Invoke();
        public void OnEndClimb() => EndClimbEvent?.Invoke();
        public void OnEndGrab() => EndGrabEvent?.Invoke();
    }

}
