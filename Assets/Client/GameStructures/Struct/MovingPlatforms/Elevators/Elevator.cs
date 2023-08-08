using GameStructures.Struct;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.MovingPlatforms.Elevators
{
    public class Elevator : MonoBehaviour
    {
        [SerializeField]
        private MovingPlatform _movingPlatform;
        [SerializeField]
        private List<ObjectButton> _buttons = new List<ObjectButton>();

        private void Awake()
        {
            if (_movingPlatform == null)
                throw new Exception($"Moving platform is Null");

            foreach ( var button in _buttons )
            {
                button.OnClickEvent += OnClickButton;
            }
            
        }

        private void OnClickButton(ObjectButton button)
        {

            var buttonIndex = _buttons.IndexOf(button);
            _movingPlatform.MoveTo(buttonIndex);
        }
    }
}
