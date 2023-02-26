using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UI.Tooltip;
using UnityEngine;

public class EquipmentDescription : Description
{
    [SerializeField]
    private float _sensetivity = 0.05f;
    public override void Show(ITooltipData data)
    {
        var descriptionData = (DescriptionData)data;
        _header.text = descriptionData.Header;
        _description.text = descriptionData.Description;
        string footer = "";
        foreach(string str in descriptionData.Footer)
        {
            footer += str + END_LINE;
        }
        _footer.text = footer;

        if(actualEnumerator != null)
            StopCoroutine(actualEnumerator);

        actualEnumerator = StartCoroutine(ShowRoutine(_sensetivity));
    }
    
    public override void Hide()
    {
        if (actualEnumerator != null)
            StopCoroutine(actualEnumerator);

        actualEnumerator = StartCoroutine(HideRoutine(_sensetivity));
    }

}

