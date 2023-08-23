using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Scripting.APIUpdating;

namespace SpaceTraveler.GameStructures.Characters.ControllerStates
{
    public interface ICharacterControllerState
    {
        void Move();
    }
}
