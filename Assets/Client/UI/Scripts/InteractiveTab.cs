using UnityEngine;

namespace SpaceTraveler.UI
{
    public abstract class InteractiveTab : MonoBehaviour
    {
        [SerializeField]
        private string _name = "InteractiveTab";
        public abstract void Initialize();
        protected abstract void OnOpen();

        public string Name => _name;

        public void Open()
        {
            SetActive(true);
            OnOpen();
        }
        public void Close()
        {
            SetActive(false);
        }

        private void SetActive(bool activity)
        {
            gameObject.SetActive(activity);
        }
    }
}


