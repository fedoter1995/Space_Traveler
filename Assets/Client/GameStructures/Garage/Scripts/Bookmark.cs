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
    protected Image _image;
    [SerializeField]
    protected TextMeshProUGUI _textMesh;

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
        if(_textMesh == null)
            _textMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        if(_image == null)
            _image = gameObject.GetComponentInChildren<Image>();

        if (_textMesh == null)
            Debug.LogError("Text component is null");
        if (_image == null)
            Debug.LogError("Image component is null");


        baseColor = _textMesh.color;
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
        _textMesh.color = newColor;
    }
}
