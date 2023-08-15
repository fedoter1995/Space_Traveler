using GameStructures.Spaceship;
using UnityEngine;

public class KeyboardStarshipInputManager : IStarshipInputManager
{
    #region Move Buttons
    private KeyCode ImpulsButton = KeyCode.W;
    private KeyCode LeftDirrection = KeyCode.A;
    private KeyCode RightDirrection = KeyCode.D;
    #endregion
    private KeyCode FireButton = KeyCode.Space;

    public bool Move
    {
        get
        {
            return Input.GetKey(ImpulsButton);
        }
    }

    public bool Fire
    {
        get
        {
            return Input.GetKey(FireButton);
        }
    }

    public int Rotation
    {
        get
        {
            int value = 0;
            if (Input.GetKey(LeftDirrection))
                value++;
            if (Input.GetKey(RightDirrection))
                value--;

            return value;
        }
    }

}
