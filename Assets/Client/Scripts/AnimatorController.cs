using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatorController : MonoBehaviour
{
    [SerializeField]
    protected Animator _animator;


    protected void SetBool(int hash, bool bolean)
    {
        _animator.SetBool(hash, bolean);
    }

    protected void SetFloat(int hash, float value)
    {
        _animator.SetFloat(hash, value);
    }

}
