using Architecture;
using GameStructures.Garage.Workshop;
using GameStructures.Gear;
using Garage.UI;
using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;

public class WorkshopTab : GarageTab
{   

    [SerializeField]
    private EquipmentBookmark _bookmarkPrefab;
    [SerializeField]
    private CraftPanel _craftPanel;


    [SerializeField]
    private Transform _treesList;
    [SerializeField]
    private Transform _equipmentList;


    private List<EquipmentTree> threes;

    private List<EquipmentBookmark> bookmarks;

    private EquipmentBookmark activeBookmark;

    private Spaceship spaceship;


    public EquipmentUISlot SlotActual => activeBookmark.Three.SlotActual;

    public override void Initialize()
    {
        Close();

        _craftPanel.Initialize();

        _craftPanel.ButtonClickEvent.AddListener(TryChangeEquipment);

        spaceship = Game.GetInteractor<SpaceshipInteractor>().spaceship;

        InitTrees();
    }
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

    private void TryChangeEquipment()
    {
        var slot = activeBookmark.Three.SlotActual;

        if (slot.Availability)
        {    
            spaceship.Equipment.SetEquipment(slot.Equip);
            activeBookmark.SetEquipment(slot.Equip);
        }
        else
        {
            if(_craftPanel.TryToCraftEquipment(slot))
            {
                activeBookmark.SetEquipment(slot.Equip);
                OnChangeTreeItem(slot);
            }
        }
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
    private void CreateTrees()
    {

        threes = new List<EquipmentTree>();

        var threesPrefabs = new List<EquipmentTree>(spaceship.WorkshopSettings.Trees);
        var activeEquipment = spaceship.Equipment.GetEquipment();
        var availableEquipment = spaceship.WorkshopSettings.AvailableEquipment;

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
