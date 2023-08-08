using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Zones
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class TriggerZone : MonoBehaviour
    {
        [SerializeField]
        protected Collider2D triggerCollider;
        [SerializeField]
        protected List<TriggerObjectType> _correctTypes = new List<TriggerObjectType>();



        protected List<ITriggerObject> inZoneObjects = new List<ITriggerObject>();


        public List<ITriggerObject> InZoneObjects => inZoneObjects;
        public List<TriggerObjectType> CorrectTypes => _correctTypes;

        public bool HaveObjectInZone(ITriggerObject obj)
        {
            return inZoneObjects.Contains(obj);
        }

        protected virtual void AddObject(ITriggerObject obj)
        {
            if (inZoneObjects.Contains(obj))
                return;

            inZoneObjects.Add(obj);
        }
        protected virtual void RemoveObject(ITriggerObject obj)
        {
            if (inZoneObjects.Contains(obj))           
                inZoneObjects.Remove(obj);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {

            var obj = collision.GetComponent<ITriggerObject>();
            if (obj == null)
                obj = collision.GetComponentInParent<ITriggerObject>();

            if (_correctTypes.Count > 0)
            {
                if (obj != null && CorrectTypes.Contains(obj.Type))
                    AddObject(obj);
            }
            else
            {
                if (obj != null)
                {
                    AddObject(obj);
                }
            }

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            var obj = collision.GetComponent<ITriggerObject>();
            if (obj == null)
                obj = collision.GetComponentInParent<ITriggerObject>();

            if (obj != null)
                RemoveObject(obj);
        }

    }
}