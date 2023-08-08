using GameStructures.Zones;
using System;
using System.Collections;
using UnityEngine;

namespace GameStructures.Hits
{
    public abstract class TakeHitHandler : MonoBehaviour, ITakeHit
    {
        [SerializeField]
        private TriggerObjectType _triggerType;
        public IHaveTakeHitHandler Obj { get; protected set; }

        public TriggerObjectType Type => _triggerType;

        public Vector3 Position => transform.position;

        public abstract event Action OnTakeHitEvent;

        public abstract void TakeHit(object sender, Hit hit);
    }
}