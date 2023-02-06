using Architecture;
using CustomTools;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipInteractor : Interactor
{
    public Spaceship spaceship { get; private set; }

    public override void OnCreate()
    {
        Vector3 playerPosition = PlayerPosition.instance.Position;
        Object playerLoad = Resources.Load("SpaceShip");
        CreatePlayer(playerLoad);
   
        ChangePlayerPosition(playerPosition);
    }

    public override void OnInitialize()
    {
        var saveDataInteractor = Game.GetInteractor<SaveDataInteractor>();
        var objectData = saveDataInteractor.Load(ToString());
        spaceship.SetObjectData(objectData);
    }

    private void CreatePlayer(Object playerPref)
    {
        spaceship = MyTools.Create(playerPref).GetComponent<Spaceship>();
    }
    private void ChangePlayerPosition(Vector3 position)
    {
        spaceship.transform.position = position;
    }

    public override Dictionary<string, object> GetObjectData()
    {
        return spaceship.GetObjectData();
    }

    public override void SetObjectData(Dictionary<string, object> obj)
    {
        if (obj != null)
            spaceship.SetObjectData(obj);
    }

    public override string ToString()
    {
        return "Spaceship";
    }
}
