using System.Collections.Generic;
using UnityEngine;
using Stats;
using System;
using GameStructures.Hit;
using GameStructures.Stats;

public abstract class Projectile : MonoBehaviour, IDoingHit, IPoolsObject<Projectile>
{
    [SerializeField]
    protected float _lifetime = 1f;
    [SerializeField]
    protected List<StatModifier> _modifiers;

    protected HitStats hitStats;
    protected ProjSettings settings;

    public event Action<Projectile> OnDisableEvent;

    protected virtual void OnTriggerEnter2D(Collider2D colision)
    {
        var ship = colision.GetComponent<Spaceship>();
        if (ship != null)
            return;

        var target = colision.GetComponent<ITakeHit>();
        if (target != null)
            Hit(target);

    }
    public abstract void Initialize(ProjSettings settings, HitStats stats);
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

    public void Hit(ITakeHit target)
    {
        var hit = new Hit(hitStats);
        target.TakeHit(hit);
        SetActive(false);
    }
}
public struct ProjSettings
{
    public Vector2 Dirrection { get; }
    public float Speed { get; }

    public ProjSettings(Vector2 dirrection, float speed)
    {
        Dirrection = dirrection;
        Speed = speed;
    }
}