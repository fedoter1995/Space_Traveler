using Architecture;
using SpaceTraveler.GameStructures.Characters.Player;
using SpaceTraveler.GameStructures.InterractiveObjects;
using SpaceTraveler.GameStructures.ItemCollections.UI;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.GameStructures.ItemCollections
{
    public class ItemStorage : Interractive2DObject
    {
        [SerializeField]
        private Inventory _inventory;

        private Actor actor;

        public event Action<Inventory> OnOpenContainerEvent;
        public event Action OnCloseContainerEvent;

        private void Awake()
        {

        }
        public override void Interract(Actor actor)
        {

        }

        private void OpenStorage(Actor actor)
        {

        }
        private void CloseContainer(Actor actor)
        {

        }
        public void InitializeUI()
        {

        }
    }
}
