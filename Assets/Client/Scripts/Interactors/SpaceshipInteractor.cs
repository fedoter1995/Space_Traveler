using Architecture;
using CustomTools;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipInteractor : Interactor
{
    public Spaceship spaceship { get; private set; }

    public override void OnCreate()
    {
        var spaceship = GameObject.FindObjectOfType<Spaceship>();
        if (spaceship == null)
        {
            Vector3 playerPosition = PlayerPosition.instance.Position;
            Object playerLoad = Resources.Load("SpaceShip");
            CreatePlayer(playerLoad);
   
            ChangePlayerPosition(playerPosition);
        }
        else
        {
            this.spaceship = spaceship;
        }

    }

    public override void OnInitialize()
    {
        var saveDataInteractor = Game.saveController;
        var objectData = saveDataInteractor.Load(ToString());
        spaceship.SetObjectData(objectData);
    }

    public override void OnStart()
    {
        spaceship.Initialize();
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
