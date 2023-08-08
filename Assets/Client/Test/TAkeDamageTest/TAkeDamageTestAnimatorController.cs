using GameStructures.Hits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAkeDamageTestAnimatorController : AnimatorController
{
    private int IntHurt = Animator.StringToHash("Hurt");
    private TakeDamageHandler handler;

    private void Awake()
    {
        handler = GetComponent<TakeDamageHandler>();

        handler.OnTakeHitEvent += OnTakeHit;
    }

    private void OnTakeHit()
    {
        SetTrigger(IntHurt);
    }
}