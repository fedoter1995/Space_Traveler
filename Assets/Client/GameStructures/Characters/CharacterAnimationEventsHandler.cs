using System;
using UnityEngine;
namespace SpaceTraveler.GameStructures.Characters
{
    public class CharacterAnimationEventsHandler : MonoBehaviour
    {

        public event Action<int> EndAttackTriggerEvent;
        public event Action<int> BeginAttackTriggerEvent;
        public event Action StepEvent;
        public void EndAttackTrigger(int attackId)
        {
            EndAttackTriggerEvent?.Invoke(attackId);
        }
        public void BeginAttackTrigger(int attackId)
        {
            BeginAttackTriggerEvent?.Invoke(attackId);
        }
        public void OnStep()
        {
            StepEvent?.Invoke();
        }
    }

}
