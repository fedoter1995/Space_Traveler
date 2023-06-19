using GameStructures.Player;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Client.GameStructures.Player.Scripts
{
    public class KeyboardActorInputManager : IActorInputManager
    {
        #region Move Buttons
        private KeyCode Right = KeyCode.D;
        private KeyCode Left = KeyCode.A;
        private KeyCode Space = KeyCode.Space;
        private KeyCode Shift = KeyCode.LeftShift;
        private KeyCode Attack1Button = KeyCode.Mouse0;
        #endregion

        public Vector2 MoveVector
        {
            get
            {
                Vector2 moveVector = new Vector2(0, 0);

                if (Input.GetKey(Right))              
                    moveVector += new Vector2(1, 0);                
                if(Input.GetKey(Left))              
                    moveVector += new Vector2(-1, 0);
                
                return moveVector;
            }
        }
        public bool IsRun
        {
            get
            {
                return Input.GetKey(Shift);
            }
        }
        public bool IsMove
        {
            get
            {
                return Input.GetKey(Right) || Input.GetKey(Left);
            }
        }
        public bool Jump
        {
            get
            {
                return Input.GetKeyDown(Space);
            }
        }
        public bool Attack1
        {
            get
            {
                return Input.GetKeyDown(Attack1Button);
            }
        }

        public bool Attack2 => throw new System.NotImplementedException();

        public bool Attack3 => throw new System.NotImplementedException();
    }
}