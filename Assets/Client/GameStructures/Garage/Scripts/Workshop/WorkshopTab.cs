using Architecture;
using GameStructures.Garage.Workshop;
using GameStructures.Gear;
using Garage.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopTab : GarageTab
{   
    [SerializeField]
    private EquipmentBookmark _bookmarkPrefab;
    [SerializeField]
    private Transform _equipmentList;
    [SerializeField]
    private Transform _treesList;
    [SerializeField]
    private Button _craftButton;




    private List<EquipmentTree> threes;

    private List<EquipmentBookmark> bookmarks;

    private EquipmentBookmark activeBookmark;

    private Spaceship spaceship;

    public override void Initialize()
    {
        Close();

        InitTrees();

        _craftButton.onClick.AddListener(TryChangeEquipment);
    }
    protected override void OnOpen()
    {
        OpenTree(bookmarks[0]);
    }
    public void OpenTree(EquipmentBookmark bookmark)
    {
        if(activeBookmark != null)
            SetActiveBookmark(activeBookmark, false);
        activeBookmark = bookmark;
        SetActiveBookmark(activeBookmark, true);
    }

    private void TryChangeEquipment()
    {
        var slot = activeBookmark.Three.SlotActual;

        if (slot.Availability)
        {    
            activeBookmark.Three.SetActualEquipment(slot.Equip);
            spaceship.Equipment.SetEquipment(slot.Equip);
        }
        else
            TryToCraftEquipment(slot);

    }
    private void SetActiveBookmark(EquipmentBookmark bookmark, bool activity)
    {
        bookmark.SetActive(activity);
    }
    private void InitTrees()
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
    private bool TryToCraftEquipment(EquipmentUISlot slot)
    {
        if (CheckRequirements(slot.Requirements))
        {
            slot.SetAvailability(true);

            activeBookmark.Three.SetActualEquipment(slot.Equip);
            spaceship.WorkshopSettings.AddToAvailableEquipment(slot.Equip);
            spaceship.Equipment.SetEquipment(slot.Equip);
            return true;
        }


        return false;
    }
    private void CreateTrees()
    {
        spaceship = Game.GetInteractor<SpaceshipInteractor>().spaceship;

        threes = new List<EquipmentTree>();

        var threesPrefabs = new List<EquipmentTree>(spaceship.WorkshopSettings.Trees);
        var activeEquipment = spaceship.Equipment.GetEquipment();
        var availableEquipment = spaceship.WorkshopSettings.AvailableEquipment;

        bookmarks = new List<EquipmentBookmark>();

        foreach (EquipmentTree treePrefab in threesPrefabs)
        {

            var tree = Instantiate(treePrefab, _treesList);

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
    private bool CheckRequirements(CraftRequirements requirements)
    {
        var inventory = spaceship.Inventory;

        var availableEquipment = spaceship.WorkshopSettings.AvailableEquipment;

        foreach(Equipment equipment in requirements.Equipments)
            if (!availableEquipment.Contains(equipment))
                return false;
        

        foreach(ItemSlot slot in requirements.Items)
            if (!inventory.HaveItemAmount(slot.CurrentItem, slot.Amount))
                return false;

        foreach (ItemSlot slot in requirements.Items)
            inventory.TryToRemove(slot.CurrentItem, slot.Amount);

        return true;
    }
}
