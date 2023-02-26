using Architecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInteractor : Interactor
{
    public EquipmentHandler equipment;

    public override void OnCreate()
    {
        equipment = new EquipmentHandler();
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
            }
            else
            {
                equipment = new EquipmentHandler();
            }

        }
    }
    public override void OnStart()
    {
        
    }

    public override Dictionary<string, object> GetObjectData()
    {
        return equipment.GetObjectData();
    }

    public override void SetObjectData(Dictionary<string, object> data)
    {
        equipment.SetObjectData(data);
    }

    public override string ToString()
    {
        return "Spaceship Equipment";
    }
}
