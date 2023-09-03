using System;
using UnityEngine;
namespace SpaceTraveler.GameStructures.Characters
{
    public class CharacterAnimationEventsHandler : MonoBehaviour
    {
        #region Events
        public event Action EndGrabEvent;
        public event Action<int> EndAttackTriggerEvent;
        public event Action<int> BeginAttackTriggerEvent;
        public event Action EndJumpEvent;
        public event Action EndLandingEvent;
        public event Action EndClimbEvent;
        public event Action StepEvent;
        #endregion
        
        public void EndAttackTrigger(int attackId) => EndAttackTriggerEvent?.Invoke(attackId);
        public void BeginAttackTrigger(int attackId) => BeginAttackTriggerEvent?.Invoke(attackId);
        public void OnStep() => StepEvent?.Invoke();
        public void OnEndJump() => EndJumpEvent?.Invoke();
        public void OnEndLanding() => EndLandingEvent?.Invoke();
        public void OnEndClimb() => EndClimbEvent?.Invoke();
        public void OnEndGrab() => EndGrabEvent?.Invoke();
    }

}
