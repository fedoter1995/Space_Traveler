using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomTools
{
    public class MyTools
    {
        public static T GetComponent<T>(GameObject sender)
        {

            var component = sender.GetComponent<T>();
            
            if(component == null)
                throw new System.Exception($"Cant find {typeof(T)} in {sender}");

            return component;
            
        }
        public static T LoadObjectResource<T>(string fileName) where T : Object
        {
            var prefab = Resources.Load<T>(fileName);

            if (prefab == null)
                throw new System.Exception($"Prefab [{fileName}] not found");
            else
                return prefab;
        }
        public static Object LoadObjectResource(string fileName)
        {
            var prefab = Resources.Load(fileName);

            if (prefab == null)
                throw new System.Exception($"Prefab [{fileName}] not found");
            else
                return prefab;
        }
        public static GameObject Create(string name)
        {
            return new GameObject(name);
        }
        public static GameObject Create(string name, Vector3 position, Quaternion rotation)
        {
            var go = new GameObject(name);
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go;
        }
        public static GameObject Create(Object original)
        {
            return Object.Instantiate(original) as GameObject;
        }
        public static GameObject Create(Object original, Vector3 position, Quaternion rotation)
        {
            return Object.Instantiate(original, position, rotation) as GameObject;
        }
        public static GameObject Create(Object original, Transform parent)
        {
            return Object.Instantiate(original, parent) as GameObject;
        }
        public static GameObject Create(Object original, Vector3 position, Quaternion rotation, Transform parent)
        {
            return Object.Instantiate(original, position, rotation, parent) as GameObject;
        }
    }
}
