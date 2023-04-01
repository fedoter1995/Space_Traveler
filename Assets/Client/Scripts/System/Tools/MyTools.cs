using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomTools
{
    public class CustomConvert
    {     
        public static Dictionary<T, B> JObjectToDict<T, B>(JObject obj)
        {
            var dict = obj.ToObject<Dictionary<T, B>>();

            return dict;
        }
        public static Vector3 JObjectToVector3(JObject obj)
        {
            var vector3 = obj.ToObject<Vector3>();

            return vector3;
        }
        public static T JObjectToObject<T>(JObject obj)
        {
            var tObj = obj.ToObject<T>();

            return tObj;
        }
        public static List<T> JArrayToList<T>(JArray array)
        {
            var list = array.ToObject<List<T>>();

            return list;
        }

    }
    public class CustomLoad
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
    }
}
