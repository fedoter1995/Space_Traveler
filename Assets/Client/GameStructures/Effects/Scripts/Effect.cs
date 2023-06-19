using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking.Types;

namespace GameStructures.Effects
{
    [Serializable]
    public abstract class Effect 
    {
        public abstract void OnEffectAplly();
    }
}
