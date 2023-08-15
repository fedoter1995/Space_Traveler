namespace UI.Tooltip
{
    /*    public class TooltipUI : MonoBehaviour
        {

            [SerializeField]
            private int _border = 10; // ширина обводки
            [SerializeField]
            private float _speed = 10; // скорость плавного затухания и проявления
            [SerializeField]
            private EquipmentDescription _description;
            [SerializeField]
            private Canvas _canvas;

            private ITooltipWindow windowActual;
            private RectTransform box => (RectTransform)_description.Box;
            public void ShowTooltip(ITooltipData data, PointerEventData eventData)
            {
                var objtPosition = eventData.pointerCurrentRaycast.gameObject.transform.position;
                var newPosition = objtPosition / _canvas.scaleFactor;

                newPosition = new Vector2(newPosition.x + 175, newPosition.y + 175);

                box.anchoredPosition = newPosition;

                _description.Show(data);
            }
            public void HideTooltip()
            {
                _description.Hide();
            }
        }*/
}
