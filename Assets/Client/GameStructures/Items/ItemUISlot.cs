using SpaceTraveler.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpaceTraveler.GameStructures.Items.UI
{
    public class ItemUISlot : TooltipUIObject
    {

        [SerializeField]
        private Image _image;
        [SerializeField]
        private Image _stroke;
        [SerializeField]
        private CanvasGroup _alertGroup;
        [SerializeField]
        private float _hideDelta = 0.01f;

        private Item item;

        public Item Item => item;

        public bool Availability { get; private set; } = false;

        public void SetItem(Item item, bool availability)
        {
            this.item = item;

            _image.sprite = this.item.Icon;

            Availability = availability;

            SetStrokeColor(availability);

        }
        private void SetStrokeColor(bool availability)
        {
            if (availability)
                _stroke.color = Color.green;
            else
                _stroke.color = Color.red;
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log(this);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {

        }

        public void Alert()
        {
            StopAllCoroutines();
            _alertGroup.alpha = 1;
            StartCoroutine(HideRoutine(_hideDelta));
        }

        private IEnumerator HideRoutine(float delta)
        {
            while(_alertGroup.alpha > 0)
            {
                yield return new WaitForEndOfFrame();
                _alertGroup.alpha -= delta;
            }
            yield return StartCoroutine(ShowRoutine(delta));
        }
        private IEnumerator ShowRoutine(float delta)
        {
            while (_alertGroup.alpha < 1)
            {
                yield return new WaitForEndOfFrame();
                _alertGroup.alpha += delta;
            }
            yield return new WaitForEndOfFrame();

        }
    }
}

