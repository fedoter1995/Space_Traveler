using Architecture;
using GameStructures.Equipment;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInteractor : Interactor
{
    public EquipmentHandler equipment;

    public List<Equipment> availableEquipment;

    public override void OnCreate()
    {
        equipment = new EquipmentHandler();
        availableEquipment = new List<Equipment>();
    }
    public override void OnInitialize()
    {
        var saveDataInteractor = Game.saveController;
        var objectData = saveDataInteractor.Load(ToString());
        if (objectData != null)
            equipment.SetObjectData(objectData);
        else
        {
            if(Game.HaveComponent<SpaceshipInteractor>())
            {
                var spaceship = Game.GetInteractor<SpaceshipInteractor>().spaceship;
                equipment = spaceship.Equipment;
                availableEquipment.AddRange(equipment.GetEquipment());
            }
            else
            {
                equipment = new EquipmentHandler();
            }

        }
    }

    public override Dictionary<string, object> GetObjectData()
    {
        var data = new Dictionary<string, object>(equipment.GetObjectData());

        var items = new List<Dictionary<string, object>>();
        for(int i = 0; i < availableEquipment.Count; i++)
        {
            items.Add(availableEquipment[i].GetObjectData());
        }

        data.Add("Available_Equipment", items);

        return data;
    }

    public override void SetObjectData(Dictionary<string, object> data)
    {

        if (data != null)
        {
            JObject equipJObject = (JObject)data[equipment.ToString()];
            var equipData = equipJObject.ToObject<Dictionary<string, object>>();
            JArray availableEquipJArray = (JArray)data["Available_Equipment"];
            var availableEquipData = availableEquipJArray.ToObject<List<Dictionary<string, object>>>();
            var repository = Game.GetRepository<ItemsRepository>();
            equipment.SetObjectData(equipData);

            foreach(Dictionary<string, object> eachData in availableEquipData)
            {
                Equipment equip = repository.GetItem<Equipment>(eachData["Id"].ToString());

                availableEquipment.Add(equip);
            }

        }

    }

    public override string ToString()
    {
        return "Spaceship Equipment";
    }
}
