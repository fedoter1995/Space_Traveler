using UnityEngine.EventSystems;
using UnityEngine;

namespace SpaceTraveler.UI.ItemStorage
{
    public class UISlot : MonoBehaviour, IDropHandler
    {
        public virtual void OnDrop(PointerEventData eventData)
        {
            var otherItem = eventData.pointerDrag.transform;
            otherItem.SetParent(transform);
            otherItem.localPosition = Vector3.zero;
        }
    }
}
