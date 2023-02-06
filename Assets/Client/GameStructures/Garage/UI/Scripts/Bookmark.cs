using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Bookmark : MonoBehaviour, IBookmark
{
    public abstract void OnClick();
    public abstract void MouseEnter();
    public abstract void MouseExit();

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
}
