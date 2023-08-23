using Architecture;
using SpaceTraveler.GameStructures.ItemCollections;
using System.Collections.Generic;

public class InventoryInteractor : Interactor
{
    public Inventory collection;

    public override void OnCreate()
    {
        collection = new Inventory();
        var saveDataInteractor = Game.saveController;
        var objectData = saveDataInteractor.Load(ToString());
        if (objectData != null)
            collection.SetObjectData(objectData);
        else
        {

            if (Game.HaveComponent<SpaceshipInteractor>())
            {

                var spaceship = Game.GetInteractor<SpaceshipInteractor>().spaceship;
                collection = spaceship.Inventory;
            }
            else
            {
                collection = new Inventory();
            }
        }
    }
    public override Dictionary<string, object> GetObjectData()
    {
        return collection.GetObjectData();
    }
    public override void SetObjectData(Dictionary<string, object> data)
    {
        collection.SetObjectData(data);
    }
    public override string ToString()
    {
        return "Inventory";
    }
}
