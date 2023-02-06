using System;
using System.Collections.Generic;
using UnityEngine;
using Stats;

public class ShotStats
{
    private ShotDamage damage;
    private float speed;
    private Vector2 shotPos;
    private Vector2 shotDir;
    private Quaternion rotation;
    public ShotDamage ShotDamage => damage;
    public float ShotSpeed => speed;

    public Vector2 ShotPos => shotPos;
    public Vector2 ShotDir => shotDir;
    public Quaternion Rotation => rotation;

    public ShotStats(ShotDamage damage, Transform parrent, float speed)
    {
        shotPos = parrent.position;
        shotDir = parrent.up;
        rotation = parrent.rotation;
        this.damage = damage;
        this.speed = speed;
    }
    public ShotStats(ShotStats stats)
    {
        damage = stats.ShotDamage;
        shotPos = stats.ShotPos;
        shotDir = stats.ShotDir;
        speed = stats.ShotSpeed;
    }
}

