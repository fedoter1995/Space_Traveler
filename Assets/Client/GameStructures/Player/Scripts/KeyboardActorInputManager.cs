using GameStructures.Player;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Client.GameStructures.Player.Scripts
{
    public class KeyboardActorInputManager : IActorInputManager
    {
        #region Move Buttons
        private KeyCode right = KeyCode.D;
        private KeyCode left = KeyCode.A;
        private KeyCode space = KeyCode.Space;
        private KeyCode shift = KeyCode.LeftShift;
        private KeyCode attackButton1 = KeyCode.Mouse0;
        private KeyCode attackButton2 = KeyCode.Mouse1;
        private KeyCode blockButton = KeyCode.F;
        private KeyCode changeStanceButton = KeyCode.Q;

        public KeyCode ChangeStanceButton => changeStanceButton;
        #endregion

        public Vector2 MoveVector
        {
            get
            {
                Vector2 moveVector = new Vector2(0, 0);

                if(IsRun)
                {
                    if (Input.GetKey(right))              
                        moveVector += new Vector2(2, 0);                
                    if(Input.GetKey(left))              
                        moveVector += new Vector2(-2, 0);
                }
                else
                {
                    if (Input.GetKey(right))
                        moveVector += new Vector2(1, 0);
                    if (Input.GetKey(left))
                        moveVector += new Vector2(-1, 0);
                }

                
                return moveVector;
            }
        }
        public bool IsRun
        {
            get
            {
                return Input.GetKey(shift);
            }
        }
        public bool IsMove
        {
            get
            {
                return Input.GetKey(right) || Input.GetKey(left);
            }
        }
        public bool Jump
        {
            get
            {
                return Input.GetKeyDown(space);
            }
        }

        public KeyCode AttackButton1 => attackButton1;

        public KeyCode BlockButton => blockButton;

        public KeyCode AttackButton2 => attackButton2;
    }
}