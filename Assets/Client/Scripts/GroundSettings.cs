using SpaceTraveler.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Audio
{
    public class GroundSettings : MonoBehaviour
    {
        [SerializeField]
        private GroundType _type = GroundType.Wood;

        [SerializeField]
        private GroundAudioSettings _audioSettings;




        public GroundType type => _type;

        public GroundAudioSettings AudioSettings => _audioSettings;
    }
    public enum GroundType
    {
        Metal,
        Wood,
    }

}