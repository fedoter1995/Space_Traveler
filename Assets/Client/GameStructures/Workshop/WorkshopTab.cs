using System;
using System.Collections.Generic;
using UnityEngine;
using SpaceTraveler.UI;
using SpaceTraveler.GameStructures.Craft.UI;
using SpaceTraveler.Gear.UI;
using SpaceTraveler.GameStructures.Gear;
using Assets.Client.GameStructures.Workshop;

namespace SpaceTraveler.GameStructures.Workshop.UI
{
    public abstract class WorkshopTab : InteractiveTab
    {   

        [SerializeField]
        protected EquipmentBookmark _bookmarkPrefab;
        [SerializeField]
        protected CraftPanel _craftPanel;


        [SerializeField]
        protected Transform _treesList;
        [SerializeField]
        protected Transform _equipmentList;


        protected List<EquipmentTree> threes;

        protected List<EquipmentBookmark> bookmarks;

        protected EquipmentBookmark activeBookmark;

        protected IInteractingWithWorkshop currentObject;

        public EquipmentUISlot SlotActual => activeBookmark.Three.SlotActual;

        protected override void OnOpen()
        {
            OpenTree(bookmarks[0]);
        }
        private void OnChangeTreeItem(EquipmentUISlot slot)
        {
            _craftPanel.OnChangeObject(slot);
        }
        public void OpenTree(EquipmentBookmark bookmark)
        {
            if(activeBookmark != null)
                SetActiveBookmark(activeBookmark, false);
            activeBookmark = bookmark;
            SetActiveBookmark(activeBookmark, true);
            OnChangeTreeItem(activeBookmark.Three.SlotActual);
        }

        protected void TryChangeEquipment()
        {
            var slot = activeBookmark.Three.SlotActual;

            if (slot.Availability)
            {
                currentObject.Equipment.TrySetEquipment(slot.Equip);
                activeBookmark.SetEquipment(slot.Equip);
            }
            else
            {
                if(_craftPanel.TryToCraftEquipment(slot))
                {
                    activeBookmark.SetEquipment(slot.Equip);
                    OnChangeTreeItem(slot);
                }
                else
                {
                    _craftPanel.Alert();
                }
            }
        }
        private void SetActiveBookmark(EquipmentBookmark bookmark, bool activity)
        {
            bookmark.SetActive(activity);
        }
        protected void InitTrees()
        {
            CreateTrees();
        }
        private void CreateBookmark(EquipmentTree three)
        {
            var bookmark = Instantiate(_bookmarkPrefab, _equipmentList);

            bookmark.Initialize();
            bookmark.SetThree(three);
            bookmark.OnClickEvent += OpenTree;
            SetActiveBookmark(bookmark, false);
            bookmarks.Add(bookmark);
        }
        private void CreateTrees()
        {

            threes = new List<EquipmentTree>();

            var threesPrefabs = new List<EquipmentTree>(currentObject.WorkshopSettings.Trees);
            var activeEquipment = currentObject.Equipment.GetEquipment();
            var availableEquipment = currentObject.WorkshopSettings.AvailableEquipment;

            bookmarks = new List<EquipmentBookmark>();

            foreach (EquipmentTree treePrefab in threesPrefabs)
            {

                var tree = Instantiate(treePrefab, _treesList);

                tree.OnChangeSlotEvent += OnChangeTreeItem;

                var type = Type.GetType(tree.TypeReference);

                foreach (Equipment equip in activeEquipment)
                {
                    if (equip.GetType() == type)
                    {
                        tree.Initialize(equip, availableEquipment);
                    }
                }

                CreateBookmark(tree);

                threes.Add(tree);
            }
        }
    }
}

