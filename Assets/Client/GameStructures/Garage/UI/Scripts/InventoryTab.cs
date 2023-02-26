using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Architecture;
using UnityEngine;

public class InventoryTab : GarageTab
{
    [SerializeField]
    private UIItemCollection _itemCollection;
    public override void Initialize()
    {
        var iemCollection = Game.GetInteractor<InventoryInteractor>().collection;
        _itemCollection.Initilize(iemCollection);
        SetActive(false);
    }

}

