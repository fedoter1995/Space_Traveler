using Architecture;
using GameStructures.Equipment;
using Garage.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopTab : GarageTab
{
    [SerializeField]
    private List<EquipmentBookmark> _bookmarks;
    [SerializeField]
    private Transform _equipmentList;
    [SerializeField]
    private EquipmentBookmark _activeBookmark;

    private EquipmentHandler equipHandler;

    public override void Initialize()
    {   
        Close();
        equipHandler = Game.GetInteractor<EquipmentInteractor>().equipment;


        var equipment = equipHandler.GetEquipment();

        foreach(EquipmentBookmark bookmark in _bookmarks)
        {
            var type = Type.GetType(bookmark.TypeReference);
            foreach (Equipment equip in equipment)
            {
                if (equip.GetType() == type)
                {
                    bookmark.SetEquipment(equip);
                    bookmark.Initialize();
                    bookmark.OnChangeEquipmentEvent += ChangeEquipment;
                }
                    
            }
        }

        SetListeners();
    }

    protected override void OnOpen()
    {
        OpenThree(_bookmarks[0]);
    }

    public void OpenThree(EquipmentBookmark bookmark)
    {
        SetActiveBookmark(_activeBookmark, false);
        _activeBookmark = bookmark;
        SetActiveBookmark(_activeBookmark, true);
    }
    private void ChangeEquipment(Equipment equip)
    {
        equipHandler.SetEquipment(equip);
    }
    private void SetActiveBookmark(EquipmentBookmark bookmark, bool activity)
    {
        bookmark.SetActive(activity);
    }

    private void SetListeners()
    {
        foreach (EquipmentBookmark bookmark in _bookmarks)
        {
            bookmark.OnClickEvent += OpenThree;
        }
    }
}
