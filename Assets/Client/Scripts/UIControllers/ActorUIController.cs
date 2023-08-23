using Architecture;
using Assets.Client.Scripts.Interactors;
using SpaceTraveler.GameStructures.ItemCollections;
using SpaceTraveler.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.UIControllers
{
    public class ActorUIController : UIController
    {
        public ActorUi ui { get; private set; }

        private ActorInteractor playerInteractor;

        public override void OnCreate()
        {
            LoadResources();
        }
        protected override void Initialize()
        {
            GetRepositories();
            GetInteractors();
        }
        public override void OnStart()
        {
            
        }
        private void LoadResources()
        {
            ui = GameObject.FindObjectOfType<ActorUi>();
            if (ui == null)
            {
                var prefab = Resources.Load<ActorUi>("UIInterface");
                ui = GameObject.Instantiate<ActorUi>(prefab);
            }
        }
        private void GetInteractors()
        {
            playerInteractor = Game.GetInteractor<ActorInteractor>();
        }
        private void GetRepositories()
        {
        }

        public void UIContainerAddListener(ItemStorage itemStorage)
        {

        }
    }
}
