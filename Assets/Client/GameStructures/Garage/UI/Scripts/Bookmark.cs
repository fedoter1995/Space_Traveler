using CustomTools;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Bookmark : MonoBehaviour, IBookmark
{

    [SerializeField]
    private Color _activeColor = Color.red;

    [SerializeField]
    protected Image image;
    [SerializeField]
    protected TextMeshProUGUI text;

    protected Color baseColor;

    public abstract void OnClick();


    public virtual void MouseEnter()
    {
        ChangeColor(_activeColor);
    }
    public virtual void MouseExit()
    {
        ChangeColor(baseColor);
    }
    public virtual void Initialize()
    {
        if(text == null)
        text = MyTools.GetComponentInChildren<TextMeshProUGUI>(gameObject);
        if(image == null)
        image = MyTools.GetComponentInChildren<Image>(gameObject);

        baseColor = text.color;
    }




    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseEnter();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        MouseExit();
    }

    public virtual void SetActive(bool activity)
    {
        if (activity)
            ChangeColor(_activeColor);
        else
            ChangeColor(baseColor);
    }
    protected void ChangeColor(Color newColor)
    {
        text.color = newColor;
    }
}
