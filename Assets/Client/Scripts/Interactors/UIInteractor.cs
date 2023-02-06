using UnityEngine;
using Architecture;
using System.Collections.Generic;

public class UIInteractor : Interactor
{

    private SpaceshipInteractor playerInteractor;

    public override void OnCreate()
    {
        LoadResources();
    }
    protected override void Initialize()
    {
        GetInteractors();
    }
    public override void OnStart()
    {
        InitDisplayTooltip();
    }
    private void LoadResources()
    {

    }
    private void GetInteractors()
    {
    }


    // use after OnInitialize

/*    private void InitInventoryUI()
    {
        var itemCollection = playerInteractor.player.Inventory.Items;
        ui.Inventory.Initialize(itemCollection);
    }*/
    public void InitDisplayTooltip()
    {

    }
    private void InitDate()
    {        

    }

    public override Dictionary<string, object> GetObjectData()
    {
        throw new System.NotImplementedException();
    }

    public override void SetObjectData(Dictionary<string, object> obj)
    {
        throw new System.NotImplementedException();
    }
}
