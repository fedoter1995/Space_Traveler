using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using System;

public abstract class Projectile : MonoBehaviour, IDoingDamage, IPoolsObject<Projectile>
{
    [SerializeField]
    protected float _lifetime = 1f;
    [SerializeField]
    protected List<StatModifier> _modifiers;

    protected ShotStats stats;

    public event Action<Projectile> OnDisableEvent;

    protected virtual void OnTriggerEnter2D(Collider2D colision)
    {
        var ship = colision.GetComponent<Spaceship>();
        if (ship != null)
            return;

        var target = colision.GetComponent<ITakeDamage>();
        if (target != null)
            DealDamage(target);

    }
    public abstract void Initialize(ShotStats stats);
    public abstract void Move();
    protected virtual void SetActive(bool activity)
    {
        OnDisableEvent?.Invoke(this);
        gameObject.SetActive(activity);
    }
    public virtual List<StatModifier> GetAllModifiers()
    {
        return new List<StatModifier>(_modifiers);
    }
    public virtual void DealDamage(ITakeDamage target)
    {
        target.TakeDamage(stats.ShotDamage);
        SetActive(false);
    }
}
