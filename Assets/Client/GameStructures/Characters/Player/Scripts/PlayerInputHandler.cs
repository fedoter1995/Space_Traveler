using System;
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
        public bool JumpInput { get; private set; }

        public event Action ChangeStanceEvent;
        public event Action JumpEvent;

        private void Awake()
        {
            _palyerActions = new PlayerActions();
        }
        private void OnEnable()
        {
            Initialize();
        }
        private void OnDisable()
        {
            _palyerActions.Gameplay.Movement.Disable();
            _palyerActions.Gameplay.Jump.Disable();
            _palyerActions.Gameplay.ChangeStance.Disable();
        }
        private void OnMoveInput(InputAction.CallbackContext context)
        {            
            moveInput = context.ReadValue<Vector2>();

            MoveX = (int)Math.Round(moveInput.x, 0);
            MoveY = (int)Math.Round(moveInput.y, 0);
        }

        public void EndJump() => JumpInput = false;
        private void OnJump(InputAction.CallbackContext context) => JumpEvent?.Invoke();
        private void OnChangeStance(InputAction.CallbackContext context) => ChangeStanceEvent?.Invoke();
        private void Initialize()
        {
            _palyerActions.Gameplay.Movement.started += OnMoveInput;
            _palyerActions.Gameplay.Movement.performed += OnMoveInput;
            _palyerActions.Gameplay.Movement.canceled += OnMoveInput;


            _palyerActions.Gameplay.Jump.started += OnJump;
            _palyerActions.Gameplay.ChangeStance.started += OnChangeStance;


            _palyerActions.Gameplay.Movement.Enable();
            _palyerActions.Gameplay.Jump.Enable();
            _palyerActions.Gameplay.ChangeStance.Enable();
        }

    }
}

