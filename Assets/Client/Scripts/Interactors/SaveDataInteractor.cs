using Architecture;
using Architecture.Saves;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataInteractor : Interactor
{
    private const string  SAVE_PATH = "Save";

    private SaveData save;
    private System.DateTime time;
    private List<IJsonSerializable> serializableObjects;
    public override void OnCreate()
    {
        save = JsonSerealization<SaveData>.Deserialize("Save");

        if (save is null)
            save = new SaveData();
    }

    public override void OnInitialize()
    {
        GetSerializableObjects();
    }
    public void Save()
    {
        save.SetObjectData(GetObjectsData());
        JsonSerealization<SaveData>.Serialize(save, "Save");
    }
    public Dictionary<string, object> Load(string Key)
    {
        return save.GetObjectData(Key);
    }
    private void GetSerializableObjects()
    {
        serializableObjects = new List<IJsonSerializable>(Game.GetSerializableObjects());
    }
    private Dictionary<string, Dictionary<string, object>> GetObjectsData()
    {
        var saveData = new Dictionary<string, Dictionary<string, object>>();
        foreach (IJsonSerializable obj in serializableObjects)
        {
            var data = obj.GetObjectData();
            var key = obj.ToString();
            saveData.Add(key, data);
        }
        return saveData;
    }

    public override Dictionary<string, object> GetObjectData()
    {

        var dict = new Dictionary<string, object>();

        dict.Add("Time", System.DateTime.Now);

        return dict;
    }

    public override void SetObjectData(Dictionary<string, object> obj)
    {
        if(obj != null)
        {
            JObject jobj = (JObject)obj["Time"];
            System.DateTime newTime = jobj.ToObject<System.DateTime>();

            time = newTime;
        }
        time = System.DateTime.Now;
    }

}
