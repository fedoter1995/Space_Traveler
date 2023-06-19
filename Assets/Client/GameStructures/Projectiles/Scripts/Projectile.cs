using System.Collections.Generic;
using UnityEngine;
using System;
using GameStructures.Hits;
using GameStructures.Stats;

public abstract class Projectile : MonoBehaviour, IDoingHit, IPoolsObject<Projectile>
{
    [SerializeField]
    protected float _lifetime = 1f;
    [SerializeField]
    protected List<StatModifier> _modifiers;

    protected HitStats hitStats;
    protected ProjSettings settings;
    protected object sender;

    public Action<Projectile> OnDisableObject { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        var target = collision.GetComponentInParent<ITakeHit>();

        if (target == null)
            target = collision.GetComponent<ITakeHit>();

        if (target != null && target.Obj != sender)
        {
            Hit(target);
            return;
        }
    }
    public abstract void Initialize(object sender, ProjSettings settings, HitStats stats);
    public abstract void Move();
    protected virtual void SetActive(bool activity)
    {
        OnDisableObject?.Invoke(this);
        gameObject.SetActive(activity);
    }
    public virtual List<StatModifier> GetAllModifiers()
    {
        return new List<StatModifier>(_modifiers);
    }
    public void Hit(ITakeHit target)
    {
        var hit = new Hit(hitStats);

        target.TakeHit(sender, hit);

        if(hitStats.PenetrationsNumb > 0)
            hitStats.OnHit();
        else
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