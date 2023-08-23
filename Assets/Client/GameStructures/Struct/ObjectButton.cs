using System;
using SpaceTraveler.GameStructures.Characters.Player;
using SpaceTraveler.GameStructures.InterractiveObjects;

namespace GameStructures.Struct
{
    public class ObjectButton : Interractive2DObject
    {
        public  event Action<ObjectButton> OnClickEvent;

        public override void Interract(Actor actor)
        {
            OnClickEvent?.Invoke(this);
        }
    }
}
