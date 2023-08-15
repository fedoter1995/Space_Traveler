using System;
using TMPro;
using UnityEngine;
using SpaceTraveler.GameStructures.InterractiveObjects;

namespace SpaceTraveler.UI.InterractiveObjectsUI
{
    [Serializable]
    public class InterractiveObjectInfoUI : UIWidget
    {
        [SerializeField]
        private TextMeshProUGUI _nameField;
        [SerializeField]
        private TextMeshProUGUI _titleField;
        [SerializeField]
        private TextMeshProUGUI _keyCodeField;


        public Interractive2DObject currentObject { get; private set; }

        private void Update()
        {
            if(Input.GetKeyDown(currentObject.Info.KeyCode)) 
            {
                currentObject.Interract();
            }
        }
        public void SetObject(Interractive2DObject obj)
        {
            currentObject = obj;
        }
        public  void HideContent()
        {
            _nameField.gameObject.SetActive(false);
            _titleField.gameObject.SetActive(false);
            _keyCodeField.gameObject.SetActive(false);

            HideWidget();
        }
        public void ShowContent(Interractive2DObject obj)
        {
            SetObject(obj);

            if (currentObject.Info.Name != null)
            {
                _nameField.text = currentObject.Info.Name;
                _nameField.gameObject.SetActive(true);
            }
            if (currentObject.Info.Title != null)
            {
                _titleField.text = currentObject.Info.Title;
                _titleField.gameObject.SetActive(true);
            }

            _keyCodeField.text = currentObject.Info.KeyCode.ToString();

            _keyCodeField.gameObject.SetActive(true);

            ShowWidget();

        }
    }
}
