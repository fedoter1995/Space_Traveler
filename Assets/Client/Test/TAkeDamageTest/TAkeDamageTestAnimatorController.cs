
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Stats;
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

    private void OnTakeHit(HitStats stats)
    {
        SetTrigger(IntHurt);
    }
}
