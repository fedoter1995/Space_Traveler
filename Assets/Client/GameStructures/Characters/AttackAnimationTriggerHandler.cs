using System;
using UnityEngine;
namespace SpaceTraveler.GameStructures.Characters
{
    public class AttackAnimationTriggerHandler : MonoBehaviour
    {
        private Animator animator;


        public event Action<int> OnAttackTriggerEvent;

        public void EndAttackTrigger(int attackId)
        {
            OnAttackTriggerEvent?.Invoke(attackId);
        }
    }

}
