using System.Collections;
using UI;
using UI.Tooltip;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TooltipUIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);

}
