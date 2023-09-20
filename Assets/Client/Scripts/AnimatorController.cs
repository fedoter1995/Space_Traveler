using SpaceTraveler.GameStructures.Characters;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public abstract class AnimatorController : MonoBehaviour
{
    protected Animator animator;

    public void Play(int stateHashName)
    {
        animator.Play(stateHashName);
    }
    public void Play(string stateName)
    {
        animator.Play(stateName);
    }
    public virtual void Initialize()
    {
        animator = GetComponent<Animator>();
    }
    public virtual void SetBool(int hash, bool bolean)
    {
        animator.SetBool(hash, bolean);
    }
    public virtual void SetInt(int hash, int iteger)
    {
        animator.SetInteger(hash, iteger);
    }

    public virtual void SetFloat(int hash, float value)
    {
        animator.SetFloat(hash, value);
    }

    public virtual void SetTrigger(int hash)
    {
        animator.SetTrigger(hash);
    }

}
