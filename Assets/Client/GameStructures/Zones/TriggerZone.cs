using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Zones
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class TriggerZone : MonoBehaviour
    {

        [SerializeField]
        private CircleCollider2D triggerCollider;
        [SerializeField]
        private List<TriggerObjectType> _correctTypes = new List<TriggerObjectType>();



        private List<ITriggerObject> inZoneObjects = new List<ITriggerObject>();
        private ITriggerObject activeObject;
        List<TriggerObjectType> CorrectTypes => _correctTypes;
        public event Action OnChangeStateEvent;
        public ITriggerObject ActiveObject => activeObject;
        public float Radius => triggerCollider.radius;
        public Vector3 HeadingToActiveObject
        {
            get
            {
                return activeObject.Position - transform.position;
            }
        }
        public Vector3 ActiveObjectPosition
        {
            get
            {
                return ActiveObject.Position;
            }
        }

        public void Initialize(float radius)
        {
            SetRadius(radius);
        }
        public void SetRadius(float radius)
        {
            triggerCollider.radius = radius;
            UpdateObjects();
        }
        public void UpdateObjects()
        {
            if(inZoneObjects.Count == 0)
            {
                activeObject = null;
            }
            else
            {
                foreach (ITriggerObject obj in inZoneObjects)
                {
                    if(activeObject != null)
                    {
                        var headingToObj = obj.Position - transform.position;

                        var headingToActiveObject = transform.position - activeObject.Position;

                        if (headingToObj.sqrMagnitude < headingToActiveObject.sqrMagnitude)
                        {
                            activeObject = obj;
                        }
                    }
                    else
                    {
                        activeObject = obj;
                    }
                }
            }

            OnChangeStateEvent?.Invoke();
        }
        public bool HaveObjectInZone(ITriggerObject obj)
        {
            return inZoneObjects.Contains(obj);
        }

        private void AddObject(ITriggerObject obj)
        {
            if (inZoneObjects.Contains(obj))
                return;

            inZoneObjects.Add(obj);
            Debug.Log(obj);

            UpdateObjects();
            
        }
        private void RemoveObject(ITriggerObject obj)
        {
            if (inZoneObjects.Contains(obj))
            {
                inZoneObjects.Remove(obj);
                UpdateObjects();
            }

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

    public enum TriggerObjectType
    {
        Player,
        Asteroid,
        Enemy,
        InteractiveObject,
    }
}

