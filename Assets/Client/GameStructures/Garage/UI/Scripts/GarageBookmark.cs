using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CustomTools;
using TMPro;
using UnityEngine.UI;

public class GarageBookmark : Bookmark
{


    [SerializeField]
    private GarageTab _tab;

    public event Action<GarageBookmark> OnClickEvent;

    
    public override void Initialize()
    {
        base.Initialize();
        _tab.Initialize();
    }

    public override void MouseEnter()
    {
        
    }

    public override void MouseExit()
    {
        
    }

    public override void SetActive(bool activity)
    {
        _tab.SetActive(activity);
        base.SetActive(activity);
    }

    public override void OnClick()
    {
        OnClickEvent?.Invoke(this);
    }
}

