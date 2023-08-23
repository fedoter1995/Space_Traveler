using Architecture;
using Assets.Client.Scripts.Interactors;
using SpaceTraveler.GameStructures.ItemCollections;
using SpaceTraveler.UI.ItemStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.UI
{
    public class ItemStorageUI : UIWidget
    {
        [SerializeField]
        private ItemStoragePanel _itemContainer;
        [SerializeField]
        private ItemStoragePanel _playerInventory;

        private Inventory inventory;
        public virtual void Initialize()
        {

        }
        public void AddListener(object listener)
        {

        }
        public void OpenContainer(ItemCollection itemCollection)
        {

        }
        public void CloseContainer()
        {

        }
    }
}
