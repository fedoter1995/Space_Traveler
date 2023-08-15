using UnityEngine;

namespace SpaceTraveler.UI
{
    public abstract class UIWidget : MonoBehaviour
    {
        protected void HideWidget()
        {
            gameObject.SetActive(false);
        }

        protected void ShowWidget()
        {
            gameObject.SetActive(true);
        }
    }
}
