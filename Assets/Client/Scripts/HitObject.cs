using GameStructures.Hits;
using GameStructures.Zones;
using System;
using UnityEngine;

public class HitObject : MonoBehaviour, ITakeHit
{
    private ITakeHit mainHitObject;
    public Vector3 Position => transform.position;

    public IHaveTakeHitHandler Obj => throw new NotImplementedException();

    public TriggerObjectType Type => throw new NotImplementedException();

    public event Action OnTakeHitEvent;

    private void Awake()
    {
        mainHitObject = GetComponentInParent<ITakeHit>();
        if (mainHitObject == null)
            Debug.LogError("Main HitObject not found");
    }


    public void TakeHit(object sender, Hit hit)
    {
        mainHitObject.TakeHit(sender, hit);
    }

}
