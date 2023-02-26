using Architecture;
using Garage.UI;
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
        SetActive(false);
        equipHandler = Game.GetInteractor<EquipmentInteractor>().equipment;


        var equipment = equipHandler.GetEquipment();

        if(equipment.Count > _bookmarks.Count)
            while(equipment.Count > _bookmarks.Count)
            {
                var slot = Object.Instantiate(_bookmarks[0]);
                _bookmarks.Add(slot);
            }

        for (int i = 0; i < equipment.Count; i++)
        {
            _bookmarks[i].SetEquipment(equipment[i]);
            _bookmarks[i].Initialize();
            _bookmarks[i].OnChangeEquipmentEvent += ChangeEquipment;
        }
        SetListeners();

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
