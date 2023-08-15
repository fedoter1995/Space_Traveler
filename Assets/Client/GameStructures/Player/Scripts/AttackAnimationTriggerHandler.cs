using System;
using UnityEngine;

public class AttackAnimationTriggerHandler : MonoBehaviour
{
    private Animator animator;


    public event Action<int> OnAttackTriggerEvent;

    public void EndAttackTrigger(int attackId)
    {
        OnAttackTriggerEvent?.Invoke(attackId);
    }
}
