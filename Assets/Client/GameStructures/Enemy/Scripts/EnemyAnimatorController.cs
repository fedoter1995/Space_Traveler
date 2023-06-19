using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameStructures.Enemy
{
    public class EnemyAnimatorController : AnimatorController
    {
        #region Hash Animator Var
        private int IntIsDestroyed = Animator.StringToHash("IsDestroyed");
        #endregion

        public event Action<bool> OnEndDestoryAnimationEvent;


        public void PlayDestroyAnimation()
        {
            SetBool(IntIsDestroyed, true);
        }


        public void OnEndDestoryAnimation()
        {
            OnEndDestoryAnimationEvent?.Invoke(false);
        }

    }

}
