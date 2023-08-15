using CustomTools;
using System.Collections.Generic;
using UnityEngine;

public class StatsDescriptionUI : MonoBehaviour
{
    [SerializeField]
    private StatsDescriptionObject _prefab;
    [SerializeField]
    private int _count = 1;
    [SerializeField]
    private bool _autoExpand = true;
    private Pool<StatsDescriptionObject> objectsPool;

    private void Awake()
    {
        objectsPool = new Pool<StatsDescriptionObject>(_prefab, _count, transform, _autoExpand);   
    }

    public void SetObjects(List<string> descriptions)
    {
        objectsPool.HideObjects();

        foreach(string decription in descriptions)
        {
            var obj = objectsPool.GetFreeObject();
            SetObject(obj, decription);
        }
    }

    private void SetObject(StatsDescriptionObject obj, string description)
    {
        obj.text = description;
    }



}
