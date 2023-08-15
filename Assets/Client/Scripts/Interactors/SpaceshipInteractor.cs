using Architecture;
using CustomTools;
using Newtonsoft.Json.Linq;
using SpaceTraveler.GameStructures.Spaceship;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipInteractor : Interactor
{

    public Starship spaceship { get; private set; }


    public override void OnCreate()
    {
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
            shipData = CustomConvert.JObjectToDict<string, object>((JObject)obj[spaceship.ToString()]);

        spaceship.SetObjectData(shipData);
    }
    public override string ToString()
    {
        var str = "Spaceships";
        return str;
    }

    private void CreatePlayer(string Name)
    {

        var spawns = Object.FindObjectsOfType<PlayerSpawn>();

        var playerPref = Resources.Load<Starship>(Name);
        if (playerPref == null)
        throw new System.Exception($"No elements with the name {Name} were found");
        else
            spaceship = GameObject.Instantiate(playerPref);

        spaceship.gameObject.SetActive(false);

        foreach (PlayerSpawn spawn in spawns)
        {
            if(spawn.IsInitial)
            {
                spaceship.gameObject.SetActive(true);
                ChangePlayerPosition(spawn.Position);
            }
        }

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
