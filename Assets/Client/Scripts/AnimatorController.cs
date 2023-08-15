using UnityEngine;

public abstract class AnimatorController : MonoBehaviour
{
    [SerializeField]
    protected Animator _animator;


    protected void SetBool(int hash, bool bolean)
    {
        _animator.SetBool(hash, bolean);
    }
    protected void SetInt(int hash, int iteger)
    {
        _animator.SetInteger(hash, iteger);
    }

    protected void SetFloat(int hash, float value)
    {
        _animator.SetFloat(hash, value);
    }

    protected void SetTrigger(int hash)
    {
        _animator.SetTrigger(hash);
    }

}
