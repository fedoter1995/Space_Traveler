using Architecture;
using CustomTools;
using Newtonsoft.Json.Linq;
using SpaceTraveler.GameStructures.Characters.Player;
using SpaceTraveler.GameStructures.Spaceship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Client.Scripts.Interactors
{
    public class ActorInteractor : Interactor
    {
        public Actor actor { get; private set; }


        public override void OnCreate()
        {
            SetObjectData(GetSaveData());
        }
        public override void OnInitialize()
        {
            
        }

        public void ChangePlayerPosition(Vector3 position)
        {
            actor.transform.position = position;
        }
        public override Dictionary<string, object> GetObjectData()
        {
            var newData = new Dictionary<string, object>();
            var data = GetSaveData();

            newData.Add("Actor", actor.ToString());
            newData.Add(actor.ToString(), actor.GetObjectData());

            var resultData = AddSaveData(data, newData);

            return resultData;
        }
        public override void SetObjectData(Dictionary<string, object> obj)
        {
            CreatePlayer("Knight");
        }
        public override string ToString()
        {
            var str = "Spaceships";
            return str;
        }
        private void CreatePlayer(string Name)
        {
            var spawns = GameObject.FindObjectsOfType<PlayerSpawn>();

            var playerPref = Resources.Load<Actor>(Name);
            if (playerPref == null)
                throw new System.Exception($"No elements with the name {Name} were found");
            else
                actor = GameObject.Instantiate(playerPref);

            actor.gameObject.SetActive(false);

            foreach (PlayerSpawn spawn in spawns)
            {
                if (spawn.IsInitial)
                {
                    actor.gameObject.SetActive(true);
                    ChangePlayerPosition(spawn.Position);
                }
            }
        }
        private Dictionary<string, object> GetSaveData()
        {
            var saveDataInteractor = Game.saveController;
            var objectData = saveDataInteractor.Load(ToString());

            if (objectData != null)
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
}
