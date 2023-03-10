using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Architecture;

public class ItemsRepository : Repository
{
    private ItemDataBase database;

    public override void OnInitialize()
    {
        ResourcesLoad();
    }
    private void ResourcesLoad()
    {
        var objectdata = Game.saveController.Load(ToString());
        SetObjectData(objectdata);
    }
    

    public T GetItem<T>(string id) where T : Item
    {
        return database.GetItem<T>(id);
    }

    public override Dictionary<string, object> GetObjectData()
    {
        var data = new Dictionary<string, object>();
        data.Add("Items_Data_Base_Name", database.Name);

        return data;
    }

    public override void SetObjectData(Dictionary<string, object> obj)
    {
        if(obj != null)
        {
            string name = obj["Items_Data_Base_Name"].ToString();
            var databases = Resources.LoadAll<ItemDataBase>("Database");
            foreach(ItemDataBase database in databases)
            {
                if (database.Name == name)
                    this.database = database; 
            }
        }
        else
        {
            database = Resources.Load<ItemDataBase>("Database/DefaultDataBase");
            Debug.Log(database);
        }

    }
}
