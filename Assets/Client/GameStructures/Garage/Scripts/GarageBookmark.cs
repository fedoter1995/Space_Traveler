using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CustomTools;
using TMPro;
using UnityEngine.UI;

public class GarageBookmark : Bookmark
{

    private GarageTab tab;

    public event Action<GarageBookmark> OnClickEvent;

    public void SetGarageTab(GarageTab tab)
    {
        this.tab = tab;

        _textMesh.text = tab.Name;
    }
    public override void MouseEnter()
    {
    
    }

    public override void MouseExit()
    {
        
    }

    public override void SetActive(bool activity)
    {
        if(activity)
            tab.Open();
        else
            tab.Close();

        base.SetActive(activity);
    }

    public override void OnClick()
    {
        OnClickEvent?.Invoke(this);
    }
}

