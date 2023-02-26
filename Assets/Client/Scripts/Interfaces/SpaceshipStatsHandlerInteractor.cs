using Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SpaceshipStatsHandlerInteractor : Interactor
{
    public ShipStatsHandler statsHandler { get; private set; }

    public override void OnCreate()
    {
        statsHandler = new ShipStatsHandler();
    }
    public override void OnInitialize()
    {
        var saveDataInteractor = Game.saveController;
        var objectData = saveDataInteractor.Load(ToString());
        if (objectData != null)
            statsHandler.SetObjectData(objectData);
        else
        {
            if (Game.HaveComponent<SpaceshipInteractor>())
            {
                var spaceship = Game.GetInteractor<SpaceshipInteractor>().spaceship;
                statsHandler = spaceship.Stats;
            }
            else
            {
                statsHandler = new ShipStatsHandler();
            }
        }
    }
    public override Dictionary<string, object> GetObjectData()
    {
        return statsHandler.GetObjectData();
    }

    public override void SetObjectData(Dictionary<string, object> data)
    {
        statsHandler.SetObjectData(data);
    }
}

