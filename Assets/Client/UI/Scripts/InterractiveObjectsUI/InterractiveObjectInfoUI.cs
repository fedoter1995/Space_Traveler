using System;
using TMPro;
using UnityEngine;
using SpaceTraveler.GameStructures.InterractiveObjects;
using SpaceTraveler.GameStructures.Characters.Player;
using System.Collections.Generic;

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

        private Actor sender;
        private List<Interractive2DObject> currentObject;
        private Interractive2DObject activeObject;

        private void Update()
        {

        }
        public void SetObjects(Interractive2DObject obj, Actor sender)
        {

        }
        public  void HideContent()
        {

        }
        public void ShowContent(Interractive2DObject obj, Actor sender)
        {


        }
    }
}
