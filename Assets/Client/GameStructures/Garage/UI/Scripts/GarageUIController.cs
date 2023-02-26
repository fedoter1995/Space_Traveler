using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Architecture;
using CustomTools;

public class GarageUIController : UIController
{
    private GarageUI garage;

    public override void OnCreate()
    {
        var garageUi = Object.FindObjectOfType<GarageUI>();
        if(garageUi == null)
        {
            FindCanvas();
            GarageUI uiLoad = Resources.Load<GarageUI>("GarageUI");
            garage = Object.Instantiate(uiLoad,uiContainer.transform);
        }
        else
        {
            garage = garageUi;
        }


    }
    public override void OnStart()
    {
        garage.Initialize();
    }
}
