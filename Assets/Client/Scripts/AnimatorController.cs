using UnityEngine;

public abstract class AnimatorController : MonoBehaviour
{
    [SerializeField]
    protected Animator _animator;

    public virtual void SetBool(int hash, bool bolean)
    {
        _animator.SetBool(hash, bolean);
    }
    public virtual void SetInt(int hash, int iteger)
    {
        _animator.SetInteger(hash, iteger);
    }

    public virtual void SetFloat(int hash, float value)
    {
        _animator.SetFloat(hash, value);
    }

    public virtual void SetTrigger(int hash)
    {
        _animator.SetTrigger(hash);
    }

}
