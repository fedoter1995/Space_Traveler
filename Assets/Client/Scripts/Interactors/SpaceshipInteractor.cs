using Architecture;
using CustomTools;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceshipInteractor : Interactor
{
    private List<Spaceship> ships;

    public Spaceship spaceship { get; private set; }



    public override void OnCreate()
    {
        ships = Resources.LoadAll<Spaceship>("").ToList();
        SetObjectData(GetSaveData());
    }
    public override void OnInitialize()
    {
        spaceship.Initialize();
    }

    public void ChangePlayerPosition(Vector3 position)
    {
        spaceship.transform.position = position;
    }
    public override Dictionary<string, object> GetObjectData()
    {
        var newData = new Dictionary<string, object>();
        var data = GetSaveData();

        newData.Add("Current Spaceship", spaceship.ToString());
        newData.Add(spaceship.ToString(), spaceship.GetObjectData());

        var resultData = AddSaveData(data, newData);

        return resultData;
    }
    public override void SetObjectData(Dictionary<string, object> obj)
    {
        Dictionary<string, object> shipData = null;

        if (obj.ContainsKey("Current Spaceship"))       
            CreatePlayer(obj["Current Spaceship"].ToString());            
        else       
            CreatePlayer("Spaceship 1");

        if (obj.ContainsKey(spaceship.ToString()))
            shipData = MyTools.JObjectToDict<string, object>((JObject)obj[spaceship.ToString()]);

        spaceship.SetObjectData(shipData);
    }
    public override string ToString()
    {
        var str = "Spaceships";
        return str;
    }

    private void CreatePlayer(string Name)
    {
        var playerPref = ships.FindAll(ship => ship.Name == Name);
            if (playerPref.Count > 1)
                throw new System.Exception("Multiple items with the same name found");
            else if (playerPref.Count < 1)
                throw new System.Exception($"No elements with the name {Name} were found");
            else
                spaceship = GameObject.Instantiate(playerPref[0]);

    }
    private Dictionary<string, object> GetSaveData()
    {
        var saveDataInteractor = Game.saveController;
        var objectData = saveDataInteractor.Load(ToString());

        if(objectData != null)
            return objectData;

        return new Dictionary<string, object>();
    }

    private Dictionary<string, object> AddSaveData(Dictionary<string, object> dataIn, Dictionary<string, object> fromData)
    {
        var resultData = new Dictionary<string, object>(dataIn);

        foreach (KeyValuePair<string, object> entry in fromData)
        {
            if (resultData.ContainsKey(entry.Key))
                resultData[entry.Key] = entry.Value;
            else
                resultData.Add(entry.Key, entry.Value);
        }

        return resultData;
    }
}
