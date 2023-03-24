using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Architecture;
using Stats;
using GameStructures.Stats;

public class StatsPresetRepository : Repository
{
    private StatPresetDataBase database;

    public override void OnCreate()
    {
        ResourcesLoad();
    }
    private void ResourcesLoad()
    {
        var objectdata = Game.saveController.Load(ToString());
        SetObjectData(objectdata);
    }
    

    public StatPreset GetStatPreset(string id)
    {
        return database.GetStatPreset(id);
    }

    public override Dictionary<string, object> GetObjectData()
    {
        var data = new Dictionary<string, object>();
        data.Add("Stats_Data_Base_Name", database.Name);

        return data;
    }

    public override void SetObjectData(Dictionary<string, object> obj)
    {
        if(obj != null)
        {
            string name = obj["Stats_Data_Base_Name"].ToString();
            database = Resources.Load<StatPresetDataBase>($"Database/{name}");
        }
        else
        {
            database = Resources.Load<StatPresetDataBase>("Database/Default_Stats_Database");
        }
    }
}
