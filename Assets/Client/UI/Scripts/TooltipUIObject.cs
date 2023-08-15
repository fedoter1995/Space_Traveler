using UnityEngine;
using UnityEngine.EventSystems;
namespace SpaceTraveler.UI
{
    public abstract class TooltipUIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public abstract void OnPointerEnter(PointerEventData eventData);

        public abstract void OnPointerExit(PointerEventData eventData);

    }
}

