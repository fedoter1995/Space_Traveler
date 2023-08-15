using TMPro;
using UnityEngine.UI;

namespace SpaceTraveler.UI
{
    public class DubleStateButton : Button
    {
        private const string AVAILABLE_BUTTON = "Equip";
        private const string UNAVAILABLE_BUTTON = "Craft";

        private TextMeshProUGUI textMesh;

        public bool State { get; private set; } = false;

        public void Initialize()
        {
            textMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ChangeState(bool state)
        {
            State = state;

            if (State)
                textMesh.text = AVAILABLE_BUTTON;
            else
                textMesh.text = UNAVAILABLE_BUTTON;
        }
    }
}
