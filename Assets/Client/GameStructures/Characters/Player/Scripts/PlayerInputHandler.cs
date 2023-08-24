using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceTraveler.GameStructures.Characters.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {

        private PlayerActions _palyerActions;

        private Vector2 moveInput;

        public int MoveX { get; private set; }
        public int MoveY { get; private set; }

        private void Awake()
        {
            _palyerActions = new PlayerActions();
        }
        private void OnEnable()
        {
            _palyerActions.Gameplay.Movement.started += OnMoveInput;
            _palyerActions.Gameplay.Movement.performed += OnMoveInput;
            _palyerActions.Gameplay.Movement.canceled += OnMoveInput;
            _palyerActions.Gameplay.Jump.started += OnJump;

            _palyerActions.Gameplay.Movement.Enable();
            _palyerActions.Gameplay.Jump.Enable();
        }
        private void OnDisable()
        {
            _palyerActions.Gameplay.Movement.Disable();
            _palyerActions.Gameplay.Jump.Disable();
        }
        public void OnMoveInput(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
            MoveX = (int)moveInput.x;
            MoveY = (int)moveInput.y;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            Debug.Log("Jump");
        }
    }
}

