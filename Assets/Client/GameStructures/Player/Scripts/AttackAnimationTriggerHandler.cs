using System;
using System.Collections;
using System.Collections.Generic;
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
