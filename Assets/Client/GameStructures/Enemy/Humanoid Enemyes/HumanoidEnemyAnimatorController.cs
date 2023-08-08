using GameStructures.Hits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStructures.Enemy
{
    public class HumanoidEnemyAnimatorController : AnimatorController
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
}
