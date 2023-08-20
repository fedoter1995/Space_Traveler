using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  SpaceTraveler.VFX
{
    public class PoolsParticles : MonoBehaviour, IPoolsObject<PoolsParticles>
    {
        [SerializeField]
        private ParticleSystem _particleSystem;
        public Action<PoolsParticles> OnDisableObject { get; set; }


        public ParticleSystem ParticleSystem => _particleSystem;
    }
}

