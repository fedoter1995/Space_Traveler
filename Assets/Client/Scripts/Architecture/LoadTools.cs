using UnityEngine;

namespace CustomTools
{
    public static class LoadTools
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
