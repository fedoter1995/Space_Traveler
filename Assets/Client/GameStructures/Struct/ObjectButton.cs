using System;
using GameStructures.InterractiveObjects;
using UnityEngine;

namespace GameStructures.Struct
{
    public class ObjectButton : Interractive2DObject
    {
        public  event Action<ObjectButton> OnClickEvent;

        public override void Interract()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}
