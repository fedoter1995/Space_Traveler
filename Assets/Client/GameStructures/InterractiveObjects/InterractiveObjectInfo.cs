using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.InterractiveObjects
{
    [Serializable]
    public class InterractiveObjectInfo
    {
        [SerializeField]
        private string _name = "";
        [SerializeField]
        private string _title = "";
        [SerializeField]
        private KeyCode _keyCode = KeyCode.G;


        public KeyCode KeyCode => _keyCode;

        public string Name
        {
            get
            {
                if (_name.Length == 0)
                    return null;
                else 
                    return _name;

            }
        }

        public string Title
        {
            get
            {
                if (_title.Length == 0)
                    return null;
                else
                    return _title;

            }
        }
    }
}
