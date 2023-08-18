using System;
using UnityEngine;
namespace GameStructures.Enemys
{
    public class SpaceEnemyAnimatorController : AnimatorController
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
