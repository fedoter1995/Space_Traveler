using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomTools
{
    public class MyTools
    {
       
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
        public static GameObject Create(string name, Vector3 position, Quaternion rotation)
        {
            var go = new GameObject(name);
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go;
        }       
        public static Dictionary<T, B> JObjectToDict<T, B>(JObject obj)
        {
            var dict = obj.ToObject<Dictionary<T, B>>();

            return dict;
        }
        public static List<T> JArrayToList<T>(JArray array)
        {
            var list = array.ToObject<List<T>>();

            return list;
        }

    }
}
