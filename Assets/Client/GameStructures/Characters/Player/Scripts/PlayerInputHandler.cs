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

        private PlayerActions _playerActions;

        private Vector2 moveInput;

        public int MoveX { get; private set; }
        public int MoveY { get; private set; }
        public bool JumpInput { get; private set; }
        public bool BlockInput { get; private set; }

        public event Action ChangeStanceEvent;
        public event Action JumpButtonEvent;
        public event Action FirstAttackButtonEvent;
        public event Action ChangePostureButtonEvent;
        public event Action<bool> BlockButtonEvent;

        private void Awake()
        {
            _playerActions = new PlayerActions();
        }
        private void OnEnable()
        {
            Initialize();
        }
        private void OnDisable()
        {
            _playerActions.Gameplay.Movement.Disable();
            _playerActions.Gameplay.Jump.Disable();
            _playerActions.Gameplay.ChangeStance.Disable();
            _playerActions.Gameplay.First_Attack.Disable();
            _playerActions.Gameplay.Block.Disable();
            _playerActions.Gameplay.Crouch.Disable();
        }
        private void OnMoveInput(InputAction.CallbackContext context)
        {            
            moveInput = context.ReadValue<Vector2>();

            MoveX = (int)Math.Round(moveInput.x, 0);
            MoveY = (int)Math.Round(moveInput.y, 0);
        }

        public void EndJump() => JumpInput = false;
        private void OnJump(InputAction.CallbackContext context) => JumpButtonEvent?.Invoke();
        private void OnChangeStance(InputAction.CallbackContext context) => ChangeStanceEvent?.Invoke();
        private void OnFirstAttackInput(InputAction.CallbackContext context) => FirstAttackButtonEvent?.Invoke();
        private void OnBlock(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                BlockButtonEvent?.Invoke(true);
            }
            else if (context.canceled)
            {
                BlockButtonEvent?.Invoke(false);
            }
        }
        private void OnCrouch(InputAction.CallbackContext context) => ChangePostureButtonEvent?.Invoke();
        private void Initialize()
        {
            //Movement inputs
            _playerActions.Gameplay.Movement.started += OnMoveInput;
            _playerActions.Gameplay.Movement.performed += OnMoveInput;
            _playerActions.Gameplay.Movement.canceled += OnMoveInput;
            _playerActions.Gameplay.Jump.started += OnJump;


            //Combat inputs
            _playerActions.Gameplay.Block.started += OnBlock;
            _playerActions.Gameplay.Block.canceled += OnBlock;
            _playerActions.Gameplay.Crouch.started += OnCrouch;
            _playerActions.Gameplay.ChangeStance.started += OnChangeStance;
            _playerActions.Gameplay.First_Attack.started += OnFirstAttackInput;

            _playerActions.Gameplay.Movement.Enable();
            _playerActions.Gameplay.Jump.Enable();
            _playerActions.Gameplay.ChangeStance.Enable();
            _playerActions.Gameplay.First_Attack.Enable();
            _playerActions.Gameplay.Block.Enable();
            _playerActions.Gameplay.Crouch.Enable();
        }

    }
}

