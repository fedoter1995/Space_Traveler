using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Zones
{
    public class EnemyTriggerZone : TriggerZone
    {

        private ITriggerObject activeObject;

        public event Action OnChangeStateEvent;
        public ITriggerObject ActiveObject => activeObject;
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

        public void UpdateActiveObject()
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


        protected override void AddObject(ITriggerObject obj)
        {
            base.AddObject(obj);
            UpdateActiveObject();       
        }
        protected override void RemoveObject(ITriggerObject obj)
        {
            base.RemoveObject(obj);
            UpdateActiveObject();
        }
        
    }

    public enum TriggerObjectType
    {
        Player,
        Asteroid,
        Enemy,
        InteractiveObject,
        TakeHitObject
    }
}

