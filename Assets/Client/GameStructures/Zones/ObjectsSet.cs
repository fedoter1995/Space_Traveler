using System.Collections.Generic;
using UnityEngine;


namespace GameStructures.Zones
{
    public class ObjectsSet<T>
    {

        private List<T> objects;

        public List<T> Objects => objects;

        public ObjectsSet()
        {
            objects = new List<T>();
        }

        public void AddObject(T obj)
        {
            if (objects.Contains(obj))
                return;

            objects.Add(obj);
        }
        public void RemoveObject(T obj)
        {
            if (objects.Contains(obj))
                objects.Remove(obj);
        }
        public void SetActiveObject(T obj)
        {

        }

    }
}
