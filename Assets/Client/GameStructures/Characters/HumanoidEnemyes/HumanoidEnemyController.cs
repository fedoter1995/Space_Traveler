using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters.HumanoidEnemyes
{
    public class HumanoidEnemyController : MonoBehaviour
    {
        [SerializeField]
        private CharacterSurfaceCheckHandler m_surfaceCheckHandler;

        public bool OnGround => m_surfaceCheckHandler.OnGround;
        public CharacterSurfaceCheckHandler SurfaceCheckHandler => m_surfaceCheckHandler;
    }
}
