using GameStructures.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidProjectile : Projectile
{

    private Coroutine moveEnumerator = null;

    public override void Initialize(ProjSettings settings, HitStats hitStats)
    {
        this.settings = settings;
        this.hitStats = hitStats;
    }
    public override void Move()
    {
        if (moveEnumerator == null)
            moveEnumerator = StartCoroutine(MoveRoutine());

    }
    private IEnumerator MoveRoutine()
    {
        float deltaTime = 0f;
        while(deltaTime < _lifetime)
        {
            yield return new WaitForFixedUpdate();
            transform.Translate(settings.Dirrection * Time.fixedDeltaTime * settings.Speed, Space.World);
            deltaTime += Time.fixedDeltaTime;
        }
        moveEnumerator = null;
        SetActive(false);
    }
    protected override void SetActive(bool activity)
    {
        moveEnumerator = null;
        base.SetActive(activity);
    }
}

